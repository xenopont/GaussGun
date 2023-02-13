using System;
using System.Collections.Generic;

namespace GaussGun
{
    internal class Experiment
    {
        public class Win32Window
        {
            public readonly IntPtr hWnd;
            public readonly string title;
            public readonly bool isIconic;
            public readonly Win32Api.Rectangle rectangle;
            public readonly uint zOrder;

            public Win32Window(
                IntPtr hWnd,
                string title,
                bool isIconic,
                Win32Api.Rectangle rectangle,
                uint zOrder
            )
            {
                this.hWnd = hWnd;
                this.title = title;
                this.isIconic = isIconic;
                this.rectangle = rectangle;
                this.zOrder = zOrder;
            }
        }

        public static List<Win32Window> ListAllWindows()
        {
            List<Win32Window> windows = new();
            uint zOrder = 0;

            Win32Api.EnumerateWindows((IntPtr hWnd) =>
            {
                bool isCloaked = Win32Api.IsWindowCloaked(hWnd);
                bool isVisible = Win32Api.IsWindowVisible(hWnd);
                bool isIconic = Win32Api.IsWindowIconic(hWnd);
                string title = Win32Api.GetWindowTitle(hWnd);
                Win32Api.Rectangle rectangle = Win32Api.GetWindowRectangle(hWnd);
                if (isVisible && !isCloaked)
                {
                    windows.Add(new Win32Window(
                        hWnd,
                        $"{zOrder} - {title} {rectangle}",
                        isIconic,
                        rectangle,
                        zOrder++
                    ));
                }
                return true;
            });

            return windows;
        }
    }
}
