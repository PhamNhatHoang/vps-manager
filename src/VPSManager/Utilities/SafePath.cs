using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace VPSManager.Utilities;

public static class SafePath
{
    private static readonly int[] BlacklistedPorts = { 0, 135, 139, 445 }; // Chặn các cổng cực kỳ nguy hiểm (SMB, RPC)

    public static bool IsValidPort(int port, out string error)
    {
        error = string.Empty;
        if (port < 1025 || port > 65535)
        {
            error = "Cổng RDP phải nằm trong khoảng từ 1025 đến 65535.";
            return false;
        }

        if (BlacklistedPorts.Contains(port))
        {
            error = $"Cổng {port} là cổng hệ thống nguy hiểm, không được phép sử dụng.";
            return false;
        }

        return true;
    }

    public static bool IsValidUsername(string username, out string error)
    {
        error = string.Empty;
        if (string.IsNullOrWhiteSpace(username))
        {
            error = "Tên đăng nhập không được để trống.";
            return false;
        }

        string trimmed = username.Trim();

        if (trimmed.Length < 3 || trimmed.Length > 20)
        {
            error = $"Tên đăng nhập phải từ 3 đến 20 ký tự (hiện có {trimmed.Length} ký tự).";
            return false;
        }

        if (!char.IsLetter(trimmed[0]))
        {
            error = "Tên đăng nhập phải bắt đầu bằng chữ cái (A-Z hoặc a-z).";
            return false;
        }

        if (trimmed.Contains(' ') || trimmed.Any(c => c > 127))
        {
            error = "Tên đăng nhập không được chứa khoảng trắng hoặc chữ tiếng Việt có dấu.";
            return false;
        }

        if (!Regex.IsMatch(trimmed, @"^[A-Za-z][A-Za-z0-9_-]{2,19}$"))
        {
            error = "Tên đăng nhập chỉ được chứa chữ cái, chữ số, dấu gạch ngang '-' hoặc gạch dưới '_'.";
            return false;
        }

        // 2. Chặn các tài khoản hệ thống Windows đặc thù
        string[] blockedSystemNames = { 
            "Administrator", 
            "Guest", 
            "DefaultAccount", 
            "WDAGUtilityAccount", 
            "SYSTEM", 
            "LOCAL", 
            "NETWORK" 
        };

        foreach (var blockedName in blockedSystemNames)
        {
            if (string.Equals(trimmed, blockedName, StringComparison.OrdinalIgnoreCase))
            {
                error = $"Không được đặt tên trùng với tài khoản hệ thống mặc định '{blockedName}'.";
                return false;
            }
        }

        return true;
    }

    public static bool IsSafeDirectoryToDelete(string path, out string error)
    {
        error = string.Empty;
        if (string.IsNullOrWhiteSpace(path))
        {
            error = "Đường dẫn không hợp lệ.";
            return false;
        }

        try
        {
            string fullPath = Path.GetFullPath(path).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            
            // 1. Kiểm tra tồn tại
            if (!Directory.Exists(fullPath))
            {
                error = "Thư mục không tồn tại.";
                return false;
            }

            // 2. Kiểm tra có phải root drive không
            string? root = Path.GetPathRoot(fullPath);
            if (string.Equals(fullPath, root, StringComparison.OrdinalIgnoreCase))
            {
                error = "Không thể xóa thư mục gốc của ổ đĩa.";
                return false;
            }

            // 3. Lấy danh sách các thư mục hệ thống nguy hiểm cần chặn
            string winDir = Environment.GetFolderPath(Environment.SpecialFolder.Windows).TrimEnd(Path.DirectorySeparatorChar);
            string progFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles).TrimEnd(Path.DirectorySeparatorChar);
            string progFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86).TrimEnd(Path.DirectorySeparatorChar);
            string sysDir = Environment.GetFolderPath(Environment.SpecialFolder.System).TrimEnd(Path.DirectorySeparatorChar);
            string userDir = Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)) ?? @"C:\Users";

            string[] dangerousDirs = { winDir, progFiles, progFilesX86, sysDir, userDir };

            foreach (var dangerousDir in dangerousDirs)
            {
                if (string.Equals(fullPath, dangerousDir, StringComparison.OrdinalIgnoreCase) ||
                    fullPath.StartsWith(dangerousDir + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
                {
                    // Ngoại lệ: Cho phép xóa thư mục .microemulator hoặc rms nằm bên trong thư mục người dùng
                    if ((fullPath.EndsWith(".microemulator", StringComparison.OrdinalIgnoreCase) || 
                         fullPath.Contains(Path.DirectorySeparatorChar + "rms", StringComparison.OrdinalIgnoreCase) ||
                         fullPath.Contains(Path.DirectorySeparatorChar + ".microemulator", StringComparison.OrdinalIgnoreCase)) &&
                        !string.Equals(fullPath, userDir, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    error = $"Thư mục này nằm trong vùng hệ thống hoặc vùng cấm ({dangerousDir}), không thể xóa để bảo đảm an toàn.";
                    return false;
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            error = $"Lỗi kiểm tra đường dẫn: {ex.Message}";
            return false;
        }
    }

    public static bool IsValidPassword(string password, string username, out string error)
    {
        error = string.Empty;
        if (string.IsNullOrWhiteSpace(password))
        {
            error = "Mật khẩu mới không được để trống.";
            return false;
        }

        if (password.Length < 8 || password.Length > 32)
        {
            error = $"Mật khẩu phải có độ dài từ 8 đến 32 ký tự (hiện tại có {password.Length} ký tự).";
            return false;
        }

        if (password.Contains(' '))
        {
            error = "Mật khẩu không được chứa khoảng trắng.";
            return false;
        }

        if (password.Any(c => c > 127))
        {
            error = "Mật khẩu không được chứa ký tự tiếng Việt có dấu hoặc ký tự Unicode.";
            return false;
        }

        bool hasUpper = password.Any(char.IsUpper);
        bool hasLower = password.Any(char.IsLower);
        bool hasDigit = password.Any(char.IsDigit);
        bool hasSpecial = password.Any(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c));

        var missing = new System.Collections.Generic.List<string>();
        if (!hasUpper) missing.Add("1 chữ hoa (A-Z)");
        if (!hasLower) missing.Add("1 chữ thường (a-z)");
        if (!hasDigit) missing.Add("1 chữ số (0-9)");
        if (!hasSpecial) missing.Add("1 ký tự đặc biệt (ví dụ: !@#$%^&*_-+=.)");

        if (missing.Count > 0)
        {
            error = $"Mật khẩu chưa đủ độ phức tạp. Còn thiếu: {string.Join(", ", missing)}.";
            return false;
        }

        if (!string.IsNullOrEmpty(username) && password.Contains(username, StringComparison.OrdinalIgnoreCase))
        {
            error = $"Mật khẩu không được chứa tên tài khoản đăng nhập ('{username}').";
            return false;
        }

        return true;
    }
}
