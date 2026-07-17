using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VPSManager.Models;
using VPSManager.Services;
using VPSManager.Utilities;

namespace VPSManager.ViewModels;

public partial class LoginLogsTabViewModel : ViewModelBase, IDisposable
{
    private readonly Timer _logPollingTimer;
    private bool _isFirstLoad = true;

    [ObservableProperty] private ObservableCollection<LoginLogItem> _logs = new();
    
    // Telegram Configuration
    [ObservableProperty] private bool _telegramEnabled;
    [ObservableProperty] private string _telegramChatId = string.Empty;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasEventLogWarning))]
    private string _eventLogWarning = string.Empty;

    public bool HasEventLogWarning => !string.IsNullOrEmpty(EventLogWarning);

    public LoginLogsTabViewModel()
    {
        // Tải cấu hình Telegram từ Settings
        var settings = SettingsService.Instance.Settings;
        TelegramChatId = settings.TelegramChatId;
        TelegramEnabled = settings.TelegramEnabled;

        // Tải logs lần đầu và bắt đầu Polling mỗi 10 giây
        RefreshLogs();
        _logPollingTimer = new Timer(PollLogsCallback, null, 10000, 10000);
    }

    private void PollLogsCallback(object? state)
    {
        RefreshLogs();
    }

    [RelayCommand]
    public void RefreshLogs()
    {
        try
        {
            var newLogs = EventLogService.Instance.GetRdpLogins(out string statusMsg);
            
            Dispatcher.UIThread.Post(() =>
            {
                EventLogWarning = statusMsg == "Thành công" ? string.Empty : statusMsg;
                
                // Cập nhật danh sách Logs bằng cách đồng bộ hóa thay vì Clear() để tránh reset thanh cuộn
                var targetLogs = newLogs.Take(200).ToList();
                if (targetLogs.Count == 0)
                {
                    if (Logs.Count > 0)
                    {
                        Logs.Clear();
                    }
                }
                else if (Logs.Count == 0)
                {
                    foreach (var log in targetLogs)
                    {
                        Logs.Add(log);
                    }
                }
                else
                {
                    var firstCurrentLog = Logs[0];
                    int matchIndex = targetLogs.FindIndex(l => l.RecordId == firstCurrentLog.RecordId 
                                                              && l.TimeCreated == firstCurrentLog.TimeCreated 
                                                              && l.Username == firstCurrentLog.Username 
                                                              && l.IpAddress == firstCurrentLog.IpAddress);
                    
                    if (matchIndex > 0)
                    {
                        for (int i = matchIndex - 1; i >= 0; i--)
                        {
                            Logs.Insert(0, targetLogs[i]);
                        }
                        
                        while (Logs.Count > 200)
                        {
                            Logs.RemoveAt(Logs.Count - 1);
                        }
                    }
                    else if (matchIndex == -1)
                    {
                        if (Logs.Count != targetLogs.Count || !Logs.SequenceEqual(targetLogs))
                        {
                            Logs.Clear();
                            foreach (var log in targetLogs)
                            {
                                Logs.Add(log);
                            }
                        }
                    }
                }

                // Xử lý gửi thông báo Telegram cho các sự kiện đăng nhập mới
                if (newLogs.Count > 0)
                {
                    ProcessNewLoginAlerts(newLogs);
                }
            });
        }
        catch (Exception ex)
        {
            Logger.Error("Lỗi trong quá trình làm mới logs", ex);
        }
    }

    private void ProcessNewLoginAlerts(System.Collections.Generic.List<LoginLogItem> allLogs)
    {
        var settings = SettingsService.Instance.Settings;
        long lastSuccessId = settings.LastProcessedSuccessRecordId;
        long lastFailureId = settings.LastProcessedFailureRecordId;

        // Tách biệt hai danh sách thành công và thất bại
        var successLogs = allLogs.Where(l => l.IsSuccess).ToList();
        var failureLogs = allLogs.Where(l => !l.IsSuccess).ToList();

        long maxSuccessId = successLogs.Count > 0 ? successLogs.Max(l => l.RecordId) : 0;
        long maxFailureId = failureLogs.Count > 0 ? failureLogs.Max(l => l.RecordId) : 0;

        if (_isFirstLoad)
        {
            // Lần chạy đầu tiên: Khởi tạo giá trị ID lớn nhất hiện tại để tránh spam thông báo lịch sử
            if (lastSuccessId == 0 && maxSuccessId > 0) settings.LastProcessedSuccessRecordId = maxSuccessId;
            if (lastFailureId == 0 && maxFailureId > 0) settings.LastProcessedFailureRecordId = maxFailureId;
            
            // Đồng bộ từ giá trị cũ nếu có
            if (settings.LastProcessedEventRecordId > 0)
            {
                if (settings.LastProcessedSuccessRecordId == 0) settings.LastProcessedSuccessRecordId = settings.LastProcessedEventRecordId;
                if (settings.LastProcessedFailureRecordId == 0) settings.LastProcessedFailureRecordId = settings.LastProcessedEventRecordId;
            }

            SettingsService.Instance.SaveSettings();
            _isFirstLoad = false;
            return;
        }

        var newEvents = new System.Collections.Generic.List<LoginLogItem>();

        // Lọc các bản ghi thành công mới
        if (successLogs.Count > 0 && lastSuccessId > 0)
        {
            var newSuccess = successLogs.Where(log => log.RecordId > lastSuccessId).ToList();
            newEvents.AddRange(newSuccess);
        }

        // Lọc các bản ghi thất bại mới
        if (failureLogs.Count > 0 && lastFailureId > 0)
        {
            var newFailure = failureLogs.Where(log => log.RecordId > lastFailureId).ToList();
            newEvents.AddRange(newFailure);
        }

        // Cập nhật ID mới nhất vào Settings
        bool settingsChanged = false;
        if (maxSuccessId > lastSuccessId)
        {
            settings.LastProcessedSuccessRecordId = maxSuccessId;
            settingsChanged = true;
        }
        if (maxFailureId > lastFailureId)
        {
            settings.LastProcessedFailureRecordId = maxFailureId;
            settingsChanged = true;
        }

        if (settingsChanged)
        {
            SettingsService.Instance.SaveSettings();
        }

        if (newEvents.Count > 0)
        {
            // Sắp xếp các sự kiện mới theo thời gian từ cũ đến mới để thông báo theo đúng trình tự
            var sortedEvents = newEvents.OrderBy(e => e.TimeCreated).ToList();

            if (TelegramEnabled && !string.IsNullOrWhiteSpace(TelegramChatId))
            {
                // Chạy tác vụ gửi Telegram ngầm, không block luồng UI
                Task.Run(async () =>
                {
                    foreach (var evt in sortedEvents)
                    {
                        await TelegramService.Instance.SendAlertAsync(
                            TelegramChatId,
                            evt.Action,
                            evt.Username,
                            evt.IpAddress,
                            evt.TimeCreated,
                            evt.IsSuccess
                        );
                        // Delay nhẹ giữa các thông báo tránh bị Telegram rate limit
                        await Task.Delay(500);
                    }
                });
            }
        }
    }

    [RelayCommand]
    private void SaveConfig()
    {
        // Tự động bật nếu có Chat ID, tắt nếu trống
        TelegramEnabled = !string.IsNullOrWhiteSpace(TelegramChatId);

        var settings = SettingsService.Instance.Settings;
        settings.TelegramEnabled = TelegramEnabled;
        settings.TelegramChatId = TelegramChatId;
        
        SettingsService.Instance.SaveSettings();
        
        Logger.Info($"Đã lưu cấu hình thông báo Telegram. Trạng thái: {(TelegramEnabled ? "Bật" : "Tắt")}");
    }

    [RelayCommand]
    private async Task TestTelegramAsync()
    {
        if (string.IsNullOrWhiteSpace(TelegramChatId))
        {
            await ShowMessageDialogAsync("Lỗi Test", "Chưa cấu hình Chat ID. Vui lòng nhập Chat ID trước khi test.");
            return;
        }

        bool success = await TelegramService.Instance.SendTestMessageAsync(TelegramChatId);
        
        if (success)
        {
            await ShowMessageDialogAsync("Gửi Test Thành Công", "Đã gửi tin nhắn test thành công! Vui lòng kiểm tra ứng dụng Telegram của bạn.");
        }
        else
        {
            await ShowMessageDialogAsync("Lỗi Gửi Test", "Gửi tin nhắn test thất bại! Vui lòng kiểm tra lại Chat ID hoặc kết nối mạng.");
        }
    }

    private async Task ShowMessageDialogAsync(string title, string content)
    {
        var lifetime = Avalonia.Application.Current?.ApplicationLifetime as Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime;
        if (lifetime?.MainWindow is Views.MainWindow mainWin)
        {
            await mainWin.ShowMessageDialog(title, content);
        }
    }

    public void Dispose()
    {
        _logPollingTimer.Dispose();
        GC.SuppressFinalize(this);
    }
}
