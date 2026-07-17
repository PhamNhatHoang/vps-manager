using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using VPSManager.Models;

namespace VPSManager.Services;

public class SettingsService
{
    private static readonly Lazy<SettingsService> LazyInstance = new(() => new SettingsService());
    public static SettingsService Instance => LazyInstance.Value;

    private readonly string _settingsFolder;
    private readonly string _settingsPath;
    private AppSettings _currentSettings;

    public AppSettings Settings => _currentSettings;

    private SettingsService()
    {
        string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        _settingsFolder = Path.Combine(appData, "VPSManager");
        _settingsPath = Path.Combine(_settingsFolder, "appsettings.json");
        _currentSettings = new AppSettings();
        LoadSettings();
    }

    public void LoadSettings()
    {
        try
        {
            if (!Directory.Exists(_settingsFolder))
            {
                Directory.CreateDirectory(_settingsFolder);
            }

            if (File.Exists(_settingsPath))
            {
                string json = File.ReadAllText(_settingsPath);
                var rawSettings = JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.AppSettings);
                if (rawSettings != null)
                {
                    _currentSettings = rawSettings;
                    // Giải mã các trường nhạy cảm
                    _currentSettings.TelegramChatId = Decrypt(_currentSettings.TelegramChatId);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Utilities.Logger.Error("Không thể load cấu hình", ex);
        }

        // Khởi tạo mặc định nếu lỗi hoặc chưa có file
        _currentSettings = new AppSettings
        {
            TelegramEnabled = false,
            TelegramChatId = string.Empty,
            ClearRamEnabled = false,
            ClearRamIntervalMinutes = 15,
            LastProcessedEventRecordId = 0,
            LastProcessedSuccessRecordId = 0,
            LastProcessedFailureRecordId = 0
        };
        SaveSettings();
    }

    public void SaveSettings()
    {
        try
        {
            if (!Directory.Exists(_settingsFolder))
            {
                Directory.CreateDirectory(_settingsFolder);
            }

            // Tạo bản sao để mã hóa trước khi ghi ra file
            var settingsToSave = new AppSettings
            {
                TelegramEnabled = _currentSettings.TelegramEnabled,
                TelegramChatId = Encrypt(_currentSettings.TelegramChatId),
                ClearRamEnabled = _currentSettings.ClearRamEnabled,
                ClearRamIntervalMinutes = _currentSettings.ClearRamIntervalMinutes,
                LastProcessedEventRecordId = _currentSettings.LastProcessedEventRecordId,
                LastProcessedSuccessRecordId = _currentSettings.LastProcessedSuccessRecordId,
                LastProcessedFailureRecordId = _currentSettings.LastProcessedFailureRecordId
            };

            string json = JsonSerializer.Serialize(settingsToSave, AppJsonSerializerContext.Default.AppSettings);
            File.WriteAllText(_settingsPath, json);
        }
        catch (Exception ex)
        {
            Utilities.Logger.Error("Không thể ghi cấu hình", ex);
        }
    }

    private string Encrypt(string plainText)
    {
        if (string.IsNullOrEmpty(plainText)) return string.Empty;
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(plainText);
            byte[] encrypted = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encrypted);
        }
        catch (Exception ex)
        {
            Utilities.Logger.Error("Lỗi mã hóa dữ liệu DPAPI", ex);
            return string.Empty;
        }
    }

    private string Decrypt(string encryptedText)
    {
        if (string.IsNullOrEmpty(encryptedText)) return string.Empty;
        try
        {
            byte[] data = Convert.FromBase64String(encryptedText);
            byte[] decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(decrypted);
        }
        catch (Exception ex)
        {
            // Tránh ghi log chi tiết lỗi giải mã để bảo mật
            return string.Empty;
        }
    }
}
