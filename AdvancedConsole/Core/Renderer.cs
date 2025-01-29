#pragma warning disable CS8602

using ColdWind.AdvancedConsoleManager;

namespace ColdWind.AdvancedConsole;

internal class Renderer
{
    private const uint NumberOfCellsToChangeAtTime = 1;

    private const ushort ForegroundBlue = 0x01;
    private const ushort ForegroundGreen = 0x02;
    private const ushort ForegroundRed = 0x04;
    private const ushort ForegroundIntensity = 0x08;

    private const ushort BackgroundBlue = 0x10;
    private const ushort BackgroundGreen = 0x20;
    private const ushort BackgroundRED = 0x40;
    private const ushort BackgroundIntensity = 0x80;

    private readonly Dictionary<Color, int> ForegroundColorCombinations;
    private readonly Dictionary<Color, int> BackgroundColorCombinations;

    private readonly Screen Screen;
    private readonly FPSCounter FPSCounter;

    private readonly Frame.Cell DefaultFrameCell;
    private readonly IntPtr OutputHandle;

    private Frame? _previousFrame = null;

    public Renderer(Screen screen, IntPtr outputHandle)
    {
        DefaultFrameCell = new Frame.Cell(
            symbol: ' ',
            foregroundColor: Color.Gray,
            backgroundColor: Color.Black);

        Screen = screen;
        OutputHandle = outputHandle;

        FPSCounter = new FPSCounter(Screen);
        FrameSourceControl = new FrameSourceControl();

        FPSCounter.Activate();
        FrameSourceControl.ConnectSource(FPSCounter, 0);

        ForegroundColorCombinations = CreateColorCombinations(
            ForegroundBlue, ForegroundGreen, ForegroundRed, ForegroundIntensity);

        BackgroundColorCombinations = CreateColorCombinations(
            BackgroundBlue, BackgroundGreen, BackgroundRED, BackgroundIntensity);
    }

    public FrameSourceControl FrameSourceControl { get; init; }

    public IFPSCounterDisplay FPSCounterControl => FPSCounter;

    public bool Running { get; private set; } = false;

    public void Run()
    {
        Running = true;
        Task.Run(CycleDrawFrames);
    }

    public void Stop() => Running = false;

    private void CycleDrawFrames()
    {
        int minFrameDrawingTimeInMilliseconds = 1;

        while (Running)
        {
            FPSCounter.StartMeasuringFrameTime();

            Frame complexFrame = FrameSourceControl.AssembleComplexFrame(
                Screen.Size, DefaultFrameCell);

            DrawFrame(complexFrame);
            Thread.Sleep(minFrameDrawingTimeInMilliseconds);

            FPSCounter.StopMeasuringFrameTime();
        }
    }

    private void DrawFrame(Frame frame)
    {
        Vector2Int size = frame.Size;

        for (int y = 0; y < size.Y; y++)
        {
            for (int x = 0; x < size.X; x++)
            {
                DrawFrameCell(frame.Cells[x, y], new Vector2Int(x, y));
            }
        }

        _previousFrame = frame.Copy;
    }

    private void DrawFrameCell(Frame.Cell cell, Vector2Int position)
    {
        bool compareWithPrevious = _previousFrame != null;

        Frame.Cell lastCell = compareWithPrevious
            ? _previousFrame.Cells[position.X, position.Y]
            : default;

        if (cell.ForegroundColor == Color.Default)
            cell.ForegroundColor = DefaultFrameCell.ForegroundColor;

        if (cell.BackgroundColor == Color.Default)
            cell.BackgroundColor = DefaultFrameCell.BackgroundColor;

        if (compareWithPrevious == false ||
            cell.ForegroundColor != lastCell.ForegroundColor ||
            cell.BackgroundColor != lastCell.BackgroundColor)
        {
            DrawFrameCellColors(cell.ForegroundColor, cell.BackgroundColor, position);
        }

        if (compareWithPrevious == false || cell.Symbol != lastCell.Symbol)
        {
            DrawFrameCellSymbol(cell.Symbol, position);
        }
    }

    private void DrawFrameCellColors(
        Color foregroundColor, Color backgroundColor, Vector2Int position)
    {
        ushort[] attributes = new ushort[NumberOfCellsToChangeAtTime];

        int numberColorCombination =
            ConvertToNumberColor(foregroundColor, true) |
            ConvertToNumberColor(backgroundColor, false);

        attributes[0] = (ushort)numberColorCombination;

        WinAPI.Coord coord = new(position);

        WinAPI.WriteConsoleOutputAttribute(
            OutputHandle, ref attributes[0], NumberOfCellsToChangeAtTime, coord, out uint _);
    }

    private void DrawFrameCellSymbol(char symbol, Vector2Int position)
    {
        WinAPI.Coord coord = new(position);

        WinAPI.WriteConsoleOutputCharacter(
            OutputHandle, symbol.ToString(), NumberOfCellsToChangeAtTime, coord, out uint _);
    }

    private int ConvertToNumberColor(Color color, bool foregroundColor)
    {
        return foregroundColor
            ? ForegroundColorCombinations[color]
            : BackgroundColorCombinations[color];
    }

    private Dictionary<Color, int> CreateColorCombinations(
        ushort blue, ushort green, ushort red, ushort intensity)
    {
        var colorCombinations = new Dictionary<Color, int>
        {
            { Color.Black, 0 },
            { Color.DarkBlue, blue },
            { Color.DarkGreen, green },
            { Color.DarkCyan, green | blue },
            { Color.DarkRed, red },
            { Color.DarkMagenta, red | blue },
            { Color.DarkYellow, red | green },
            { Color.DarkGray, red | green | blue },
            { Color.Gray, intensity },
            { Color.Blue, intensity | blue },
            { Color.Green, intensity | green },
            { Color.Cyan, intensity | green | blue },
            { Color.Red, intensity | red },
            { Color.Magenta, intensity | red | blue },
            { Color.Yellow, intensity | red | green },
            { Color.White, intensity | red | green | blue },
        };

        return colorCombinations;
    }
}
