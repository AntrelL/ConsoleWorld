using ColdWind.AdvancedConsoleManager;

namespace ColdWind.ConsoleWorldCore.OS;

public class ConsoleWorldOS
{
    public void Start()
    {
        var mainConsole = new AdvancedConsole();
        Console.WriteLine("ConsoleWorldOS started");

        TestAdvancedConsole(mainConsole);

        Console.ReadKey();
        mainConsole.Destroy();
    }

    private void TestAdvancedConsole(AdvancedConsole console)
    {
        Console.WriteLine("Advanced Console test started");

        console.MoveToCenter();

        Thread.Sleep(2000);
        console.MoveTo(200, 200);

        Thread.Sleep(1000);
        console.Title = "Console :))))";
        console.SetSize(75, 20);
        console.MoveTo(new Vector2Int(1200, 200));

        Console.WriteLine("---Position:" + console.GetPosition());

        int windowStartPositionX = console.GetPosition().X;
        int windowStartPositionY = console.GetPosition().Y;

        for (int i = windowStartPositionX; i > windowStartPositionX - 1000; i -= 3)
        {
            console.MoveTo(new Vector2Int(i, console.GetPosition().Y));
            Thread.Sleep(1);
        }

        Console.WriteLine("---Position:" + console.GetPosition());

        Thread.Sleep(100);
        Console.WriteLine("Advanced Console test finished");
    }
}
