using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace VPSManager.Services;

public partial class MemoryService
{
    private static readonly Lazy<MemoryService> LazyInstance = new(() => new MemoryService());
    public static MemoryService Instance => LazyInstance.Value;

    [LibraryImport("psapi.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool EmptyWorkingSet(IntPtr hProcess);

    private MemoryService() { }

    public bool ClearRam(out string statusMsg)
    {
        int successCount = 0;
        int failCount = 0;
        long totalMemoryBefore = GetTotalMemoryInUseBytes();

        try
        {
            // Tối ưu chính tiến trình hiện tại trước
            try
            {
                using var currentProc = Process.GetCurrentProcess();
                if (EmptyWorkingSet(currentProc.Handle))
                {
                    successCount++;
                }
            }
            catch { }

            // Lấy toàn bộ danh sách các tiến trình hệ thống đang chạy
            var processes = Process.GetProcesses();
            foreach (var proc in processes)
            {
                try
                {
                    // Bỏ qua Idle (0) và System (4)
                    if (proc.Id == 0 || proc.Id == 4)
                        continue;

                    // Thử gọi EmptyWorkingSet giải phóng RAM tiến trình
                    if (EmptyWorkingSet(proc.Handle))
                    {
                        successCount++;
                    }
                    else
                    {
                        failCount++;
                    }
                }
                catch
                {
                    failCount++;
                }
                finally
                {
                    proc.Dispose();
                }
            }

            // Gọi ép dọn rác bộ nhớ .NET
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            long totalMemoryAfter = GetTotalMemoryInUseBytes();
            double freedMb = (totalMemoryBefore - totalMemoryAfter) / (1024.0 * 1024.0);
            
            if (freedMb < 0) freedMb = 0;

            statusMsg = $"Thành công giải phóng {successCount} tiến trình. Tiết kiệm ~{freedMb:F1} MB RAM.";
            Utilities.Logger.Info(statusMsg);
            return true;
        }
        catch (Exception ex)
        {
            statusMsg = $"Lỗi dọn RAM: {ex.Message}";
            Utilities.Logger.Error("Lỗi dọn dẹp RAM hệ thống", ex);
            return false;
        }
    }

    private static long GetTotalMemoryInUseBytes()
    {
        try
        {
            using var pc = Process.GetCurrentProcess();
            return GC.GetTotalMemory(false);
        }
        catch
        {
            return 0;
        }
    }
}
