using ColdWind.AdvancedConsoleManager;
using System.Runtime.InteropServices;

namespace ColdWind.AdvancedConsole;

internal static class WinAPI
{
    [DllImport("kernel32.dll")]
    public static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    [DllImport("user32.dll")]
    public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    [DllImport("user32.dll")]
    public static extern int GetSystemMetrics(int nIndex);

    [DllImport("user32.dll")]
    public static extern bool GetWindowRect(IntPtr hWnd, out RectangleForm lpRect);

    [DllImport("user32.dll")]
    public static extern bool SetWindowPos(
        IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

    [DllImport("kernel32.dll")]
    public static extern IntPtr GetStdHandle(uint nStdHandle);

    [DllImport("kernel32.dll")]
    public static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

    [DllImport("kernel32.dll")]
    public static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

    [DllImport("kernel32.dll")]
    public static extern bool GetConsoleScreenBufferInfoEx(
        IntPtr hConsoleOutput, ref ConsoleScreenBufferInfoEx lpConsoleScreenBufferInfoEx);

    [DllImport("kernel32.dll")]
    public static extern bool SetConsoleScreenBufferInfoEx(
        IntPtr hConsoleOutput, ref ConsoleScreenBufferInfoEx lpConsoleScreenBufferInfoEx);

    [DllImport("kernel32.dll")]
    public static extern bool WriteConsoleOutputAttribute(
        IntPtr hConsoleOutput,
        ref ushort lpAttribute,
        uint nLength,
        Coord dwWriteCoord,
        out uint lpNumberOfAttrsWritten);

    [DllImport("kernel32.dll")]
    public static extern bool WriteConsoleOutputCharacter(
        IntPtr hConsoleOutput,
        string lpCharacter,
        uint nLength,
        Coord dwWriteCoord,
        out uint lpNumberOfCharsWritten);

    [StructLayout(LayoutKind.Sequential)]
    public struct Coord
    {
        public short X;
        public short Y;

        public Coord(short x, short y)
        {
            X = x;
            Y = y;
        }

        public Coord(Vector2Int vector2Int)
            : this((short)vector2Int.X, (short)vector2Int.Y)
        {
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ConsoleScreenBufferInfoEx
    {
        public uint cbSize;
        public Coord dwSize;
        public Coord dwCursorPosition;
        public ushort wAttributes;
        public SmallRect srWindow;
        public Coord dwMaximumWindowSize;
        public ushort wPopupAttributes;
        public bool bFullscreenSupported;

        public Colorref black, darkBlue, darkGreen, darkCyan, darkRed, darkMagenta,
            darkYellow, gray, darkGray, blue, green, cyan, red, magenta, yellow, white;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Colorref
    {
        public uint ColorDWORD;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SmallRect
    {
        public short Left, Top, Right, Bottom;

        public SmallRect(short width, short height)
        {
            Left = Top = 0;

            Right = width;
            Bottom = height;
        }
    }
}
