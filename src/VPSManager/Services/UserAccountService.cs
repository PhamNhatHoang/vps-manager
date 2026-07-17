using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using VPSManager.Utilities;

namespace VPSManager.Services;

public partial class UserAccountService
{
    private static readonly Lazy<UserAccountService> LazyInstance = new(() => new UserAccountService());
    public static UserAccountService Instance => LazyInstance.Value;

    [LibraryImport("netapi32.dll", SetLastError = true)]
    private static partial int NetUserSetInfo(
        [MarshalAs(UnmanagedType.LPWStr)] string? servername,
        [MarshalAs(UnmanagedType.LPWStr)] string username,
        int level,
        IntPtr buf,
        out uint parm_err);

    [LibraryImport("netapi32.dll", SetLastError = true)]
    private static partial int NetApiBufferFree(IntPtr buffer);

    [LibraryImport("netapi32.dll", SetLastError = true)]
    private static partial int NetUserEnum(
        [MarshalAs(UnmanagedType.LPWStr)] string? servername,
        int level,
        int filter,
        out IntPtr bufptr,
        int prefmaxlen,
        out int entriesread,
        out int totalentries,
        ref int resume_handle);

    private UserAccountService() { }

    public List<string> GetLocalUsers()
    {
        var users = new List<string>();
        IntPtr bufptr = IntPtr.Zero;
        int entriesread = 0;
        int totalentries = 0;
        int resume_handle = 0;
        const int FILTER_NORMAL_ACCOUNT = 2; // Chỉ lấy các tài khoản người dùng thông thường

        try
        {
            int result = NetUserEnum(null, 0, FILTER_NORMAL_ACCOUNT, out bufptr, -1, out entriesread, out totalentries, ref resume_handle);
            if (result == 0 && bufptr != IntPtr.Zero)
            {
                IntPtr iter = bufptr;
                for (int i = 0; i < entriesread; i++)
                {
                    IntPtr namePtr = Marshal.ReadIntPtr(iter);
                    if (namePtr != IntPtr.Zero)
                    {
                        string? username = Marshal.PtrToStringUni(namePtr);
                        if (!string.IsNullOrEmpty(username))
                        {
                            bool isSystem = string.Equals(username, "DefaultAccount", StringComparison.OrdinalIgnoreCase) ||
                                            string.Equals(username, "Guest", StringComparison.OrdinalIgnoreCase) ||
                                            string.Equals(username, "WDAGUtilityAccount", StringComparison.OrdinalIgnoreCase);
                            if (!isSystem)
                            {
                                users.Add(username);
                            }
                        }
                    }
                    iter = iter + IntPtr.Size;
                }
            }
            else
            {
                Logger.Error($"Lỗi khi lấy danh sách local users qua NetUserEnum. Mã lỗi: {result}");
            }
        }
        catch (Exception ex)
        {
            Logger.Error("Lỗi trong GetLocalUsers", ex);
        }
        finally
        {
            if (bufptr != IntPtr.Zero)
            {
                NetApiBufferFree(bufptr);
            }
        }

        // Fallback nếu danh sách trống
        if (users.Count == 0)
        {
            users.Add(Environment.UserName);
        }

        return users;
    }

    public bool ChangeUsername(string oldName, string newName, out string error)
    {
        error = string.Empty;
        if (!AdminService.Instance.IsRunningAsAdmin())
        {
            error = "Yêu cầu quyền Administrator để thực hiện đổi tên.";
            return false;
        }

        if (!SafePath.IsValidUsername(newName, out string valError))
        {
            error = valError;
            return false;
        }

        IntPtr stringPtr = IntPtr.Zero;
        IntPtr buf = IntPtr.Zero;

        try
        {
            stringPtr = Marshal.StringToHGlobalUni(newName);
            buf = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(buf, stringPtr);

            int result = NetUserSetInfo(null, oldName, 0, buf, out _);
            if (result != 0)
            {
                error = $"Lỗi hệ thống Windows API (Mã lỗi: {result}).";
                Logger.Error($"Lỗi khi cố gắng đổi username '{oldName}' thành '{newName}'. Code: {result}");
                return false;
            }

            Logger.Info($"Đã đổi username từ '{oldName}' sang '{newName}' thành công.");
            return true;
        }
        catch (Exception ex)
        {
            error = $"Lỗi: {ex.Message}";
            Logger.Error($"Lỗi đổi username từ '{oldName}' sang '{newName}'", ex);
            return false;
        }
        finally
        {
            if (stringPtr != IntPtr.Zero) Marshal.FreeHGlobal(stringPtr);
            if (buf != IntPtr.Zero) Marshal.FreeHGlobal(buf);
        }
    }

    public bool ChangePassword(string username, string newPassword, out string error)
    {
        error = string.Empty;
        if (!AdminService.Instance.IsRunningAsAdmin())
        {
            error = "Yêu cầu quyền Administrator để đổi mật khẩu.";
            return false;
        }

        if (!SafePath.IsValidPassword(newPassword, username, out string valError))
        {
            error = valError;
            return false;
        }

        IntPtr passwordPtr = IntPtr.Zero;
        IntPtr buf = IntPtr.Zero;

        try
        {
            passwordPtr = Marshal.StringToHGlobalUni(newPassword);
            buf = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(buf, passwordPtr);

            // Cấp 1003 đại diện cho thông tin mật khẩu (USER_INFO_1003)
            int result = NetUserSetInfo(null, username, 1003, buf, out _);
            if (result != 0)
            {
                error = $"Lỗi hệ thống Windows API (Mã lỗi: {result}).";
                Logger.Error($"Lỗi khi đổi mật khẩu của tài khoản '{username}'. Code: {result}");
                return false;
            }

            Logger.Info($"Đã đổi mật khẩu cho tài khoản '{username}' thành công.");
            return true;
        }
        catch (Exception ex)
        {
            error = $"Lỗi: {ex.Message}";
            Logger.Error($"Lỗi đổi mật khẩu cho tài khoản '{username}'", ex);
            return false;
        }
        finally
        {
            if (passwordPtr != IntPtr.Zero) Marshal.FreeHGlobal(passwordPtr);
            if (buf != IntPtr.Zero) Marshal.FreeHGlobal(buf);
        }
    }
}
