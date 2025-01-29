using ColdWind.AdvancedConsoleManager;

namespace ColdWind.AdvancedConsole;

internal class TestColorChangeCanvas : IFramesSource
{
    private Frame _frameForRender;

    private Screen _screen;
    private Random _random;

    public TestColorChangeCanvas(Screen screen)
    {
        _screen = screen;

        _random = new Random();
        _frameForRender = new Frame(screen.Size);
    }

    public Frame GetFrameForRender() => _frameForRender;

    public void CreateFrameWithRandomColor()
    {
        Color color = (Color)_random.Next(1, 17);
        var cell = new Frame.Cell(backgroundColor: color);

        _frameForRender = new Frame(_screen.Size, cell);
    }
}
