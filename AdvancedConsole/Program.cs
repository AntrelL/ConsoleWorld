using ColdWind.AdvancedConsoleManager;

namespace ColdWind.AdvancedConsole;

internal class Program
{
    static void Main(string[] args)
    {
        TestWindow();

        Console.WriteLine("Hello, World! (Advanced Console)");
        Console.ReadKey();
    }

    private static void TestWindow()
    {
        var window = new Window(50, 25);
        window.MoveToCenter();

        Thread.Sleep(2000);
        window.MoveTo(new Vector2Int(200, 200));

        Thread.Sleep(1000);
        window.Title = "Console :))))";
        window.Screen.SetSize(75, 20);
        window.MoveTo(new Vector2Int(1200, 200));

        Thread.Sleep(1000);

        int windowStartPositionX = window.GetPosition().X;
        int windowStartPositionY = window.GetPosition().Y;

        for (int i = windowStartPositionX; i > windowStartPositionX - 1000; i -= 1)
        {
            window.MoveTo(new Vector2Int(i, window.GetPosition().Y));
            Thread.Sleep(1);
        }
    }
}
