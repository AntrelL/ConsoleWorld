using ColdWind.AdvancedConsoleManager;
using System.Runtime.InteropServices;

namespace ColdWind.AdvancedConsole;

internal class Screen
{
    private const uint StandardInputHandle = unchecked((uint)-10);
    private const uint StandardOutputHandle = unchecked((uint)-11);

    private const uint EnableQuickEdit = 0x0040;

    private readonly IntPtr InputHandle;
    private readonly IntPtr OutputHandle;

    private Vector2Int _size;

    public Screen(Vector2Int size)
    {
        InputHandle = WinAPI.GetStdHandle(StandardInputHandle);
        OutputHandle = WinAPI.GetStdHandle(StandardOutputHandle);

        _size = size;

        InitializeMode();
        SetSize(size);
    }

    public Vector2Int Size => _size.Copy();

    public void SetSize(int width, int height) => SetSize(new Vector2Int(width, height));

    public void SetSize(Vector2Int size)
    {
        int widthOfBlackSideLine = 2;
        _size = size;

        Console.Clear();

        Console.SetWindowSize(size.X - widthOfBlackSideLine, size.Y);
        Console.SetBufferSize(size.X, size.Y);

        WinAPI.ConsoleScreenBufferInfoEx bufferInfo = default;
        bufferInfo.cbSize = (uint)Marshal.SizeOf(bufferInfo);

        WinAPI.GetConsoleScreenBufferInfoEx(OutputHandle, ref bufferInfo);

        ++bufferInfo.srWindow.Right;
        ++bufferInfo.srWindow.Bottom;

        WinAPI.SetConsoleScreenBufferInfoEx(OutputHandle, ref bufferInfo);
    }

    private void InitializeMode()
    {
        WinAPI.GetConsoleMode(InputHandle, out uint consoleMode);
        WinAPI.SetConsoleMode(InputHandle, consoleMode & ~EnableQuickEdit);
    }
}
