using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace VPSManager.Views;

public partial class MainWindow : Window
{
    private TaskCompletionSource<bool>? _confirmTcs;

    public MainWindow()
    {
        InitializeComponent();
    }

    // Hàm gọi hiển thị hộp thoại xác nhận tuỳ chỉnh, AOT-friendly hoàn toàn
    public Task<bool> ShowConfirmDialog(string title, string content)
    {
        _confirmTcs = new TaskCompletionSource<bool>();
        
        DialogTitleText.Text = title;
        DialogContentText.Text = content;

        var cancelButton = this.FindControl<Button>("CancelButton");
        var confirmButton = this.FindControl<Button>("ConfirmButton");
        if (cancelButton != null) cancelButton.IsVisible = true;
        if (confirmButton != null) confirmButton.Content = "Đồng ý";
        
        DialogOverlay.IsVisible = true;
        
        return _confirmTcs.Task;
    }

    // Hàm hiển thị hộp thoại thông báo đơn giản (chỉ có nút OK), AOT-friendly
    public Task<bool> ShowMessageDialog(string title, string content)
    {
        _confirmTcs = new TaskCompletionSource<bool>();
        
        DialogTitleText.Text = title;
        DialogContentText.Text = content;

        var cancelButton = this.FindControl<Button>("CancelButton");
        var confirmButton = this.FindControl<Button>("ConfirmButton");
        if (cancelButton != null) cancelButton.IsVisible = false;
        if (confirmButton != null) confirmButton.Content = "OK";
        
        DialogOverlay.IsVisible = true;
        
        return _confirmTcs.Task;
    }

    private void OnConfirmDialogClick(object? sender, RoutedEventArgs e)
    {
        DialogOverlay.IsVisible = false;
        _confirmTcs?.TrySetResult(true);
    }

    private void OnCancelDialogClick(object? sender, RoutedEventArgs e)
    {
        DialogOverlay.IsVisible = false;
        _confirmTcs?.TrySetResult(false);
    }
}