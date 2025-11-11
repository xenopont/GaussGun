using GaussPlatform.Exceptions;

namespace GaussPlatform.Services;

public class TrayManager:IDisposable
{
    private readonly NotifyIcon? _notifyIcon;
    private const string WhiteIconResourcePath = "Resources/TrayIcon/White.ico";
    private const string BlackIconResourcePath = "Resources/TrayIcon/Black.ico";

    public TrayManager()
    {
        var iconStream = Utils.ResourceLoader.Load(WhiteIconResourcePath);
        if (iconStream == null)
        {
            throw new ApplicationStartException($"Cannot load try icon resource: {WhiteIconResourcePath}");
        }

        _notifyIcon = new NotifyIcon
        {
            Icon = new Icon(iconStream),
            Visible = true,
            Text = "Gauss Platform"
        };
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _notifyIcon?.Visible = false;
        _notifyIcon?.Dispose();
    }
}