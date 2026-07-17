namespace VPSManager.Models;

public record VpsInfo(
    string OsName,
    int CpuCores,
    string TotalRam,
    double CpuUsagePercentage,
    string RamUsageText,
    double RamUsagePercentage,
    int RdpPort
);
