using System.IO;
using Application = System.Windows.Application;

namespace GaussPlatform.Utils;

public static class ResourceLoader
{
    public static Stream? Load(string path)
    {
        var stream = Application.GetResourceStream(new Uri($"pack://application:,,,{path}"));
        return stream?.Stream;
    }
}