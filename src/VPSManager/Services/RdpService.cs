using System;
using System.Diagnostics;
using Microsoft.Win32;

namespace VPSManager.Services;

public class RdpService
{
    private static readonly Lazy<RdpService> LazyInstance = new(() => new RdpService());
    public static RdpService Instance => LazyInstance.Value;

    private RdpService() { }

    public int GetCurrentRdpPort()
    {
        try
        {
            using var key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Terminal Server\WinStations\RDP-Tcp");
            if (key != null)
            {
                var value = key.GetValue("PortNumber");
                if (value is int port)
                {
                    return port;
                }
            }
        }
        catch (Exception ex)
        {
            Utilities.Logger.Error("Không thể đọc Port RDP từ Registry", ex);
        }
        return 3389; // Mặc định RDP Port
    }

    public bool SetRdpPort(int newPort, out string error)
    {
        error = string.Empty;
        if (!AdminService.Instance.IsRunningAsAdmin())
        {
            error = "Yêu cầu quyền Administrator để thực hiện thao tác này.";
            return false;
        }

        try
        {
            using (var key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Terminal Server\WinStations\RDP-Tcp", true))
            {
                if (key == null)
                {
                    error = "Không tìm thấy khóa Registry cấu hình RDP.";
                    return false;
                }
                key.SetValue("PortNumber", newPort, RegistryValueKind.DWord);
            }

            // Tự động cấu hình Firewall cho port mới
            if (UpdateFirewallRule(newPort, out string fwError))
            {
                Utilities.Logger.Info($"Đã đổi cổng RDP sang {newPort} thành công và thiết lập Firewall.");
                return true;
            }
            else
            {
                error = $"Ghi Registry thành công nhưng cấu hình Firewall lỗi: {fwError}";
                return false;
            }
        }
        catch (Exception ex)
        {
            error = $"Lỗi thay đổi cổng RDP: {ex.Message}";
            Utilities.Logger.Error("Lỗi đổi RDP Port", ex);
            return false;
        }
    }

    private bool UpdateFirewallRule(int port, out string error)
    {
        error = string.Empty;
        try
        {
            // Xóa rule cũ nếu có
            var deletePsi = new ProcessStartInfo
            {
                FileName = "netsh",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true
            };
            deletePsi.ArgumentList.Add("advfirewall");
            deletePsi.ArgumentList.Add("firewall");
            deletePsi.ArgumentList.Add("delete");
            deletePsi.ArgumentList.Add("rule");
            deletePsi.ArgumentList.Add("name=VPSManager_RDP");

            using (var pDelete = Process.Start(deletePsi))
            {
                pDelete?.WaitForExit();
            }

            // Thêm rule mới cho TCP
            var addTcpPsi = new ProcessStartInfo
            {
                FileName = "netsh",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true
            };
            addTcpPsi.ArgumentList.Add("advfirewall");
            addTcpPsi.ArgumentList.Add("firewall");
            addTcpPsi.ArgumentList.Add("add");
            addTcpPsi.ArgumentList.Add("rule");
            addTcpPsi.ArgumentList.Add("name=VPSManager_RDP");
            addTcpPsi.ArgumentList.Add("dir=in");
            addTcpPsi.ArgumentList.Add("action=allow");
            addTcpPsi.ArgumentList.Add("protocol=TCP");
            addTcpPsi.ArgumentList.Add($"localport={port}");

            using (var pAddTcp = Process.Start(addTcpPsi))
            {
                pAddTcp?.WaitForExit();
                if (pAddTcp?.ExitCode != 0)
                {
                    error = "Không thể thêm rule Firewall mới cho cổng TCP.";
                    return false;
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            error = ex.Message;
            Utilities.Logger.Error("Lỗi cấu hình Windows Firewall", ex);
            return false;
        }
    }
}
