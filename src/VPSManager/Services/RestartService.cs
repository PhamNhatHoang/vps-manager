using System;
using System.Diagnostics;

namespace VPSManager.Services;

public class RestartService
{
    private static readonly Lazy<RestartService> LazyInstance = new(() => new RestartService());
    public static RestartService Instance => LazyInstance.Value;

    private RestartService() { }

    public bool RestartVps(out string error)
    {
        error = string.Empty;
        if (!AdminService.Instance.IsRunningAsAdmin())
        {
            error = "Yêu cầu quyền Administrator để khởi động lại VPS.";
            return false;
        }

        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = "shutdown",
                UseShellExecute = false,
                CreateNoWindow = true
            };
            psi.ArgumentList.Add("/r");
            psi.ArgumentList.Add("/t");
            psi.ArgumentList.Add("0");

            Process.Start(psi);
            Utilities.Logger.Warn("Ứng dụng đã kích hoạt lệnh khởi động lại Windows Server (shutdown /r /t 0)");
            return true;
        }
        catch (Exception ex)
        {
            error = $"Không thể khởi chạy lệnh shutdown: {ex.Message}";
            Utilities.Logger.Error("Lỗi gọi lệnh restart VPS", ex);
            return false;
        }
    }
}
