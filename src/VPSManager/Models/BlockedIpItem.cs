using System;

namespace VPSManager.Models;

public record BlockedIpItem(
    string IpAddress,
    DateTime BlockedAt,
    int FailedAttempts,
    string Reason
);
