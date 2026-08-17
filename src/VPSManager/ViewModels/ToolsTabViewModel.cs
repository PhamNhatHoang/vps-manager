using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VPSManager.Models;
using VPSManager.Services;
using VPSManager.Utilities;

namespace VPSManager.ViewModels;

public partial class ToolsTabViewModel : ViewModelBase
{
    private const string PrimaryCdnToolsJsonUrl = "https://cdn.jsdelivr.net/gh/PhamNhatHoang/vps-manager@main/tools.json";
    private const string FallbackCdnToolsJsonUrl = "https://raw.githubusercontent.com/PhamNhatHoang/vps-manager/refs/heads/main/tools.json";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsListVisible))]
    private bool _isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsListVisible))]
    private bool _hasError;

    [ObservableProperty] private string _errorMessage = string.Empty;

    public bool IsListVisible => !IsLoading && !HasError;

    [ObservableProperty]
    private ObservableCollection<ToolDownloadItem> _tools = new();

    public ToolsTabViewModel()
    {
        _ = LoadRemoteToolsAsync();
    }

    [RelayCommand]
    public async Task ReloadToolsAsync()
    {
        await LoadRemoteToolsAsync();
    }

    private async Task LoadRemoteToolsAsync()
    {
        Dispatcher.UIThread.Post(() =>
        {
            IsLoading = true;
            HasError = false;
            ErrorMessage = string.Empty;
            Tools.Clear();
        });

        List<ToolDownloadItem>? items = null;
        string? lastErrorMessage = null;

        // 1. Thử tải từ CDN chính jsDelivr (nhanh, toàn cầu, không bị giới hạn Rate Limit)
        try
        {
            using var httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
            httpClient.DefaultRequestHeaders.Add("User-Agent", "VPSManager-App");

            string json = await httpClient.GetStringAsync(PrimaryCdnToolsJsonUrl);
            items = JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.ListToolDownloadItem);

            if (items != null && items.Count > 0)
            {
                _ = SaveToolsCacheAsync(json);
            }
        }
        catch (Exception ex)
        {
            Logger.Warn($"Không thể tải tools từ CDN jsDelivr: {ex.Message}");
            lastErrorMessage = ex.Message;
        }

        // 2. Nếu CDN chính không lấy được, fallback sang GitHub Raw trực tiếp (không dùng query ?t= để tránh bị 429)
        if (items == null || items.Count == 0)
        {
            try
            {
                using var httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
                httpClient.DefaultRequestHeaders.Add("User-Agent", "VPSManager-App");

                string json = await httpClient.GetStringAsync(FallbackCdnToolsJsonUrl);
                items = JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.ListToolDownloadItem);

                if (items != null && items.Count > 0)
                {
                    _ = SaveToolsCacheAsync(json);
                }
            }
            catch (Exception ex)
            {
                Logger.Warn($"Không thể tải tools từ GitHub Raw fallback: {ex.Message}");
                lastErrorMessage = ex.Message;
            }
        }

        // 3. Nếu mạng hoàn toàn lỗi / offline / rate limit, đọc từ file tools.json local trong thư mục ứng dụng
        if (items == null || items.Count == 0)
        {
            items = await LoadLocalToolsFallbackAsync();
        }

        // 4. Cập nhật UI
        Dispatcher.UIThread.Post(() =>
        {
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    Tools.Add(item);
                }
                IsLoading = false;
                HasError = false;
            }
            else
            {
                IsLoading = false;
                HasError = true;
                ErrorMessage = $"Không thể tải danh sách công cụ.\nChi tiết lỗi: {lastErrorMessage ?? "Không tìm thấy dữ liệu công cụ"}";
            }
        });
    }

    private static async Task SaveToolsCacheAsync(string json)
    {
        try
        {
            string localPath = Path.Combine(AppContext.BaseDirectory, "tools.json");
            await File.WriteAllTextAsync(localPath, json);
        }
        catch (Exception ex)
        {
            Logger.Warn($"Không thể lưu cache tools.json: {ex.Message}");
        }
    }

    private static async Task<List<ToolDownloadItem>?> LoadLocalToolsFallbackAsync()
    {
        try
        {
            string localPath = Path.Combine(AppContext.BaseDirectory, "tools.json");
            if (File.Exists(localPath))
            {
                string json = await File.ReadAllTextAsync(localPath);
                var items = JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.ListToolDownloadItem);
                if (items != null && items.Count > 0)
                {
                    Logger.Info("Đã nạp danh sách công cụ từ file local tools.json");
                    return items;
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Warn($"Không thể nạp tools.json từ local: {ex.Message}");
        }
        return null;
    }

    [RelayCommand]
    private async Task DownloadToolAsync(ToolDownloadItem item)
    {
        if (item == null || item.IsDownloading) return;

        // Reset trạng thái trước khi tải
        item.IsDownloading = true;
        item.Progress = 0;
        item.Status = "Đang tải 0%...";

        var cts = new CancellationTokenSource();


        var progress = new Progress<double>(val =>
        {
            Dispatcher.UIThread.Post(() =>
            {
                item.Progress = val;
                if (val < 100)
                {
                    item.Status = $"Đang tải {val:F1}%...";
                }
                else
                {
                    item.Status = "Đang giải nén...";
                }
            });
        });

        try
        {
            await ToolDownloadService.Instance.DownloadAndExtractAsync(
                item.FileName, 
                item.OutputFolderName, 
                progress, 
                cts.Token
            );

            Dispatcher.UIThread.Post(() =>
            {
                item.Progress = 100;
                item.Status = "Tải & Giải nén thành công! (Lưu ở Desktop)";
            });

            await Dispatcher.UIThread.InvokeAsync(async () =>
            {
                await ShowMessageDialogAsync("Tải Thành Công", $"Đã tải và giải nén thành công công cụ '{item.Name}' ra màn hình Desktop!");
            });
        }
        catch (OperationCanceledException)
        {
            Dispatcher.UIThread.Post(() =>
            {
                item.Progress = 0;
                item.Status = "Đã hủy tải.";
            });
        }
        catch (Exception ex)
        {
            Dispatcher.UIThread.Post(() =>
            {
                item.Progress = 0;
                item.Status = $"Lỗi: {ex.Message}";
            });
            Logger.Error($"Tải tool {item.Name} thất bại", ex);

            await Dispatcher.UIThread.InvokeAsync(async () =>
            {
                await ShowMessageDialogAsync("Lỗi Tải Tool", $"Tải công cụ '{item.Name}' thất bại!\nChi tiết: {ex.Message}");
            });
        }
        finally
        {
            item.IsDownloading = false;
            cts.Dispose();
        }
    }

    private async Task ShowMessageDialogAsync(string title, string content)
    {
        var lifetime = Avalonia.Application.Current?.ApplicationLifetime as Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime;
        if (lifetime?.MainWindow is Views.MainWindow mainWin)
        {
            await mainWin.ShowMessageDialog(title, content);
        }
    }

}
