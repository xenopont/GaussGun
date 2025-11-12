using GaussPlatform.Exceptions;

namespace GaussPlatform.Services;

public class TrayManager:IDisposable
{
    private const string WhiteIconResourcePath = "/Resources/Icons/TrayIcon/White.ico";
    private const string BlackIconResourcePath = "/Resources/Icons/TrayIcon/Black.ico";
    
    private readonly NotifyIcon? _notifyIcon;
    private readonly ContextMenuStrip? _contextMenu;

    public TrayManager(EventHandler showSettings, EventHandler exit)
    {
        _contextMenu = new ContextMenuStrip();
        _contextMenu.Items.Add("Settings", null, showSettings);
        _contextMenu.Items.Add("Exit", null, exit);
        
        var iconStream = Utils.ResourceLoader.Load(WhiteIconResourcePath);
        if (iconStream == null)
        {
            throw new ApplicationStartException($"Cannot load tray icon resource: {WhiteIconResourcePath}");
        }

        _notifyIcon = new NotifyIcon
        {
            Icon = new Icon(iconStream),
            Visible = true,
            Text = "Gauss Platform",
            ContextMenuStrip = _contextMenu
        };
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _notifyIcon?.Visible = false;
        _contextMenu?.Dispose();
        _notifyIcon?.Dispose();
    }
}