using System;
using System.IO;
using System.Collections.Generic;
using VPSManager.Utilities;

namespace VPSManager.Services;

public class GameDataService
{
    private static readonly Lazy<GameDataService> LazyInstance = new(() => new GameDataService());
    public static GameDataService Instance => LazyInstance.Value;

    private GameDataService() { }

    public bool DeleteGameData(out string resultMsg)
    {
        int deletedFiles = 0;
        int deletedFolders = 0;
        int skippedFiles = 0;

        var targetPaths = new List<string>();

        // 1. Thư mục .microemulator của user chạy app hiện tại
        string currentUserProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string currentMicroEmu = Path.Combine(currentUserProfile, ".microemulator");
        targetPaths.Add(currentMicroEmu);

        // 2. Thư mục .microemulator của Administrator (nếu chạy bằng user khác)
        string adminMicroEmu = @"C:\Users\Administrator\.microemulator";
        if (!string.Equals(currentMicroEmu, adminMicroEmu, StringComparison.OrdinalIgnoreCase))
        {
            targetPaths.Add(adminMicroEmu);
        }

        // 3. Thư mục "rms" hoặc "RMS" cùng thư mục chạy ứng dụng
        string localRms = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rms");
        targetPaths.Add(localRms);
        string localRmsCaps = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RMS");
        targetPaths.Add(localRmsCaps);

        Logger.Info("Khởi chạy tiến trình xóa dữ liệu Game (MicroEmulator .microemulator và các thư mục RMS)...");

        foreach (var rawPath in targetPaths)
        {
            if (string.IsNullOrWhiteSpace(rawPath)) continue;

            if (!Directory.Exists(rawPath))
            {
                continue;
            }

            // Kiểm tra an toàn trước khi xóa
            if (!SafePath.IsSafeDirectoryToDelete(rawPath, out string error))
            {
                Logger.Warn($"Bỏ qua thư mục '{rawPath}' do không an toàn: {error}");
                continue;
            }

            Logger.Info($"Bắt đầu dọn dẹp thư mục: {rawPath}");
            DeleteDirectoryContents(rawPath, ref deletedFiles, ref deletedFolders, ref skippedFiles);
            
            // Cố gắng xóa chính thư mục gốc nếu nó trống
            try
            {
                if (Directory.GetFileSystemEntries(rawPath).Length == 0)
                {
                    Directory.Delete(rawPath, false);
                    deletedFolders++;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Không thể xóa thư mục gốc '{rawPath}'", ex);
            }
        }

        resultMsg = $"Hoàn tất dọn dẹp! Đã xóa {deletedFiles} tệp tin, {deletedFolders} thư mục. Bỏ qua {skippedFiles} tệp tin bị khóa.";
        Logger.Info($"Kết quả xóa data game: {resultMsg}");
        return true;
    }

    private void DeleteDirectoryContents(string path, ref int deletedFiles, ref int deletedFolders, ref int skippedFiles)
    {
        // 1. Xóa các tệp tin trong thư mục hiện tại
        try
        {
            foreach (var file in Directory.GetFiles(path))
            {
                try
                {
                    File.Delete(file);
                    deletedFiles++;
                }
                catch (Exception ex)
                {
                    skippedFiles++;
                    Logger.Warn($"Không thể xóa tệp '{file}': {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Error($"Lỗi khi quét tệp tin trong thư mục '{path}'", ex);
        }

        // 2. Duyệt đệ quy xóa các thư mục con
        try
        {
            foreach (var dir in Directory.GetDirectories(path))
            {
                DeleteDirectoryContents(dir, ref deletedFiles, ref deletedFolders, ref skippedFiles);
                try
                {
                    if (Directory.GetFileSystemEntries(dir).Length == 0)
                    {
                        Directory.Delete(dir, false);
                        deletedFolders++;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Warn($"Không thể xóa thư mục con '{dir}': {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Error($"Lỗi khi quét thư mục con trong '{path}'", ex);
        }
    }
}
