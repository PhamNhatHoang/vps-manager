using System;
using System.Runtime.InteropServices;
using System.Threading;
using Avalonia;

namespace VPSManager;

sealed partial class Program
{
    private const string AppMutexName = @"Local\VPSManager_SingleInstance_Mutex_98a72b";

    [LibraryImport("user32.dll", EntryPoint = "FindWindowW", StringMarshalling = StringMarshalling.Utf16)]
    private static partial IntPtr FindWindow(string? lpClassName, string? lpWindowName);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool SetForegroundWindow(IntPtr hWnd);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool ShowWindow(IntPtr hWnd, int nCmdShow);

    private const int SW_RESTORE = 9;

    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called.
    [STAThread]
    public static void Main(string[] args)
    {
        Mutex? mutex = null;
        bool hasHandle = false;

        try
        {
            mutex = new Mutex(true, AppMutexName, out bool createdNew);
            hasHandle = createdNew;

            // Nếu vừa khởi động lại (Restart As Admin), đợi 1.5s để tiến trình cũ đóng hoàn toàn
            if (!hasHandle)
            {
                try
                {
                    hasHandle = mutex.WaitOne(TimeSpan.FromMilliseconds(1500), false);
                }
                catch (AbandonedMutexException)
                {
                    // Tiến trình cũ bị đóng mà chưa kịp release Mutex -> Cho phép tiếp quản
                    hasHandle = true;
                }
            }

            if (!hasHandle)
            {
                // Đã có 1 bản VPS Manager đang chạy -> Tìm và đưa cửa sổ hiện tại lên trước
                IntPtr existingHwnd = FindWindow(null, "VPS Manager");
                if (existingHwnd != IntPtr.Zero)
                {
                    ShowWindow(existingHwnd, SW_RESTORE);
                    SetForegroundWindow(existingHwnd);
                }
                return;
            }

            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
        }
        catch (Exception ex)
        {
            // Trường hợp lỗi cấp phép Mutex bất thường -> Vẫn tiếp tục mở app bình thường
            Utilities.Logger.Error("Lỗi SingleInstance Mutex", ex);
            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
        }
        finally
        {
            if (hasHandle && mutex != null)
            {
                try
                {
                    mutex.ReleaseMutex();
                }
                catch { }
                mutex.Dispose();
            }
        }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
#if DEBUG
            .WithDeveloperTools()
#endif
            .WithInterFont()
            .LogToTrace();
}
