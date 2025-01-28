using ColdWind.AdvancedConsoleManager;

using AdvancedConsoleAPI = ColdWind.AdvancedConsoleManager.AdvancedConsole;

namespace ColdWind.AdvancedConsole;

internal class Window
{
    private const int GeneralWindowLongStyle = -16;
    private const int WindowStyleThickFrame = 0x00040000;
    private const int WindowStyleMaximizeBox = 0x00010000;

    private const int SystemMetricsXScreenCoordinate = 0;
    private const int SystemMetricsYScreenCoordinate = 1;

    private const uint SettingWindowPositionNoSize = 0x0001;
    private const uint SettingWindowPositionNoZOrder = 0x0004;

    private readonly IntPtr Handle;

    public Window(int width, int height, string title = AdvancedConsoleAPI.DefaultTitle)
    {
        Handle = WinAPI.GetConsoleWindow();
        Screen = new Screen(new Vector2Int(width, height));

        InitializeStyle();
        SetTitle(title);
    }

    public Screen Screen { get; init; }

    public string GetTitle() => Console.Title;

    public void SetTitle(string title) =>
        Console.Title = title;

    public void MoveTo(Vector2Int position)
    {
        uint flags = SettingWindowPositionNoSize | SettingWindowPositionNoZOrder;
        WinAPI.SetWindowPos(Handle, IntPtr.Zero, position.X, position.Y, 0, 0, flags);
    }

    public void MoveToCenter()
    {
        Vector2Int monitorScreenSize = GetMonitorScreenSize();
        Vector2Int windowSize = GetWindowSize();

        Vector2Int newWindowPosition = (monitorScreenSize - windowSize) / 2;
        MoveTo(newWindowPosition);
    }

    public Vector2Int GetPosition()
    {
        WinAPI.GetWindowRect(Handle, out RectangleForm rectangleForm);

        return new Vector2Int(rectangleForm.Left, rectangleForm.Top);
    }

    private void InitializeStyle()
    {
        int style = WinAPI.GetWindowLong(Handle, GeneralWindowLongStyle);

        style ^= WindowStyleThickFrame;
        style &= ~WindowStyleMaximizeBox;

        WinAPI.SetWindowLong(Handle, GeneralWindowLongStyle, style);
    }

    private Vector2Int GetWindowSize()
    {
        WinAPI.GetWindowRect(Handle, out RectangleForm rectangleForm);

        int width = rectangleForm.Right - rectangleForm.Left;
        int height = rectangleForm.Bottom - rectangleForm.Top;

        return new Vector2Int(width, height);
    }

    private Vector2Int GetMonitorScreenSize()
    {
        return new Vector2Int(
            WinAPI.GetSystemMetrics(SystemMetricsXScreenCoordinate),
            WinAPI.GetSystemMetrics(SystemMetricsYScreenCoordinate));
    }
}
