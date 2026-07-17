using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VPSManager.Services;

namespace VPSManager.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private bool _isAdmin;
    [ObservableProperty] private string _appVersion = "v1.0.0";

    public string AdminStatusText => IsAdmin ? "Administrator (Toàn quyền)" : "Người Dùng Thường (Hạn chế)";
    public string AdminStatusColor => IsAdmin ? "#28A745" : "#DC3545";

    public VpsTabViewModel VpsTab { get; }
    public LoginLogsTabViewModel LoginLogsTab { get; }
    public ToolsTabViewModel ToolsTab { get; }

    public MainWindowViewModel()
    {
        IsAdmin = AdminService.Instance.IsRunningAsAdmin();
        VpsTab = new VpsTabViewModel();
        LoginLogsTab = new LoginLogsTabViewModel();
        ToolsTab = new ToolsTabViewModel();
    }

    [RelayCommand]
    private void RunAsAdmin()
    {
        AdminService.Instance.RestartAsAdmin();
    }
}
