using System;
using System.Runtime.InteropServices;
using VPSManager.Models;

namespace VPSManager.Services;

public partial class SystemInfoService
{
    private static readonly Lazy<SystemInfoService> LazyInstance = new(() => new SystemInfoService());
    public static SystemInfoService Instance => LazyInstance.Value;

    [StructLayout(LayoutKind.Sequential)]
    private struct FILETIME
    {
        public uint dwLowDateTime;
        public uint dwHighDateTime;

        public readonly ulong ToUlong()
        {
            return ((ulong)dwHighDateTime << 32) | dwLowDateTime;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct MEMORYSTATUSEX
    {
        public uint dwLength;
        public uint dwMemoryLoad;
        public ulong ullTotalPhys;
        public ulong ullAvailPhys;
        public ulong ullTotalPageFile;
        public ulong ullAvailPageFile;
        public ulong ullTotalVirtual;
        public ulong ullAvailVirtual;
        public ulong ullAvailExtendedVirtual;

        public MEMORYSTATUSEX()
        {
            dwLength = (uint)Marshal.SizeOf<MEMORYSTATUSEX>();
        }
    }

    [LibraryImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool GetSystemTimes(
        out FILETIME lpIdleTime,
        out FILETIME lpKernelTime,
        out FILETIME lpUserTime
    );

    [LibraryImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool GlobalMemoryStatusEx(ref MEMORYSTATUSEX lpBuffer);

    private readonly object _syncLock = new();
    private FILETIME _prevIdleTime;
    private FILETIME _prevKernelTime;
    private FILETIME _prevUserTime;
    private bool _hasPrevTimes;
    private double _lastCpuUsage;

    private SystemInfoService()
    {
        // Khởi tạo các giá trị thời gian ban đầu
        lock (_syncLock)
        {
            if (GetSystemTimes(out _prevIdleTime, out _prevKernelTime, out _prevUserTime))
            {
                _hasPrevTimes = true;
            }
        }
    }

    public VpsInfo GetCurrentVpsInfo()
    {
        double cpuUsage = _lastCpuUsage;
        lock (_syncLock)
        {
            if (GetSystemTimes(out var idleTime, out var kernelTime, out var userTime))
            {
                if (_hasPrevTimes)
                {
                    ulong prevIdle = _prevIdleTime.ToUlong();
                    ulong prevKernel = _prevKernelTime.ToUlong();
                    ulong prevUser = _prevUserTime.ToUlong();

                    ulong currIdle = idleTime.ToUlong();
                    ulong currKernel = kernelTime.ToUlong();
                    ulong currUser = userTime.ToUlong();

                    if (currKernel >= prevKernel && currUser >= prevUser && currIdle >= prevIdle)
                    {
                        ulong kernelDelta = currKernel - prevKernel;
                        ulong userDelta = currUser - prevUser;
                        ulong idleDelta = currIdle - prevIdle;
                        ulong totalDelta = kernelDelta + userDelta;

                        if (totalDelta > 0)
                        {
                            if (idleDelta <= totalDelta)
                            {
                                ulong activeDelta = totalDelta - idleDelta;
                                cpuUsage = Math.Clamp((double)activeDelta * 100.0 / totalDelta, 0.0, 100.0);
                                _lastCpuUsage = cpuUsage;
                            }
                        }
                    }
                }

                _prevIdleTime = idleTime;
                _prevKernelTime = kernelTime;
                _prevUserTime = userTime;
                _hasPrevTimes = true;
            }
        }

        // Lấy thông tin RAM
        var memStatus = new MEMORYSTATUSEX();
        double ramUsagePercent = 0;
        string ramUsageText = "Không xác định";
        string totalRamText = "Không xác định";

        if (GlobalMemoryStatusEx(ref memStatus))
        {
            double totalGb = memStatus.ullTotalPhys / (1024.0 * 1024.0 * 1024.0);
            double availGb = memStatus.ullAvailPhys / (1024.0 * 1024.0 * 1024.0);
            double usedGb = totalGb - availGb;

            ramUsagePercent = memStatus.dwMemoryLoad;
            ramUsageText = $"{usedGb:F2} GB / {totalGb:F2} GB";
            totalRamText = $"{totalGb:F2} GB";
        }

        // Lấy Port RDP
        int rdpPort = RdpService.Instance.GetCurrentRdpPort();

        // Lấy thông tin OS và Core CPU
        string osName = RuntimeInformation.OSDescription;
        int cpuCores = Environment.ProcessorCount;

        return new VpsInfo(
            OsName: osName,
            CpuCores: cpuCores,
            TotalRam: totalRamText,
            CpuUsagePercentage: Math.Round(cpuUsage, 1),
            RamUsageText: ramUsageText,
            RamUsagePercentage: ramUsagePercent,
            RdpPort: rdpPort
        );
    }
}
