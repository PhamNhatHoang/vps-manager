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

public class RamIntervalItem
{
    public double Value { get; set; }
    public string Display { get; set; } = string.Empty;
}

public partial class VpsTabViewModel : ViewModelBase, IDisposable
{
    // Cấu hình Polling tài nguyên CPU/RAM
    private readonly Timer _resourceTimer;
    
    // Cấu hình Tự động dọn RAM
    private Timer? _clearRamTimer;
    private DateTime _nextClearTime;
    private readonly Timer _clearRamStatusTimer;

    // Các thuộc tính liên kết UI (Thông tin VPS)
    [ObservableProperty] private double _cpuUsage;
    [ObservableProperty] private double _ramUsagePercentage;
    [ObservableProperty] private string _ramUsageText = "Đang tải...";
    [ObservableProperty] private string _totalRam = "Đang tải...";
    [ObservableProperty] private int _cpuCores;
    [ObservableProperty] private string _osName = "Đang tải...";
    [ObservableProperty] private int _rdpPort;

    // Các thuộc tính thay đổi thông tin
    [ObservableProperty] private string _newRdpPortText = string.Empty;
    [ObservableProperty] private string _newUsername = string.Empty;
    [ObservableProperty] private string _newPassword = string.Empty;

    // Danh sách tài khoản local phục vụ các Combobox riêng biệt
    [ObservableProperty] private ObservableCollection<string> _usernames = new();
    [ObservableProperty] private string _selectedRenameUsername = string.Empty;
    [ObservableProperty] private string _selectedPasswordUsername = string.Empty;

    // Cấu hình Tự động dọn RAM
    public ObservableCollection<RamIntervalItem> ClearRamIntervals { get; } = new()
    {
        new() { Value = 0, Display = "Tắt" },
        new() { Value = 0.5, Display = "30 giây" },
        new() { Value = 1.0, Display = "1 phút" },
        new() { Value = 5.0, Display = "5 phút" },
        new() { Value = 15.0, Display = "15 phút" },
        new() { Value = 30.0, Display = "30 phút" },
        new() { Value = 60.0, Display = "60 phút" }
    };

    [ObservableProperty] private RamIntervalItem? _selectedIntervalItem;
    [ObservableProperty] private string _clearRamStatus = "Đang tắt";

    // Trạng thái chung / Log thao tác nhanh
    [ObservableProperty] private string _actionLogText = "Hệ thống sẵn sàng.\n";

    public VpsTabViewModel()
    {
        // 1. Polling tài nguyên hệ thống mỗi 2 giây
        _resourceTimer = new Timer(PollSystemResources, null, 0, 2000);

        // 2. Timer cập nhật text countdown cho Clear RAM mỗi giây để đếm giây chính xác hơn cho 30s
        _clearRamStatusTimer = new Timer(UpdateClearRamCountdown, null, 1000, 1000);

        // 3. Tải danh sách user local lên các combobox
        RefreshUsernames();

        // Tải cấu hình từ Settings
        var settings = SettingsService.Instance.Settings;
        if (!settings.ClearRamEnabled)
        {
            SelectedIntervalItem = ClearRamIntervals.FirstOrDefault(x => x.Value == 0) ?? ClearRamIntervals[0];
        }
        else
        {
            double savedInterval = settings.ClearRamIntervalMinutes;
            SelectedIntervalItem = ClearRamIntervals.FirstOrDefault(x => x.Value > 0 && Math.Abs(x.Value - savedInterval) < 0.01) ?? ClearRamIntervals[4];
        }
    }

