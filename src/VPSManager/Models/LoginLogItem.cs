using System;

namespace VPSManager.Models;

public record LoginLogItem(
    DateTime TimeCreated,
    string IpAddress,
    string Username,
    string Action,
    bool IsSuccess,
    long RecordId
)
{
    public string StatusColor => IsSuccess ? "#23A55A" : (Action.Contains("màn hình") ? "#3B82F6" : "#F23F43");
}
