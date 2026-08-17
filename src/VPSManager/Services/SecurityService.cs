using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using VPSManager.Models;
using VPSManager.Utilities;

namespace VPSManager.Services;

public class SecurityService
{
    private static readonly Lazy<SecurityService> LazyInstance = new(() => new SecurityService());
    public static SecurityService Instance => LazyInstance.Value;

    private readonly List<BlockedIpItem> _blockedIps = new();
    private readonly object _lock = new();
    private static readonly string BlockedIpsCacheFile = Path.Combine(AppContext.BaseDirectory, "blocked_ips.json");

    private SecurityService()
    {
        LoadBlockedIpsCache();
        EnsureRdpCompatibility();
    }

    public void EnsureRdpCompatibility()
    {
        try
        {
            if (AdminService.Instance.IsRunningAsAdmin())
            {
                using var key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Terminal Server\WinStations\RDP-Tcp", true);
                if (key != null)
                {
                    key.SetValue("UserAuthentication", 0, RegistryValueKind.DWord);
                    key.SetValue("SecurityLayer", 0, RegistryValueKind.DWord);
                }

                using var tsKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Terminal Server", true);
                tsKey?.SetValue("fDenyTSConnections", 0, RegistryValueKind.DWord);

                using var credSspKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\CredSSP\Parameters", true);
                credSspKey?.SetValue("AllowEncryptionOracle", 2, RegistryValueKind.DWord);
            }
        }
        catch (Exception ex)
        {
            Logger.Warn($"Không thể cấu hình tự động RDP compatibility: {ex.Message}");
        }
    }

    // --- 1. QUẢN LÝ TỰ ĐỘNG CHẶN IP & FIREWALL ---

    public List<BlockedIpItem> GetBlockedIps()
    {
        lock (_lock)
        {
            return _blockedIps.OrderByDescending(x => x.BlockedAt).ToList();
        }
    }

    public bool BlockIp(string ip, string reason, out string error)
    {
        return BlockIp(ip, reason, 0, out error);
    }

