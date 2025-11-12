using System.IO;
using Application = System.Windows.Application;

namespace GaussPlatform.Utils;

public static class ResourceLoader
{
    public static Stream? Load(string path)
    {
        try
        {
            var stream = Application.GetResourceStream(new Uri($"pack://application:,,,{path}"));
            return stream?.Stream;
        }
        catch
        {
            return null;
        }
    }
}