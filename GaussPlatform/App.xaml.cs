using System.Windows;

namespace GaussPlatform;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    private NotifyIcon? _notifyIcon;
        
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var stream = GetResourceStream(new Uri("pack://application:,,,/Resources/Icons/TrayIcon/White.ico"));
        if (stream == null)
        {
            return;
        }
        _notifyIcon = new NotifyIcon
        {
            Icon = new Icon(stream.Stream),
            Visible = true,
            Text = "My WPF Tray App"
        };

        var contextMenu = new ContextMenuStrip();
        contextMenu.Items.Add("Show Settings", null, null);
        contextMenu.Items.Add("Exit", null, OnExitClicked);
        _notifyIcon.ContextMenuStrip = contextMenu;
    }
    
    protected override void OnExit(ExitEventArgs e)
    {
        _notifyIcon?.Visible = false;
        _notifyIcon?.Dispose();
        base.OnExit(e);
    }
    
    private void OnExitClicked(object? sender, EventArgs e)
    {
        Shutdown();
    }
}