    public bool BlockIp(string ip, string reason, int failedAttempts, out string error)
    {
        error = string.Empty;
        if (string.IsNullOrWhiteSpace(ip))
        {
            error = "Địa chỉ IP không hợp lệ.";
            return false;
        }

        ip = ip.Trim();

        if (!IsValidIpAddressOrCidr(ip))
        {
            error = $"'{ip}' không phải là địa chỉ IP (IPv4/IPv6) hợp lệ (Ví dụ hợp lệ: 14.241.12.34 hoặc 1.2.3.0/24).";
            return false;
        }

        if (ip == "127.0.0.1" || ip == "::1" || ip.Equals("Local Console", StringComparison.OrdinalIgnoreCase) || ip.Equals("Không rõ", StringComparison.OrdinalIgnoreCase))
        {
            error = "Không thể chặn IP cục bộ hoặc IP không xác định.";
            return false;
        }

        if (!AdminService.Instance.IsRunningAsAdmin())
        {
            error = "Yêu cầu quyền Administrator để cấu hình Firewall.";
            return false;
        }

        try
        {
            // Thêm rule Firewall chặn IP
            string ruleName = $"VPSManager_Block_{ip.Replace(':', '_').Replace('/', '_')}";
            var psi = new ProcessStartInfo
            {
                FileName = "netsh",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            psi.ArgumentList.Add("advfirewall");
            psi.ArgumentList.Add("firewall");
            psi.ArgumentList.Add("add");
            psi.ArgumentList.Add("rule");
            psi.ArgumentList.Add($"name={ruleName}");
            psi.ArgumentList.Add("dir=in");
            psi.ArgumentList.Add("action=block");
            psi.ArgumentList.Add($"remoteip={ip}");

            using (var proc = Process.Start(psi))
            {
                string stdErr = proc?.StandardError.ReadToEnd() ?? string.Empty;
                string stdOut = proc?.StandardOutput.ReadToEnd() ?? string.Empty;
                proc?.WaitForExit();

                if (proc != null && proc.ExitCode != 0)
                {
                    error = !string.IsNullOrWhiteSpace(stdErr) ? stdErr.Trim() : (!string.IsNullOrWhiteSpace(stdOut) ? stdOut.Trim() : "Không thể thêm rule vào Windows Firewall.");
                    return false;
                }
            }

            lock (_lock)
            {
                _blockedIps.RemoveAll(x => x.IpAddress.Equals(ip, StringComparison.OrdinalIgnoreCase));
                _blockedIps.Add(new BlockedIpItem(
                    IpAddress: ip,
                    BlockedAt: DateTime.Now,
                    FailedAttempts: failedAttempts,
                    Reason: reason
                ));
                SaveBlockedIpsCache();
            }

            Logger.Info($"[BẢO MẬT] Đã chặn IP: {ip} - Lý do: {reason}");
            return true;
        }
        catch (Exception ex)
        {
            error = ex.Message;
            Logger.Error($"Lỗi khi chặn IP {ip}", ex);
            return false;
        }
    }

    public bool UnblockIp(string ip, out string error)
    {
        error = string.Empty;
        if (string.IsNullOrWhiteSpace(ip))
        {
            error = "Địa chỉ IP không hợp lệ.";
            return false;
        }

        ip = ip.Trim();
        if (!AdminService.Instance.IsRunningAsAdmin())
        {
            error = "Yêu cầu quyền Administrator để cấu hình Firewall.";
            return false;
        }

        try
        {
            string ruleName = $"VPSManager_Block_{ip.Replace(':', '_')}";
            var psi = new ProcessStartInfo
            {
                FileName = "netsh",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true
            };
            psi.ArgumentList.Add("advfirewall");
            psi.ArgumentList.Add("firewall");
            psi.ArgumentList.Add("delete");
            psi.ArgumentList.Add("rule");
            psi.ArgumentList.Add($"name={ruleName}");

            using (var proc = Process.Start(psi))
            {
                proc?.WaitForExit();
            }

            lock (_lock)
            {
                _blockedIps.RemoveAll(x => x.IpAddress.Equals(ip, StringComparison.OrdinalIgnoreCase));
                SaveBlockedIpsCache();
            }

            Logger.Info($"[BẢO MẬT] Đã mở chặn IP: {ip}");
            return true;
        }
        catch (Exception ex)
        {
            error = ex.Message;
            Logger.Error($"Lỗi khi mở chặn IP {ip}", ex);
            return false;
        }
    }

    public bool UnblockAllIps(out string error)
    {
        error = string.Empty;
        if (!AdminService.Instance.IsRunningAsAdmin())
        {
            error = "Yêu cầu quyền Administrator.";
            return false;
        }

        try
        {
            List<BlockedIpItem> list;
            lock (_lock)
            {
                list = _blockedIps.ToList();
            }

            foreach (var item in list)
            {
                UnblockIp(item.IpAddress, out _);
            }

            lock (_lock)
            {
                _blockedIps.Clear();
                SaveBlockedIpsCache();
            }

            Logger.Info("[BẢO MẬT] Đã xóa toàn bộ IP trong danh sách chặn.");
            return true;
        }
        catch (Exception ex)
        {
            error = ex.Message;
            return false;
        }
    }

    public int CheckAndAutoBlockBruteForceIps(int threshold)
    {
        if (threshold <= 0) threshold = 5;
        int blockedCount = 0;

        try
        {
            var logs = EventLogService.Instance.GetRdpLogins(out _, 100);
            var recentFails = logs
                .Where(x => !x.IsSuccess && 
                            !string.IsNullOrWhiteSpace(x.IpAddress) && 
                            x.IpAddress != "127.0.0.1" && 
                            x.IpAddress != "::1" && 
                            !x.IpAddress.Equals("Local Console", StringComparison.OrdinalIgnoreCase) && 
                            !x.IpAddress.Equals("Không rõ", StringComparison.OrdinalIgnoreCase) &&
                            x.TimeCreated >= DateTime.Now.AddMinutes(-30))
                .GroupBy(x => x.IpAddress)
                .Select(g => new { Ip = g.Key, Count = g.Count() })
                .Where(x => x.Count >= threshold)
                .ToList();

            foreach (var fail in recentFails)
            {
                bool alreadyBlocked;
                lock (_lock)
                {
                    alreadyBlocked = _blockedIps.Any(x => x.IpAddress.Equals(fail.Ip, StringComparison.OrdinalIgnoreCase));
                }

                if (!alreadyBlocked)
                {
                    if (BlockIp(fail.Ip, $"Tự động chặn do đăng nhập sai {fail.Count} lần trong 30 phút", fail.Count, out string err))
                    {
                        blockedCount++;
                        // Bắn thông báo qua Telegram nếu bật
                        var settings = SettingsService.Instance.Settings;
                        if (settings.TelegramEnabled && !string.IsNullOrWhiteSpace(settings.TelegramChatId))
                        {
                            _ = TelegramService.Instance.SendAlertAsync(
                                settings.TelegramChatId, 
                                $"[TỰ ĐỘNG CHẶN] Phát hiện dò mật khẩu ({fail.Count} lần thất bại)", 
                                "Kẻ tấn công", 
                                fail.Ip, 
                                DateTime.Now, 
                                false
                            );
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Error("Lỗi trong quá trình kiểm tra Auto Block IP", ex);
        }

        return blockedCount;
    }

    // --- 2. CHÍNH SÁCH KHÓA TÀI KHOẢN (ACCOUNT LOCKOUT POLICY) ---

    public bool GetAccountLockoutThreshold(out int threshold)
    {
        threshold = 0;
        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = "net",
                Arguments = "accounts",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true
            };

            using var proc = Process.Start(psi);
            if (proc != null)
            {
                string output = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();

                // Tìm dòng "Lockout threshold" hoặc "Ngưỡng khóa"
                var match = Regex.Match(output, @"(?:Lockout threshold|Ngưỡng khóa|lockout threshold)\D+(\d+)", RegexOptions.IgnoreCase);
                if (match.Success && int.TryParse(match.Groups[1].Value, out int val))
                {
                    threshold = val;
                    return true;
                }
                if (output.Contains("Never", StringComparison.OrdinalIgnoreCase) || output.Contains("Không bao giờ", StringComparison.OrdinalIgnoreCase))
                {
                    threshold = 0;
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Error("Không thể lấy trạng thái Account Lockout", ex);
        }
        return false;
    }

    public bool SetAccountLockout(int threshold, int durationMinutes, int windowMinutes, out string error)
    {
        error = string.Empty;
        if (!AdminService.Instance.IsRunningAsAdmin())
        {
            error = "Yêu cầu quyền Administrator.";
            return false;
        }

        try
        {
            string args = threshold > 0 
                ? $"accounts /lockoutthreshold:{threshold} /lockoutduration:{durationMinutes} /lockoutwindow:{windowMinutes}"
                : "accounts /lockoutthreshold:0";

            var psi = new ProcessStartInfo
            {
                FileName = "net",
                Arguments = args,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true
            };

            using var proc = Process.Start(psi);
            proc?.WaitForExit();

            if (proc != null && proc.ExitCode == 0)
            {
                Logger.Info($"[BẢO MẬT] Đã cập nhật Account Lockout Policy (Threshold: {threshold})");
                return true;
            }

            string outMsg = proc?.StandardOutput.ReadToEnd() ?? string.Empty;
            error = string.IsNullOrWhiteSpace(outMsg) ? "Cập nhật chính sách khóa tài khoản thất bại." : outMsg.Trim();
            return false;
        }
        catch (Exception ex)
        {
            error = ex.Message;
            Logger.Error("Lỗi thiết lập Account Lockout Policy", ex);
            return false;
        }
    }

    // --- 3. GIỚI HẠN IP KẾT NỐI RDP (IP WHITELIST) ---

    public bool SetRdpIpScope(string allowedIps, out string error)
    {
        error = string.Empty;
        if (!AdminService.Instance.IsRunningAsAdmin())
        {
            error = "Yêu cầu quyền Administrator.";
            return false;
        }

        try
        {
            string scope = string.IsNullOrWhiteSpace(allowedIps) ? "any" : allowedIps.Trim();

            var psi = new ProcessStartInfo
            {
                FileName = "netsh",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true
            };
            psi.ArgumentList.Add("advfirewall");
            psi.ArgumentList.Add("firewall");
            psi.ArgumentList.Add("set");
            psi.ArgumentList.Add("rule");
            psi.ArgumentList.Add("name=VPSManager_RDP");
            psi.ArgumentList.Add($"new");
            psi.ArgumentList.Add($"remoteip={scope}");

            using var proc = Process.Start(psi);
            proc?.WaitForExit();

            if (proc != null && proc.ExitCode == 0)
            {
                Logger.Info($"[BẢO MẬT] Đã cập nhật giới hạn IP RDP: {scope}");
                return true;
            }

            error = "Không thể cập nhật cấu hình Remote IP trên Firewall rule VPSManager_RDP.";
            return false;
        }
        catch (Exception ex)
        {
            error = ex.Message;
            Logger.Error("Lỗi cập nhật IP Whitelist", ex);
            return false;
        }
    }

    // --- 4. BẮT BUỘC XÁC THỰC NLA (NETWORK LEVEL AUTHENTICATION) ---

    public bool IsNlaEnabled()
    {
        try
        {
            using var key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Terminal Server\WinStations\RDP-Tcp");
            if (key != null)
            {
                var val = key.GetValue("UserAuthentication");
                if (val is int intVal)
                {
                    return intVal == 1;
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Error("Không thể đọc trạng thái NLA từ Registry", ex);
        }
        return false;
    }

    public bool SetNlaEnabled(bool enable, out string error)
    {
        error = string.Empty;
        if (!AdminService.Instance.IsRunningAsAdmin())
        {
            error = "Yêu cầu quyền Administrator.";
            return false;
        }

        try
        {
            using (var key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Terminal Server\WinStations\RDP-Tcp", true))
            {
                if (key != null)
                {
                    key.SetValue("UserAuthentication", enable ? 1 : 0, RegistryValueKind.DWord);
                    key.SetValue("SecurityLayer", enable ? 1 : 0, RegistryValueKind.DWord);
                }
            }

            using (var tsKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Terminal Server", true))
            {
                tsKey?.SetValue("fDenyTSConnections", 0, RegistryValueKind.DWord);
            }

            // Sửa lỗi CredSSP Encryption Oracle Remediation
            try
            {
                using var credSspKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\CredSSP\Parameters", true);
                credSspKey?.SetValue("AllowEncryptionOracle", 2, RegistryValueKind.DWord);
            }
            catch (Exception ex)
            {
                Logger.Warn($"Không thể ghi key CredSSP Parameters: {ex.Message}");
            }

            Logger.Info($"[BẢO MẬT] Đã {(enable ? "Bật" : "Tắt")} bắt buộc xác thực NLA (SecurityLayer: {(enable ? 1 : 0)}).");
            return true;
        }
        catch (Exception ex)
        {
            error = ex.Message;
            Logger.Error("Lỗi cấu hình NLA", ex);
            return false;
        }
    }

    public bool FixRdpConnectionIssues(out string error)
    {
        return SetNlaEnabled(false, out error);
    }

    // --- LOCAL CACHE PERSISTENCE ---

    private void LoadBlockedIpsCache()
    {
        try
        {
            if (File.Exists(BlockedIpsCacheFile))
            {
                string json = File.ReadAllText(BlockedIpsCacheFile);
                var items = JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.ListBlockedIpItem);
                if (items != null)
                {
                    lock (_lock)
                    {
                        _blockedIps.Clear();
                        _blockedIps.AddRange(items);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Warn($"Không thể đọc cache blocked_ips.json: {ex.Message}");
        }
    }

    private void SaveBlockedIpsCache()
    {
        try
        {
            List<BlockedIpItem> list;
            lock (_lock)
            {
                list = _blockedIps.ToList();
            }
            string json = JsonSerializer.Serialize(list, AppJsonSerializerContext.Default.ListBlockedIpItem);
            File.WriteAllText(BlockedIpsCacheFile, json);
        }
        catch (Exception ex)
        {
            Logger.Warn($"Không thể lưu cache blocked_ips.json: {ex.Message}");
        }
    }

    public static bool IsValidIpAddressOrCidr(string ipStr)
    {
        if (string.IsNullOrWhiteSpace(ipStr)) return false;

        ipStr = ipStr.Trim();

        // Kiểm tra định dạng dải CIDR (ví dụ 14.241.0.0/16)
        if (ipStr.Contains('/'))
        {
            var parts = ipStr.Split('/');
            if (parts.Length == 2 && 
                System.Net.IPAddress.TryParse(parts[0], out _) && 
                int.TryParse(parts[1], out int prefix) && 
                prefix >= 0 && prefix <= 128)
            {
                return true;
            }
            return false;
        }

        // Kiểm tra IP đơn lẻ (IPv4 hoặc IPv6)
        return System.Net.IPAddress.TryParse(ipStr, out _);
    }
}
