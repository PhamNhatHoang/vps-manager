using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using VPSManager.Models;

namespace VPSManager.Services;

public class EventLogService
{
    private static readonly Lazy<EventLogService> LazyInstance = new(() => new EventLogService());
    public static EventLogService Instance => LazyInstance.Value;

    private EventLogService() 
    {
        EnableLogonAuditing();
    }

    private static void EnableLogonAuditing()
    {
        try
        {
            if (AdminService.Instance.IsRunningAsAdmin())
            {
                // Kích hoạt audit logon qua GUID (ngôn ngữ độc lập)
                var psi = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "auditpol.exe",
                    Arguments = "/set /subcategory:\"{0CCE9215-69AE-11D9-BED3-505054503030}\" /success:enable /failure:enable",
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                using var proc = System.Diagnostics.Process.Start(psi);
                proc?.WaitForExit();
                
                // Fallback bằng tên tiếng Anh nếu GUID không thành công
                if (proc == null || proc.ExitCode != 0)
                {
                    var psi2 = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "auditpol.exe",
                        Arguments = "/set /subcategory:\"Logon\" /success:enable /failure:enable",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };
                    using var proc2 = System.Diagnostics.Process.Start(psi2);
                    proc2?.WaitForExit();
                }
                Utilities.Logger.Info("Đã kích hoạt chính sách ghi nhận logon audit qua auditpol.exe");
            }
        }
        catch (Exception ex)
        {
            Utilities.Logger.Warn($"Không thể kích hoạt Audit Logon Policy: {ex.Message}");
        }
    }

    // Regex trích xuất XML hiệu năng cao, tương thích hoàn toàn với Native AOT
    private static readonly Regex UserRegex = new(@"<User>([^<]*)</User>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private static readonly Regex AddressRegex = new(@"<Address>([^<]*)</Address>", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    // Regex phục vụ đọc Security log (Event 4625)
    private static readonly Regex TargetUserRegex = new(@"<Data\s+Name=[""']TargetUserName[""']\s*>([^<]*)</Data>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private static readonly Regex IpAddressRegex = new(@"<Data\s+Name=[""']IpAddress[""']\s*>([^<]*)</Data>", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public List<LoginLogItem> GetRdpLogins(out string statusMessage, int maxCount = 200)
    {
        var logs = new List<LoginLogItem>();
        statusMessage = "Thành công";
        var warnings = new List<string>();

        try
        {
            // Nguồn 1: Quét từ LocalSessionManager/Operational (21: Đăng nhập thành công, 25: Kết nối lại phiên)
            try
            {
                string queryText = "*[System[(EventID=21 or EventID=25)]]";
                var query = new EventLogQuery("Microsoft-Windows-TerminalServices-LocalSessionManager/Operational", PathType.LogName, queryText)
                {
                    ReverseDirection = true
                };

                using var reader = new EventLogReader(query);
                EventRecord? record;

                while ((record = reader.ReadEvent()) != null && logs.Count < maxCount)
                {
                    using (record)
                    {
                        string xml = record.ToXml();
                        string rawUser = ExtractXmlValue(xml, UserRegex);
                        if (rawUser.Contains('\\'))
                        {
                            rawUser = rawUser.Split('\\')[^1];
                        }
                        
                        string ip = ExtractXmlValue(xml, AddressRegex);
                        if (string.IsNullOrEmpty(ip) || ip.Trim() == "-" || ip.ToUpper() == "LOCAL")
                        {
                            ip = "Local Console";
                        }

                        string action = "Success";

                        logs.Add(new LoginLogItem(
                            TimeCreated: record.TimeCreated ?? DateTime.Now,
                            IpAddress: ip,
                            Username: rawUser,
                            Action: action,
                            IsSuccess: true,
                            RecordId: record.RecordId ?? 0
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = $"LocalSessionManager: {ex.Message}";
                warnings.Add(msg);
                Utilities.Logger.Warn(msg);
            }

            // Nguồn 2: Quét từ Security Log (Event ID 4625 - Đăng nhập thất bại)
            try
            {
                string queryText = "*[System[(EventID=4625)]]";
                var query = new EventLogQuery("Security", PathType.LogName, queryText)
                {
                    ReverseDirection = true
                };

                using var reader = new EventLogReader(query);
                EventRecord? record;
                int countSecurity = 0;

                while ((record = reader.ReadEvent()) != null && countSecurity < maxCount)
                {
                    using (record)
                    {
                        string xml = record.ToXml();

                        string rawUser = ExtractXmlValue(xml, TargetUserRegex);
                        if (rawUser.Contains('\\'))
                        {
                            rawUser = rawUser.Split('\\')[^1];
                        }
                        // Bỏ qua tài khoản hệ thống
                        if (string.IsNullOrEmpty(rawUser) || rawUser == "-" || rawUser == "$")
                            continue;

                        string ip = ExtractXmlValue(xml, IpAddressRegex);
                        if (string.IsNullOrEmpty(ip) || ip.Trim() == "-" || ip == "127.0.0.1" || ip == "::1")
                        {
                            ip = "Không rõ";
                        }

                        logs.Add(new LoginLogItem(
                            TimeCreated: record.TimeCreated ?? DateTime.Now,
                            IpAddress: ip,
                            Username: rawUser,
                            Action: "Fail",
                            IsSuccess: false,
                            RecordId: record.RecordId ?? 0
                        ));
                        countSecurity++;
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                string msg = "Security Log: Không có quyền truy cập (cần chạy Admin)";
                warnings.Add(msg);
                Utilities.Logger.Warn(msg);
            }
            catch (Exception ex)
            {
                string msg = $"Security Log: {ex.Message}";
                warnings.Add(msg);
                Utilities.Logger.Warn(msg);
            }

            // Sắp xếp gộp toàn bộ bản ghi theo thời gian tạo mới nhất và giới hạn số lượng hiển thị
            logs = logs.OrderByDescending(x => x.TimeCreated).Take(maxCount).ToList();
            
            if (warnings.Count > 0)
            {
                statusMessage = string.Join(" | ", warnings);
            }
        }
        catch (Exception ex)
        {
            statusMessage = $"Lỗi đọc Event Log: {ex.Message}";
            Utilities.Logger.Error("Lỗi đọc Event Log tổng thể", ex);
        }

        return logs;
    }

    private static string ExtractXmlValue(string xml, Regex regex)
    {
        var match = regex.Match(xml);
        return match.Success ? match.Groups[1].Value.Trim() : string.Empty;
    }
}
