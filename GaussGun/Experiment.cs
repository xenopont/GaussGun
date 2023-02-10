using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussGun
{
    internal class Experiment
    {
        public class Win32Window
        {
            public readonly IntPtr hWnd;
            public readonly string Title;

            public Win32Window(IntPtr hWnd, string title)
            {
                this.hWnd = hWnd;
                Title = title;
            }
        }

        public static List<Win32Window> ListAllWindows()
        {
            List<Win32Window> windows = new();

            _ = Win32.EnumWindows((IntPtr hWnd, int lParam) =>
            {
                if (Win32.IsWindowVisible(hWnd) > 0)
                {
                    windows.Add(new Win32Window(hWnd, hWnd.ToString()));
                }
                return true;
            }, 0);

            return windows;
        }
    }
}
