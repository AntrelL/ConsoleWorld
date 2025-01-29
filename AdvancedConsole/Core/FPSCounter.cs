using ColdWind.AdvancedConsoleManager;
using System.Diagnostics;

namespace ColdWind.AdvancedConsole;

internal class FPSCounter : IFramesSource, IFPSCounterDisplay
{
    private readonly Screen Screen;

    private Frame _frameForRender;
    private Stopwatch _timer;

    private long _elapsedExecutionTime = 0;
    private int _intermediateFPSPerSecond = 0;
    private int _numberOfFPSToDisplay = 0;

    public FPSCounter(Screen screen)
    {
        Screen = screen;

        _frameForRender = new Frame(Screen.Size);
        _timer = new Stopwatch();
    }

    public bool Activated { get; private set; } = false;

    public bool DisplayEnabled { get; private set; } = false;

    public void Activate()
    {
        if (DisplayEnabled)
            throw new Exception("The FPS counter is already activated.");

        Activated = true;
        Screen.SizeChanged += OnScreenSizeChanged;
    }

    public void Deactivate()
    {
        if (DisplayEnabled == false)
            throw new Exception("The FPS counter is already deactivated.");

        Activated = false;
        Screen.SizeChanged -= OnScreenSizeChanged;
    }

    public void EnableDisplay()
    {
        if (Activated == false)
            throw new Exception("The FPS counter is not activated.");

        if (DisplayEnabled)
            throw new Exception("FPS display is already enabled.");

        DisplayEnabled = true;
        CreateFrameForRender();
    }

    public void DisableDisplay()
    {
        if (Activated == false)
            throw new Exception("The FPS counter is not activated.");

        if (DisplayEnabled == false)
            throw new Exception("FPS display is already disabled.");

        DisplayEnabled = false;
        CreateFrameForRender();
    }

    public void StartMeasuringFrameTime()
    {
        if (_timer.IsRunning)
            throw new Exception("Frame time measurement is already running.");

        _timer.Start();
    }

    public void StopMeasuringFrameTime()
    {
        if (_timer.IsRunning == false)
            throw new Exception("Frame time measurement not started.");

        _timer.Stop();
        AddFrameMetering(_timer.ElapsedMilliseconds);
        _timer.Reset();
    }

    public Frame GetFrameForRender() => _frameForRender;

    private void AddFrameMetering(long frameTime)
    {
        int numberOfCounterUpdatesPerSecond = 2;
        long counterUpdateTimeInMilliseconds = 1000;

        counterUpdateTimeInMilliseconds /= numberOfCounterUpdatesPerSecond;

        _elapsedExecutionTime += frameTime;
        _intermediateFPSPerSecond++;

        if (_elapsedExecutionTime < counterUpdateTimeInMilliseconds)
            return;

        _elapsedExecutionTime -= counterUpdateTimeInMilliseconds;
        _numberOfFPSToDisplay = _intermediateFPSPerSecond * numberOfCounterUpdatesPerSecond;
        _intermediateFPSPerSecond = 0;

        CreateFrameForRender();
    }

    private void CreateFrameForRender()
    {
        var frameForRender = new Frame(Screen.Size);

        if (DisplayEnabled == false)
        {
            _frameForRender = frameForRender;
            return;
        }

        Vector2Int indentation = new Vector2Int(2, 1);

        string numberOfFPSText = $"FPS: {_numberOfFPSToDisplay}";

        int endXPointOfText = frameForRender.Size.X - indentation.X;
        int startPointOfText = endXPointOfText - numberOfFPSText.Length;

        int textCounter = 0;

        for (int x = startPointOfText; x < endXPointOfText; x++)
        {
            var cell = new Frame.Cell(numberOfFPSText[textCounter++], Color.White, Color.Black);
            frameForRender.Cells[x, indentation.Y] = cell;
        }

        _frameForRender = frameForRender;
    }

    private void OnScreenSizeChanged(IReadOnlyVector2Int readOnlyVector2Int) =>
        CreateFrameForRender();
}
