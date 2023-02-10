﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GaussGun
{
    internal class Win32
    {
        public delegate bool CallBackPtr(IntPtr hWnd, int lParam);

        [DllImport("user32.dll")]
        public static extern int EnumWindows(CallBackPtr EnumWindowsProc, int lParam);

        [DllImport("user32.dll")]
        public static extern int IsWindowVisible(IntPtr hWnd);

        [DllImport("dwmapi.dll")]
        public static extern void DwmGetWindowAttribute(IntPtr hWnd, DwmWindowAttribute attributeName,
            ref int attributeValue, int attributeReceivedSize);
        public enum DwmWindowAttribute: uint
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
    }
}
