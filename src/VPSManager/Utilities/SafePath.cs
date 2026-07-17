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

        // 1. Kiểm tra độ dài và định dạng bằng Regex:
        // - Bắt đầu bằng chữ cái
        // - Độ dài từ 3 đến 20 ký tự
        // - Không khoảng trắng, không tiếng Việt có dấu, chỉ chứa chữ cái, số, gạch dưới và gạch ngang
        if (!Regex.IsMatch(username, @"^[A-Za-z][A-Za-z0-9_-]{2,19}$"))
        {
            error = "Username phải từ 3-20 ký tự, bắt đầu bằng chữ cái, không dấu, không khoảng trắng, chỉ chứa chữ, số, '-' và '_'.";
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
            if (string.Equals(username, blockedName, StringComparison.OrdinalIgnoreCase))
            {
                error = $"Không được đặt tên trùng với tài khoản hệ thống '{blockedName}'.";
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
            error = "Mật khẩu không được để trống.";
            return false;
        }

        // 1. Kiểm tra độ dài từ 12 - 32 ký tự, phải có ít nhất 1 chữ thường, 1 chữ hoa, 1 số, 1 ký tự đặc biệt
        // Sử dụng Regex khuyến nghị điều chỉnh độ dài thành 12-32:
        if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$*_\-+=.])[A-Za-z\d!@#$*_\-+=.]{12,32}$"))
        {
            error = "Mật khẩu phải từ 12-32 ký tự, chứa ít nhất 1 chữ thường, 1 chữ hoa, 1 số, 1 ký tự đặc biệt (!@#$*_-+=.), không dấu, không khoảng trắng.";
            return false;
        }

        // 2. Không chứa username (không phân biệt chữ hoa/thường)
        if (!string.IsNullOrEmpty(username) && password.Contains(username, StringComparison.OrdinalIgnoreCase))
        {
            error = "Mật khẩu không được chứa tên đăng nhập (Username).";
            return false;
        }

        return true;
    }
}
