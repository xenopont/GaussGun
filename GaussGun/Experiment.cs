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
            public readonly string title;
            public readonly bool isIconic;
            public readonly Win32.Rect rect;

            public Win32Window(IntPtr hWnd, string title, bool isIconic, Win32.Rect rect)
            {
                this.hWnd = hWnd;
                this.title = title;
                this.isIconic = isIconic;
                this.rect = rect;
            }
        }

        public static List<Win32Window> ListAllWindows()
        {
            List<Win32Window> windows = new();

            _ = Win32.EnumWindows((IntPtr hWnd, int lParam) =>
            {
                bool isCloaked = IsWindowCloaked(hWnd);
                bool isIconic = Win32.IsIconic(hWnd);
                string title = GetWindowTitle(hWnd);
                Win32.GetWindowRect(hWnd, out var rect);
                if (IsWindowVisible(hWnd) && !isCloaked)
                {
                    windows.Add(new Win32Window(hWnd, $"{title} {rect}", isIconic, rect));
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
            int isCloaked = 0;
            uint ret = Win32.DwmGetWindowAttribute(hWnd, Win32.DwmWindowAttribute.DWMWA_CLOAKED, ref isCloaked, 4);
            return ret == 0 && isCloaked > 0;
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
