using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SharpCompress.Archives;
using SharpCompress.Common;

namespace VPSManager.Services;

public class ToolDownloadService
{
    private static readonly Lazy<ToolDownloadService> LazyInstance = new(() => new ToolDownloadService());
    public static ToolDownloadService Instance => LazyInstance.Value;

    private const string BaseReleaseUrl = "https://github.com/PhamNhatHoang/vps-manager/releases/download/tools/";

    private readonly HttpClient _httpClient;

    private ToolDownloadService()
    {
        _httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromMinutes(10) // Hỗ trợ tải file dung lượng trung bình/lớn
        };
    }

    public async Task DownloadAndExtractAsync(
        string url, 
        string outputFolderName, 
        IProgress<double> progressReporter, 
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException("Đường dẫn tải xuống không được để trống.");
        }

        // Nếu URL chỉ là tên file (không chứa giao thức http/https), tự động ghép với Base URL của repository
        if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && 
            !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            url = $"{BaseReleaseUrl}{url}";
        }
        else if (url.StartsWith("https://github.com/", StringComparison.OrdinalIgnoreCase) && 
                 !url.EndsWith(".zip", StringComparison.OrdinalIgnoreCase) && 
                 !url.EndsWith(".rar", StringComparison.OrdinalIgnoreCase) && 
                 !url.EndsWith(".7z", StringComparison.OrdinalIgnoreCase))
        {
            // Tự động phân tích và chuyển đổi URL nếu chỉ copy link Github repository
            string cleanUrl = url.TrimEnd('/');
            string repoName = cleanUrl.Substring(cleanUrl.LastIndexOf('/') + 1);
            url = $"{cleanUrl}/releases/latest/download/{repoName}.rar";
        }

        if (!url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("Đường dẫn tải xuống phải sử dụng giao thức bảo mật HTTPS.");
        }

        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string outputDirectoryPath = Path.Combine(desktopPath, outputFolderName);

        // Tạo tên file tải tạm thời trên Desktop
        string tempFilePath = Path.Combine(desktopPath, $"{outputFolderName}_temp_{Guid.NewGuid():N}.tmp");

        try
        {
            Utilities.Logger.Info($"Bắt đầu tải công cụ từ: {url}");
            
            // 1. Tải file về máy
            using (var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {
                response.EnsureSuccessStatusCode();

                long? totalBytes = response.Content.Headers.ContentLength;
                using var downloadStream = await response.Content.ReadAsStreamAsync(cancellationToken);
                using var fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);

                byte[] buffer = new byte[8192];
                long totalReadBytes = 0;
                int readBytes;

                while ((readBytes = await downloadStream.ReadAsync(buffer, cancellationToken)) > 0)
                {
                    await fileStream.WriteAsync(buffer.AsMemory(0, readBytes), cancellationToken);
                    totalReadBytes += readBytes;

                    if (totalBytes.HasValue && totalBytes.Value > 0)
                    {
                        double progress = (double)totalReadBytes / totalBytes.Value * 100.0;
                        progressReporter.Report(Math.Round(progress, 1));
                    }
                }
            }

            Utilities.Logger.Info($"Đã tải xong file tạm thời: {tempFilePath}. Tiến hành giải nén...");
            progressReporter.Report(100.0); // Hoàn thành tải, bắt đầu giải nén

            // Tạo thư mục giải nén nếu chưa tồn tại
            if (!Directory.Exists(outputDirectoryPath))
            {
                Directory.CreateDirectory(outputDirectoryPath);
            }

            // 2. Giải nén file
            await Task.Run(() =>
            {
                // Kiểm tra định dạng đuôi file để tối ưu
                if (url.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
                {
                    System.IO.Compression.ZipFile.ExtractToDirectory(tempFilePath, outputDirectoryPath, true);
                }
                else
                {
                    // Dùng GrindCore.SharpCompress để mở và giải nén RAR (hoặc các định dạng khác như 7z)
                    using var archive = ArchiveFactory.Open(tempFilePath);
                    foreach (var entry in archive.Entries)
                    {
                        entry.WriteToDirectory(outputDirectoryPath, new ExtractionOptions
                        {
                            ExtractFullPath = true,
                            Overwrite = true
                        });
                    }
                }
            }, cancellationToken);

            Utilities.Logger.Info($"Đã giải nén công cụ '{outputFolderName}' thành công vào: {outputDirectoryPath}");
        }
        finally
        {
            // 3. Dọn dẹp file tải tạm thời
            try
            {
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);
                }
            }
            catch (Exception ex)
            {
                Utilities.Logger.Warn($"Không thể xóa file tạm thời '{tempFilePath}': {ex.Message}");
            }
        }
    }
}