    [RelayCommand]
    public void RefreshUsernames()
    {
        try
        {
            var users = UserAccountService.Instance.GetLocalUsers();
            
            // Sao lưu các lựa chọn hiện tại trước khi làm mới
            string currentRenameSel = SelectedRenameUsername;
            string currentPasswordSel = SelectedPasswordUsername;

            Usernames.Clear();
            foreach (var u in users)
            {
                Usernames.Add(u);
            }

            if (Usernames.Count > 0)
            {
                // Khôi phục lựa chọn cho Rename
                if (!string.IsNullOrEmpty(currentRenameSel) && Usernames.Contains(currentRenameSel))
                {
                    SelectedRenameUsername = currentRenameSel;
                }
                else
                {
                    SelectedRenameUsername = GetDefaultUser();
                }

                // Khôi phục lựa chọn cho Password
                if (!string.IsNullOrEmpty(currentPasswordSel) && Usernames.Contains(currentPasswordSel))
                {
                    SelectedPasswordUsername = currentPasswordSel;
                }
                else
                {
                    SelectedPasswordUsername = GetDefaultUser();
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Error("Lỗi khi tải danh sách local users", ex);
        }
    }

    private string GetDefaultUser()
    {
        if (Usernames.Contains("Administrator"))
        {
            return "Administrator";
        }
        if (Usernames.Contains(Environment.UserName))
        {
            return Environment.UserName;
        }
        return Usernames.Count > 0 ? Usernames[0] : string.Empty;
    }

    private void PollSystemResources(object? state)
    {
        try
        {
            var info = SystemInfoService.Instance.GetCurrentVpsInfo();
            Dispatcher.UIThread.Post(() =>
            {
                CpuUsage = info.CpuUsagePercentage;
                RamUsagePercentage = info.RamUsagePercentage;
                RamUsageText = info.RamUsageText;
                TotalRam = info.TotalRam;
                CpuCores = info.CpuCores;
                OsName = info.OsName;
                RdpPort = info.RdpPort;
            });
        }
        catch (Exception ex)
        {
            Logger.Error("Lỗi polling tài nguyên hệ thống", ex);
        }
    }

    // --- NHÓM CHỨC NĂNG: ĐỔI PORT RDP ---
    [RelayCommand]
    private async Task ChangeRdpPortAsync()
    {
        if (!int.TryParse(NewRdpPortText, out int port))
        {
            AppendLog("[LỖI] Đổi Port RDP thất bại: Cổng RDP nhập vào không phải là số hợp lệ.");
            return;
        }

        if (!SafePath.IsValidPort(port, out string valError))
        {
            AppendLog($"[LỖI] Đổi Port RDP thất bại: {valError}");
            return;
        }

        bool? confirm = await ShowConfirmDialogAsync("Đổi Cổng RDP", $"Bạn có chắc chắn muốn đổi cổng kết nối RDP sang {port}?\nLưu ý: Bạn cần tạo rule Firewall hoặc restart VPS/Service RDP sau đó.");
        if (confirm != true) return;

        bool success = RdpService.Instance.SetRdpPort(port, out string error);
        if (success)
        {
            RdpPort = port;
            NewRdpPortText = string.Empty;
            AppendLog($"[THÀNH CÔNG] Đã đổi cổng RDP sang {port}. Vui lòng restart VPS hoặc service TermService để cổng mới có hiệu lực.");
            
            // Hiển thị thông báo và tự động restart VPS
            await ShowMessageDialogAsync("Đổi Port RDP Thành Công", $"Đổi cổng RDP sang {port} thành công!\nỨng dụng sẽ tự động khởi động lại VPS ngay bây giờ để áp dụng thay đổi.");
            
            AppendLog("[HỆ THỐNG] Đang phát lệnh tự động khởi động lại VPS...");
            RestartService.Instance.RestartVps(out _);
        }
        else
        {
            AppendLog($"[LỖI] Đổi cổng RDP thất bại: {error}");
        }
    }

    // --- NHÓM CHỨC NĂNG: ĐỔI USERNAME ---
    [RelayCommand]
    private async Task ChangeUsernameAsync()
    {
        if (string.IsNullOrWhiteSpace(SelectedRenameUsername))
        {
            AppendLog("[LỖI] Chưa chọn tài khoản cần đổi.");
            return;
        }

        if (string.IsNullOrWhiteSpace(NewUsername))
        {
            AppendLog("[LỖI] Tên đăng nhập mới không được để trống.");
            return;
        }

        string oldUser = SelectedRenameUsername;
        bool? confirm = await ShowConfirmDialogAsync("Đổi Tên Tài Khoản", $"Bạn có chắc chắn muốn đổi tên tài khoản local '{oldUser}' thành '{NewUsername}'?");
        if (confirm != true) return;

        bool success = UserAccountService.Instance.ChangeUsername(oldUser, NewUsername, out string error);
        if (success)
        {
            string addedName = NewUsername;
            NewUsername = string.Empty;
            
            // Làm mới danh sách user
            RefreshUsernames();
            if (Usernames.Contains(addedName))
            {
                SelectedRenameUsername = addedName;
            }
            
            AppendLog($"[THÀNH CÔNG] Đã đổi tên tài khoản từ '{oldUser}' sang '{addedName}'. Hãy dùng tên mới cho lần login tiếp theo.");

            // Hiển thị thông báo thành công
            await ShowMessageDialogAsync("Đổi Tên Thành Công", $"Đổi tên tài khoản từ '{oldUser}' sang '{addedName}' thành công!\nVui lòng sử dụng tên đăng nhập mới cho lần kết nối tiếp theo.");
        }
        else
        {
            AppendLog($"[LỖI] Đổi tên tài khoản thất bại: {error}");
        }
    }

    // --- NHÓM CHỨC NĂNG: ĐỔI PASSWORD ---
    [RelayCommand]
    private async Task ChangePasswordAsync()
    {
        if (string.IsNullOrWhiteSpace(SelectedPasswordUsername))
        {
            AppendLog("[LỖI] Chưa chọn tài khoản cần đổi mật khẩu.");
            return;
        }

        if (string.IsNullOrWhiteSpace(NewPassword))
        {
            AppendLog("[LỖI] Mật khẩu mới không được để trống.");
            return;
        }

        bool? confirm = await ShowConfirmDialogAsync("Đổi Mật Khẩu", $"Bạn có chắc chắn muốn đổi mật khẩu cho tài khoản '{SelectedPasswordUsername}'?");
        if (confirm != true) return;

        bool success = UserAccountService.Instance.ChangePassword(SelectedPasswordUsername, NewPassword, out string error);
        if (success)
        {
            // In file changepass.txt ra desktop
            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = System.IO.Path.Combine(desktopPath, "changepass.txt");
                string content = $"- Username: {SelectedPasswordUsername}\r\n- Password: {NewPassword}\r\n- Time: {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                System.IO.File.WriteAllText(filePath, content);
                
                AppendLog($"[THÀNH CÔNG] Đã thay đổi mật khẩu cho tài khoản '{SelectedPasswordUsername}' thành công.");
                AppendLog($"[THÀNH CÔNG] Đã ghi thông tin mật khẩu mới vào file: {filePath}");

                // Hiển thị thông báo thành công
                await ShowMessageDialogAsync("Đổi Mật Khẩu Thành Công", $"Đổi mật khẩu tài khoản '{SelectedPasswordUsername}' thành công!\nThông tin mật khẩu mới đã được lưu tại file: {filePath}");
            }
            catch (Exception ex)
            {
                AppendLog($"[THÀNH CÔNG] Đã thay đổi mật khẩu thành công (Nhưng lỗi ghi file desktop: {ex.Message}).");
                await ShowMessageDialogAsync("Đổi Mật Khẩu Thành Công", $"Đổi mật khẩu tài khoản '{SelectedPasswordUsername}' thành công!\n(Lưu ý: Có lỗi xảy ra khi ghi file thông tin ra Desktop: {ex.Message})");
            }
            
            NewPassword = string.Empty;
        }
        else
        {
            AppendLog($"[LỖI] Đổi mật khẩu thất bại: {error}");
        }
    }

    // --- NHÓM CHỨC NĂNG: CLEAR RAM ---
 
    partial void OnSelectedIntervalItemChanged(RamIntervalItem? value)
    {
        bool enable = value != null && value.Value > 0;
        UpdateClearRamTimerState(enable);
    }
 
    private void UpdateClearRamTimerState(bool enable)
    {
        try
        {
            double intervalVal = SelectedIntervalItem?.Value ?? 0;
 
            // Lưu cấu hình vào file Settings
            var settings = SettingsService.Instance.Settings;
            settings.ClearRamEnabled = enable;
            if (enable)
            {
                settings.ClearRamIntervalMinutes = intervalVal;
            }
            SettingsService.Instance.SaveSettings();
 
            // Giải phóng timer cũ nếu đang chạy
            _clearRamTimer?.Dispose();
            _clearRamTimer = null;
 
            if (enable && intervalVal > 0)
            {
                _nextClearTime = DateTime.Now.AddMinutes(intervalVal);
                var dueTime = TimeSpan.FromSeconds(intervalVal * 60);
                _clearRamTimer = new Timer(ExecuteAutoClearRam, null, dueTime, dueTime);
                
                ClearRamStatus = $"Chờ dọn: {FormatInterval(intervalVal)}";
                AppendLog($"[INFO] Đã bật tự động dọn RAM (Mỗi {FormatInterval(intervalVal)}).");
            }
            else
            {
                ClearRamStatus = "Tắt";
                AppendLog("[INFO] Đã tắt tự động dọn RAM.");
            }
        }
        catch (Exception ex)
        {
            Logger.Error("Lỗi cấu hình Timer Clear RAM", ex);
        }
    }
 
    private string FormatInterval(double minutes)
    {
        if (minutes < 1.0)
        {
            return $"{(int)(minutes * 60)}s";
        }
        return $"{minutes}m";
    }
 
    private void ExecuteAutoClearRam(object? state)
    {
        Dispatcher.UIThread.Post(() => ClearRamStatus = "Đang dọn...");
        
        bool success = MemoryService.Instance.ClearRam(out string msg);
        
        Dispatcher.UIThread.Post(() =>
        {
            double intervalVal = SelectedIntervalItem?.Value ?? 15.0;
            _nextClearTime = DateTime.Now.AddMinutes(intervalVal);
            ClearRamStatus = success ? $"Chờ dọn: {FormatInterval(intervalVal)}" : "Lỗi dọn RAM";
            AppendLog($"[DỌN RAM] {msg}");
        });
    }
 
    private void UpdateClearRamCountdown(object? state)
    {
        if (SelectedIntervalItem == null || SelectedIntervalItem.Value == 0) return;
        
        var diff = _nextClearTime - DateTime.Now;
        if (diff.TotalSeconds > 0)
        {
            Dispatcher.UIThread.Post(() =>
            {
                if (diff.TotalMinutes < 1.0)
                {
                    ClearRamStatus = $"Chờ dọn: {(int)diff.TotalSeconds}s";
                }
                else
                {
                    ClearRamStatus = $"Chờ dọn: {Math.Ceiling(diff.TotalMinutes)}m";
                }
            });
        }
    }

    [RelayCommand]
    private void ClearRamNow()
    {
        ClearRamStatus = "Đang dọn...";
        bool success = MemoryService.Instance.ClearRam(out string msg);
        ClearRamStatus = success ? "Dọn xong!" : "Lỗi dọn RAM";
        AppendLog($"[DỌN RAM TỨC THÌ] {msg}");
    }

    // --- NHÓM HÀNH ĐỘNG NHANH ---
    [RelayCommand]
    private async Task RestartVpsAsync()
    {
        AppendLog("[HỆ THỐNG] Đang phát lệnh khởi động lại VPS...");
        await Task.Run(() =>
        {
            bool success = RestartService.Instance.RestartVps(out string error);
            Dispatcher.UIThread.Post(() =>
            {
                if (!success)
                {
                    AppendLog($"[LỖI] Lệnh restart thất bại: {error}");
                }
            });
        });
    }

    [RelayCommand]
    private async Task DeleteGameDataAsync()
    {
        AppendLog("[HỆ THỐNG] Đang dọn dẹp dữ liệu game...");
        
        string message = string.Empty;
        bool success = await Task.Run(() => GameDataService.Instance.DeleteGameData(out message));
        
        AppendLog($"[DỌN GAME] {message}");
        
        if (success)
        {
            await ShowMessageDialogAsync("Dọn Dẹp Thành Công", "Đã xóa toàn bộ thư mục J2ME '.microemulator' và các thư mục 'rms' thành công!");
        }
        else
        {
            await ShowMessageDialogAsync("Dọn Dẹp Thất Bại", $"Có lỗi xảy ra khi dọn dẹp dữ liệu game:\n{message}");
        }
    }

    private void AppendLog(string message)
    {
        ActionLogText += $"[{DateTime.Now:HH:mm:ss}] {message}\n";
    }

    // Helper tạo hộp thoại confirm
    private async Task<bool?> ShowConfirmDialogAsync(string title, string content)
    {
        var lifetime = Avalonia.Application.Current?.ApplicationLifetime as Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime;
        if (lifetime?.MainWindow is Views.MainWindow mainWin)
        {
            return await mainWin.ShowConfirmDialog(title, content);
        }
        return false;
    }

    // Helper hiển thị thông báo
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
        _resourceTimer.Dispose();
        _clearRamTimer?.Dispose();
        _clearRamStatusTimer.Dispose();
        GC.SuppressFinalize(this);
    }
}
