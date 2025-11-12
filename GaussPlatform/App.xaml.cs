using System.Windows;
using GaussPlatform.Services;
using MessageBox = System.Windows.MessageBox;

namespace GaussPlatform;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    private TrayManager? _trayManager;
        
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        try
        {
            _trayManager = new TrayManager((_, __) => {}, (_, __) => GracefullyShutdown());
        }
        catch (Exception exception)
        {
            MessageBox.Show($"Application cannot be load.\nError: {exception.Message}");
            GracefullyShutdown();
        }
    }

    private void GracefullyShutdown()
    {
        _trayManager?.Dispose();
        Shutdown();
    }
}