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
                bool isCloaked = IsWindowCloaked(hWnd);
                string title = GetWindowTitle(hWnd);
                if (true)
                {
                    windows.Add(new Win32Window(hWnd, title + (isCloaked ? " CLOACKED" : "")));
                }
                return true;
            }, 0);

            return windows;
        }

        private static bool IsWindowVisible(IntPtr hWnd)
        {
            return Win32.IsWindowVisible(hWnd) > 0;
        }

        private static bool IsWindowCloaked(IntPtr hWnd)
        {
            int IsCloaked = 0;
            
            return Win32.DwmGetWindowAttribute(hWnd, Win32.DwmWindowAttribute.DWMWA_CLOAKED, ref IsCloaked, sizeof(int)) > 0 && IsCloaked > 0;
        }

        private static string GetWindowTitle(IntPtr hWnd)
        {
            int length = Win32.GetWindowTextLength(hWnd);
            if (length == 0)
            {
                return NO_TITLE;
            }
            StringBuilder sb = new(length + 1);
            int res = Win32.GetWindowText(hWnd, sb, sb.Capacity);
            if (res == 0) { return "ERROR GETTING TITLE"; }
            return sb.ToString();
        }

        private const string NO_TITLE = "NO TITLE";
    }
}
