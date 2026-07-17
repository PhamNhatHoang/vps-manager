using CommunityToolkit.Mvvm.ComponentModel;

namespace VPSManager.Models;

public partial class ToolDownloadItem : ObservableObject
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string OutputFolderName { get; set; } = string.Empty;

    [ObservableProperty]
    private double _progress;

    [ObservableProperty]
    private string _status = "Chưa tải";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ButtonText))]
    private bool _isDownloading;

    public string ButtonText => IsDownloading ? "Đang tải..." : Name;
}
