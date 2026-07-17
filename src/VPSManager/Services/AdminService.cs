using System;
using System.Diagnostics;
using System.Security.Principal;

namespace VPSManager.Services;

public class AdminService
{
    private static readonly Lazy<AdminService> LazyInstance = new(() => new AdminService());
    public static AdminService Instance => LazyInstance.Value;

    private AdminService() { }

    public bool IsRunningAsAdmin()
    {
        try
        {
            using var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        catch
        {
            return false;
        }
    }

    public bool RestartAsAdmin()
    {
        try
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = Environment.ProcessPath,
                UseShellExecute = true,
                Verb = "runas"
            };

            Process.Start(processInfo);
            Environment.Exit(0);
            return true;
        }
        catch (Exception ex)
        {
            Utilities.Logger.Error("Không thể restart app dưới quyền Admin", ex);
            return false;
        }
    }
}
