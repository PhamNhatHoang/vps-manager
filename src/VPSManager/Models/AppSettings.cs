using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VPSManager.Models;

public class AppSettings
{
    public bool TelegramEnabled { get; set; }
    public string TelegramChatId { get; set; } = string.Empty;
    public bool ClearRamEnabled { get; set; }
    public double ClearRamIntervalMinutes { get; set; } = 15;
    public long LastProcessedEventRecordId { get; set; }
    public long LastProcessedSuccessRecordId { get; set; }
    public long LastProcessedFailureRecordId { get; set; }
}

// Cấu hình source generator cho System.Text.Json giúp chạy mượt mà trên Native AOT
[JsonSerializable(typeof(AppSettings))]
[JsonSerializable(typeof(List<ToolDownloadItem>))]
[JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}
// Để sử dụng trong code, ta dùng:
// AppJsonSerializerContext.Default.AppSettings
// AppJsonSerializerContext.Default.ListToolDownloadItem
