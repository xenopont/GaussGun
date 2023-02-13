using System;
using System.Runtime.InteropServices;
using System.Text;

namespace GaussGun
{
    internal class Win32Api
    {
        public delegate bool EnumerateWindowsCallback(IntPtr hWnd);
        public static void EnumerateWindows(EnumerateWindowsCallback callback)
        {
            _ = RawApi.EnumWindows((IntPtr hWnd, int lParam) =>
            {
                return callback(hWnd);
            }, 0);
        }

        public static Rectangle GetWindowRectangle(IntPtr hWnd)
        {
            int hResult = RawApi.GetWindowRect(hWnd, out RawApi.RECT rectangle);
            if (hResult == 0)
            {
                return new Rectangle();
            }

            return new Rectangle(
                rectangle.left,
                rectangle.top,
                rectangle.right - rectangle.left,
                rectangle.bottom - rectangle.top
            );
        }

        #region Remove after window title is not needed
        public static string GetWindowTitle(IntPtr hWnd)
        {
            int length = RawApi.GetWindowTextLength(hWnd);
            if (length <= 0)
            {
                return "NO TITLE";
            }
            StringBuilder sb = new(length + 1);
            int res = RawApi.GetWindowText(hWnd, sb, sb.Capacity);
            if (res == 0)
            {
                return "ERROR GETTING TITLE";
            }
            return sb.ToString();
        }
        #endregion

        public static bool IsWindowCloaked(IntPtr hWnd)
        {
            int isCloaked = 0;
            uint hResult = RawApi.DwmGetWindowAttribute(
                hWnd,
                RawApi.DwmWindowAttribute.DWMWA_CLOAKED,
                ref isCloaked,
                4
            );

            return hResult == 0 && isCloaked > 0;
        }

        public static bool IsWindowIconic(IntPtr hWnd)
        {
            return RawApi.IsIconic(hWnd) > 0;
        }

        public static bool IsWindowVisible(IntPtr hWnd)
        {
            return RawApi.IsWindowVisible(hWnd) > 0;
        }

        public readonly struct Rectangle
        {
            public readonly int Left;
            public readonly int Top;
            public readonly int Width;
            public readonly int Height;

            public Rectangle(int left, int top, int width, int height)
            {
                Left = left;
                Top = top;
                Width = width;
                Height = height;
            }

            public Rectangle() : this(0, 0, 0, 0) { }

            public override string ToString()
            {
                return $"[{Left}, {Top}; {Width}, {Height}]";
            }
        } 

        private static class RawApi
        {
            private const string DLL_DWM_API = "dwmapi.dll";
            private const string DLL_USER_32 = "user32.dll";

            [DllImport(DLL_DWM_API)]
            public static extern uint DwmGetWindowAttribute(
                IntPtr hWnd,
                DwmWindowAttribute attributeName,
                ref int attributeValue,
                int cbSize
            );
            public enum DwmWindowAttribute : uint
            {
                DWMWA_NCRENDERING_ENABLED = 1,
                DWMWA_NCRENDERING_POLICY,
                DWMWA_TRANSITIONS_FORCEDISABLED,
                DWMWA_ALLOW_NCPAINT,
                DWMWA_CAPTION_BUTTON_BOUNDS,
                DWMWA_NONCLIENT_RTL_LAYOUT,
                DWMWA_FORCE_ICONIC_REPRESENTATION,
                DWMWA_FLIP3D_POLICY,
                DWMWA_EXTENDED_FRAME_BOUNDS,
                DWMWA_HAS_ICONIC_BITMAP,
                DWMWA_DISALLOW_PEEK,
                DWMWA_EXCLUDED_FROM_PEEK,
                DWMWA_CLOAK,
                DWMWA_CLOAKED,
                DWMWA_FREEZE_REPRESENTATION,
                DWMWA_LAST
            }

            public delegate bool WNDENUMPROC(IntPtr hWnd, int lParam);
            [DllImport(DLL_USER_32)]
            public static extern int EnumWindows(WNDENUMPROC EnumWindowsProc, int lParam);

            [DllImport(DLL_USER_32)]
            public static extern int GetWindowRect(IntPtr hWnd, out RECT lpRect);

            #region To remove after window title not needed
            [DllImport(DLL_USER_32, CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

            [DllImport(DLL_USER_32)]
            public static extern int GetWindowTextLength(IntPtr hWnd);
            #endregion

            [DllImport(DLL_USER_32)]
            public static extern int IsIconic(IntPtr hWnd);

            [DllImport(DLL_USER_32)]
            public static extern int IsWindowVisible(IntPtr hWnd);

            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }
        }
    }
}
