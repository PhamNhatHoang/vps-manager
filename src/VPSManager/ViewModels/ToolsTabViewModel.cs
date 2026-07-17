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
    private const string RemoteToolsJsonUrl = "https://raw.githubusercontent.com/PhamNhatHoang/vps-manager/refs/heads/main/tools.json";

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

    private async Task LoadRemoteToolsAsync()
    {
        Dispatcher.UIThread.Post(() =>
        {
            IsLoading = true;
            HasError = false;
            ErrorMessage = string.Empty;
            Tools.Clear();
        });

        try
        {
            using var httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(15) };
            httpClient.DefaultRequestHeaders.Add("User-Agent", "VPSManager-App");

            // Sử dụng cache buster để tránh GitHub CDN cache file cũ
            string urlWithNoCache = $"{RemoteToolsJsonUrl}?t={DateTime.UtcNow.Ticks}";
            string json = await httpClient.GetStringAsync(urlWithNoCache);
            var items = JsonSerializer.Deserialize(json, AppJsonSerializerContext.Default.ListToolDownloadItem);

            Dispatcher.UIThread.Post(() =>
            {
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        Tools.Add(item);
                    }
                }
                IsLoading = false;
            });
        }
        catch (Exception ex)
        {
            Logger.Error("Không thể tải danh sách Tools từ GitHub", ex);
            Dispatcher.UIThread.Post(() =>
            {
                IsLoading = false;
                HasError = true;
                ErrorMessage = $"Không thể tải danh sách công cụ.\nChi tiết lỗi: {ex.Message}";
            });
        }
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
