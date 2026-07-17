using System;
using System.IO;

namespace VPSManager.Utilities;

public static class Logger
{
    private static readonly object LockObj = new();
    private static string? _logFilePath;

    private static string LogFilePath
    {
        get
        {
            if (_logFilePath == null)
            {
                string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string appFolder = Path.Combine(appData, "VPSManager");
                Directory.CreateDirectory(appFolder);
                _logFilePath = Path.Combine(appFolder, "app.log");
            }
            return _logFilePath;
        }
    }

    public static void Info(string message) => Log("INFO", message);
    public static void Warn(string message) => Log("WARN", message);
    public static void Error(string message, Exception? ex = null)
    {
        string fullMessage = message;
        if (ex != null)
        {
            fullMessage += $" | Exception: {ex.Message}\nStacktrace: {ex.StackTrace}";
        }
        Log("ERROR", fullMessage);
    }

    private static void Log(string level, string message)
    {
        // Loại bỏ mật khẩu hoặc các thông tin nhạy cảm khỏi log
        string sanitized = SanitizeMessage(message);

        lock (LockObj)
        {
            try
            {
                RotateLogIfNeeded();
                string logLine = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {sanitized}{Environment.NewLine}";
                File.AppendAllText(LogFilePath, logLine);
            }
            catch
            {
                // Bỏ qua lỗi ghi log để không làm crash ứng dụng chính
            }
        }
    }

    private static string SanitizeMessage(string message)
    {
        if (string.IsNullOrEmpty(message)) return message;

        // Xóa sạch hoặc ẩn mật khẩu trong các tham số net user
        if (message.Contains("net user", StringComparison.OrdinalIgnoreCase))
        {
            return "[Command net user ẩn thông tin nhạy cảm]";
        }

        // Ẩn Token Telegram nếu xuất hiện trong tin nhắn
        // Ví dụ: bot123456:ABC-DEF1234ghIkl-zyx57W2v1u123ew11
        if (message.Contains("api.telegram.org", StringComparison.OrdinalIgnoreCase))
        {
            return "[Telegram API Call - ẩn thông tin nhạy cảm]";
        }

        return message;
    }

    private static void RotateLogIfNeeded()
    {
        try
        {
            var fileInfo = new FileInfo(LogFilePath);
            if (fileInfo.Exists && fileInfo.Length > 5 * 1024 * 1024) // 5 MB
            {
                string backupPath = Path.Combine(Path.GetDirectoryName(LogFilePath)!, $"app_{DateTime.Now:yyyyMMdd_HHmmss}.log");
                File.Move(LogFilePath, backupPath);
            }
        }
        catch
        {
            // Bỏ qua lỗi xoay vòng file log
        }
    }
}
