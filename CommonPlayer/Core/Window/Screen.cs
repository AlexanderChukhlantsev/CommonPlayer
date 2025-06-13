using System.Runtime.InteropServices;
using System.Windows;

namespace CommonPlayer.Core.Window
{
    internal class Screen
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            public int Left, Top, Right, Bottom;
        }


        [StructLayout(LayoutKind.Sequential)]
        internal struct MONITORINFO
        {
            public uint cbSize;
            public RECT rcMonitor;
            public RECT rcWork;
            public uint dwFlags;
        }


        [DllImport("user32.dll")]
        private static extern nint MonitorFromPoint(POINT pt, uint dwFlags);


        [DllImport("user32.dll")]
        private static extern bool GetMonitorInfo(nint hMonitor, ref MONITORINFO lpmi);


        [StructLayout(LayoutKind.Sequential)]
        private struct POINT(int x, int y)
        {
            public int X = x;
            public int Y = y;
        }


        internal static MONITORINFO? GetMonitor(Point mousePosition)
        {
            POINT point = new((int)mousePosition.X, (int)mousePosition.Y);

            nint monitor = MonitorFromPoint(point, 2);

            if (monitor != nint.Zero)
            {
                var monitorInfo = new MONITORINFO { cbSize = (uint)Marshal.SizeOf<MONITORINFO>() };

                if (GetMonitorInfo(monitor, ref monitorInfo))
                {
                    return monitorInfo;
                }
            }

            return null;
        }
    }
}
