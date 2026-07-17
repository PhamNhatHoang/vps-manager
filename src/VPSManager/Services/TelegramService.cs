using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using VPSManager.Config;

namespace VPSManager.Services;

public class TelegramService
{
    private static readonly Lazy<TelegramService> LazyInstance = new(() => new TelegramService());
    public static TelegramService Instance => LazyInstance.Value;

    private readonly HttpClient _httpClient;

    private string _cachedVpsIp = string.Empty;

    private TelegramService()
    {
        _httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(10)
        };
    }

    public async Task<string> GetVpsPublicIpAsync(CancellationToken cancellationToken = default)
    {
        if (!string.IsNullOrEmpty(_cachedVpsIp))
        {
            return _cachedVpsIp;
        }

        try
        {
            using var response = await _httpClient.GetAsync("https://api.ipify.org", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                _cachedVpsIp = (await response.Content.ReadAsStringAsync(cancellationToken)).Trim();
                return _cachedVpsIp;
            }
        }
        catch
        {
            // Bỏ qua thử lại bằng fallback
        }

        try
        {
            using var response = await _httpClient.GetAsync("https://icanhazip.com", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                _cachedVpsIp = (await response.Content.ReadAsStringAsync(cancellationToken)).Trim();
                return _cachedVpsIp;
            }
        }
        catch
        {
            // Bỏ qua
        }

        return Environment.MachineName;
    }

    public async Task<bool> SendAlertAsync(string chatId, string action, string username, string ip, DateTime time, bool isSuccess, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(chatId))
        {
            return false;
        }

        string vpsIp = await GetVpsPublicIpAsync(cancellationToken);
        
        string statusText;
        if (isSuccess)
        {
            string cleanAction = action.Replace("(", "- ").Replace(")", "");
            statusText = $"Thành công `({cleanAction})`";
        }
        else
        {
            statusText = "Thất bại `(Fail - 4625)`";
        }

        string header = isSuccess ? "🟢 *ĐĂNG NHẬP THÀNH CÔNG*" : "🔴 *CẢNH BÁO ĐĂNG NHẬP THẤT BẠI*";
        
        string message = $"{header}\n" +
                         $"🌐 IP VPS: `{vpsIp}`\n" +
                         "━━━━━━━━━━━━━━━━━━\n" +
                         $"👤 IP đăng nhập: `{ip}`\n" +
                         $"📌 Trạng thái: {statusText}\n" +
                         $"🕒 Thời gian: `{time:yyyy-MM-dd HH:mm:ss}`\n" +
                         "━━━━━━━━━━━━━━━━━━\n" +
                         "Nếu Quý khách không phải người thực hiện\n" +
                         "Vui lòng kiểm tra lại VPS để đảm bảo an toàn.\n" +
                         "━━━━━━━━━━━━━━━━━━\n" +
                         "cloudvpsviet.com - Cảm ơn Quý khách đã sử dụng dịch vụ.";

        return await SendMessageRawAsync(CentralBotConfig.DefaultBotToken, chatId, message, cancellationToken);
    }

    public async Task<bool> SendTestMessageAsync(string chatId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(chatId))
        {
            return false;
        }

        string vpsIp = await GetVpsPublicIpAsync(cancellationToken);
        string message = "🔵 *THÔNG BÁO TEST KẾT NỐI*\n" +
                         $"🌐 IP VPS: `{vpsIp}`\n" +
                         "━━━━━━━━━━━━━━━━━━\n" +
                         "cloudvpsviet.com - Cảm ơn Quý khách đã sử dụng dịch vụ.";

        return await SendMessageRawAsync(CentralBotConfig.DefaultBotToken, chatId, message, cancellationToken);
    }

    private async Task<bool> SendMessageRawAsync(string botToken, string chatId, string message, CancellationToken cancellationToken)
    {
        try
        {
            string url = $"https://api.telegram.org/bot{botToken}/sendMessage";
            
            // Dùng FormUrlEncodedContent để tránh JSON Reflection hoàn toàn, an toàn cho Native AOT
            var postData = new Dictionary<string, string>
            {
                { "chat_id", chatId },
                { "text", message },
                { "parse_mode", "Markdown" }
            };

            using var content = new FormUrlEncodedContent(postData);
            using var response = await _httpClient.PostAsync(url, content, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            
            string errorText = await response.Content.ReadAsStringAsync(cancellationToken);
            Utilities.Logger.Warn($"Gửi Telegram thất bại. Status: {response.StatusCode}, Chi tiết: {errorText}");
            return false;
        }
        catch (Exception ex)
        {
            Utilities.Logger.Error("Lỗi kết nối khi gửi Telegram", ex);
            return false;
        }
    }
}
