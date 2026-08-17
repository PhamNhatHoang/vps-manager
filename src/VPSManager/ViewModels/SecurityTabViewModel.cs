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

public partial class SecurityTabViewModel : ViewModelBase, IDisposable
{
    private readonly Timer _autoBlockTimer;

    // 1. Tự động Chặn IP
    [ObservableProperty] private bool _autoBlockEnabled = true;
    [ObservableProperty] private string _thresholdText = "10";
    [ObservableProperty] private ObservableCollection<BlockedIpItem> _blockedIps = new();
    [ObservableProperty] private string _blockedCountText = "Chưa chặn IP nào";
    [ObservableProperty] private string _manualIpToBlock = string.Empty;
    [ObservableProperty] private bool _hasBlockedIps;
    [ObservableProperty] private bool _hasNoBlockedIps = true;

    // 2. Chính Sách Khóa Tài Khoản
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AccountLockoutButtonText))]
    private bool _isAccountLockoutActive;
    
    [ObservableProperty] private string _accountLockoutStatusText = "Đang kiểm tra...";
    public string AccountLockoutButtonText => IsAccountLockoutActive ? "Tắt" : "Bật";

    // 3. Giới Hạn IP (Whitelist)
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(WhitelistStatusSummary))]
    private bool _isWhitelistActive;
    
    [ObservableProperty] private string _whitelistIpText = string.Empty;
    public string WhitelistStatusSummary => IsWhitelistActive ? "Đang bật Whitelist" : "Mở mọi IP";

    public SecurityTabViewModel()
    {
        // Tải cấu hình từ Settings
        var settings = SettingsService.Instance.Settings;
        AutoBlockEnabled = settings.AutoBlockIpEnabled;
        
        int savedThreshold = settings.MaxFailedAttemptsBeforeBlock;
        if (savedThreshold < 5 || savedThreshold > 50)
        {
            savedThreshold = 10;
        }
        ThresholdText = savedThreshold.ToString();

        IsWhitelistActive = settings.WhitelistOnlyEnabled;
        WhitelistIpText = settings.WhitelistIps;

        // Tải trạng thái ban đầu
        RefreshData();

        // Timer kiểm tra và tự động block IP mỗi 10 giây
        _autoBlockTimer = new Timer(AutoBlockTimerCallback, null, 5000, 10000);
    }

    [RelayCommand]
    public void RefreshData()
    {
        try
        {
            // 1. Danh sách IP bị chặn
            var ips = SecurityService.Instance.GetBlockedIps();
            BlockedIps.Clear();
            foreach (var ip in ips)
            {
                BlockedIps.Add(ip);
            }
            HasBlockedIps = BlockedIps.Count > 0;
            HasNoBlockedIps = BlockedIps.Count == 0;
            BlockedCountText = BlockedIps.Count > 0 ? $"Đã chặn {BlockedIps.Count} IP" : "Chưa chặn IP nào";

            // 2. Trạng thái Account Lockout
            if (SecurityService.Instance.GetAccountLockoutThreshold(out int threshold))
            {
                IsAccountLockoutActive = threshold > 0;
                AccountLockoutStatusText = threshold > 0 
                    ? $"Khóa 15 phút nếu sai {threshold} lần" 
                    : "Không giới hạn số lần sai";
            }
            else
            {
                AccountLockoutStatusText = "Không xác định";
            }
        }
        catch (Exception ex)
        {
            Logger.Error("Lỗi làm mới dữ liệu SecurityTab", ex);
        }
    }

    private int GetValidatedThreshold()
    {
        if (int.TryParse(ThresholdText, out int val))
        {
            return Math.Clamp(val, 5, 50);
        }
        return 10;
    }

    private void AutoBlockTimerCallback(object? state)
    {
        if (!AutoBlockEnabled) return;

        try
        {
            int threshold = GetValidatedThreshold();
            int newBlocked = SecurityService.Instance.CheckAndAutoBlockBruteForceIps(threshold);
            if (newBlocked > 0)
            {
                Dispatcher.UIThread.Post(() => RefreshData());
            }
        }
        catch (Exception ex)
        {
            Logger.Error("Lỗi AutoBlockTimerCallback", ex);
        }
    }

    partial void OnAutoBlockEnabledChanged(bool value)
    {
        var settings = SettingsService.Instance.Settings;
        settings.AutoBlockIpEnabled = value;
        SettingsService.Instance.SaveSettings();
    }

    partial void OnThresholdTextChanged(string value)
    {
        if (int.TryParse(value, out int val))
        {
            int clamped = Math.Clamp(val, 5, 50);
            var settings = SettingsService.Instance.Settings;
            settings.MaxFailedAttemptsBeforeBlock = clamped;
            SettingsService.Instance.SaveSettings();
        }
    }

    // --- CÁC COMMAND THAO TÁC ---

    [RelayCommand]
    private async Task ManualBlockIpAsync()
    {
        if (string.IsNullOrWhiteSpace(ManualIpToBlock))
        {
            await ShowMessageDialogAsync("Thiếu Thông Tin", "Vui lòng nhập địa chỉ IP cần chặn (Ví dụ: 14.241.12.34).");
            return;
        }

        string ip = ManualIpToBlock.Trim();
        if (!SecurityService.IsValidIpAddressOrCidr(ip))
        {
            await ShowMessageDialogAsync("IP Không Hợp Lệ", $"'{ip}' không phải là địa chỉ IP (IPv4/IPv6) hợp lệ.\n(Ví dụ hợp lệ: 14.241.12.34 hoặc 1.2.3.0/24)");
            return;
        }

        bool success = SecurityService.Instance.BlockIp(ip, "Chặn thủ công", 0, out string error);
        if (success)
        {
            ManualIpToBlock = string.Empty;
            RefreshData();
            await ShowMessageDialogAsync("Chặn Thành Công", $"Đã thêm IP '{ip}' vào danh sách chặn Firewall!");
        }
        else
        {
            await ShowMessageDialogAsync("Chặn Thất Bại", $"Không thể chặn IP '{ip}':\n{error}");
        }
    }

    [RelayCommand]
    private async Task UnblockIpAsync(BlockedIpItem? item)
    {
        if (item == null) return;

        bool success = SecurityService.Instance.UnblockIp(item.IpAddress, out string error);
        if (success)
        {
            RefreshData();
            await ShowMessageDialogAsync("Gỡ Chặn Thành Công", $"Đã xóa IP '{item.IpAddress}' khỏi danh sách chặn!");
        }
        else
        {
            await ShowMessageDialogAsync("Gỡ Chặn Thất Bại", $"Không thể gỡ chặn IP '{item.IpAddress}':\n{error}");
        }
    }

    [RelayCommand]
    private async Task UnblockAllIpsAsync()
    {
        if (BlockedIps.Count == 0) return;

        bool? confirm = await ShowConfirmDialogAsync("Xóa Toàn Bộ", "Bạn có chắc chắn muốn xóa toàn bộ IP đang bị chặn khỏi Firewall?");
        if (confirm != true) return;

        bool success = SecurityService.Instance.UnblockAllIps(out string error);
        if (success)
        {
            RefreshData();
            await ShowMessageDialogAsync("Hoàn Tất", "Đã xóa toàn bộ IP trong danh sách chặn Firewall.");
        }
        else
        {
            await ShowMessageDialogAsync("Thất Bại", $"Lỗi khi xóa danh sách chặn:\n{error}");
        }
    }

    [RelayCommand]
    private async Task ToggleAccountLockoutAsync()
    {
        bool targetState = !IsAccountLockoutActive;
        string actionText = targetState ? "BẬT khóa tài khoản (sau 5 lần sai)" : "TẮT khóa tài khoản";
        
        bool? confirm = await ShowConfirmDialogAsync("Khóa Tài Khoản", $"Bạn có chắc chắn muốn {actionText}?");
        if (confirm != true) return;

        int threshold = targetState ? 5 : 0;
        bool success = SecurityService.Instance.SetAccountLockout(threshold, 15, 15, out string error);
        if (success)
        {
            RefreshData();
            string msg = targetState 
                ? "Đã BẬT chính sách khóa tài khoản!\nTài khoản sẽ tự động khóa 15 phút nếu bị nhập sai 5 lần liên tiếp."
                : "Đã TẮT chính sách khóa tài khoản.";
            await ShowMessageDialogAsync("Thành Công", msg);
        }
        else
        {
            await ShowMessageDialogAsync("Thất Bại", $"Lỗi cập nhật:\n{error}");
        }
    }

    [RelayCommand]
    private async Task ApplyWhitelistAsync()
    {
        if (string.IsNullOrWhiteSpace(WhitelistIpText))
        {
            await ShowMessageDialogAsync("Thiếu IP", "Vui lòng nhập IP hoặc dải IP được phép kết nối (VD: 14.241.12.34 hoặc 14.241.0.0/16).");
            return;
        }

        string rawIps = WhitelistIpText.Trim();
        var ipList = rawIps.Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        foreach (var ipItem in ipList)
        {
            if (!SecurityService.IsValidIpAddressOrCidr(ipItem))
            {
                await ShowMessageDialogAsync("IP Không Hợp Lệ", $"'{ipItem}' không phải là IP hoặc dải CIDR hợp lệ.");
                return;
            }
        }

        string normalizedIps = string.Join(",", ipList);

        bool? confirm = await ShowConfirmDialogAsync("Giới Hạn IP (Whitelist)", $"Chỉ cho phép IP '{normalizedIps}' kết nối RDP?\n(Toàn bộ IP scan khác sẽ bị chặn hoàn toàn)");
        if (confirm != true) return;

        bool success = SecurityService.Instance.SetRdpIpScope(normalizedIps, out string error);
        if (success)
        {
            IsWhitelistActive = true;
            var settings = SettingsService.Instance.Settings;
            settings.WhitelistOnlyEnabled = true;
            settings.WhitelistIps = normalizedIps;
            SettingsService.Instance.SaveSettings();

            await ShowMessageDialogAsync("Thành Công", $"Đã giới hạn RDP chỉ cho phép IP '{normalizedIps}'. Mọi scan khác đã bị chặn!");
        }
        else
        {
            await ShowMessageDialogAsync("Thất Bại", $"Lỗi cấu hình Whitelist:\n{error}");
        }
    }

    [RelayCommand]
    private async Task ResetWhitelistAsync()
    {
        bool? confirm = await ShowConfirmDialogAsync("Mở Mọi IP", "Bạn có chắc chắn muốn mở cổng RDP cho tất cả các IP kết nối (any IP)?");
        if (confirm != true) return;

        bool success = SecurityService.Instance.SetRdpIpScope("any", out string error);
        if (success)
        {
            IsWhitelistActive = false;
            WhitelistIpText = string.Empty;
            var settings = SettingsService.Instance.Settings;
            settings.WhitelistOnlyEnabled = false;
            settings.WhitelistIps = string.Empty;
            SettingsService.Instance.SaveSettings();

            await ShowMessageDialogAsync("Thành Công", "Đã mở lại cổng RDP cho tất cả các IP.");
        }
        else
        {
            await ShowMessageDialogAsync("Thất Bại", $"Lỗi khi mở lại cổng:\n{error}");
        }
    }

    private async Task<bool?> ShowConfirmDialogAsync(string title, string content)
    {
        var lifetime = Avalonia.Application.Current?.ApplicationLifetime as Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime;
        if (lifetime?.MainWindow is Views.MainWindow mainWin)
        {
            return await mainWin.ShowConfirmDialog(title, content);
        }
        return false;
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
        _autoBlockTimer.Dispose();
        GC.SuppressFinalize(this);
    }
}
