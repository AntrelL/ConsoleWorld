using ColdWind.ConsoleWorldCore.OS;

namespace ColdWind.ConsoleWorld;

internal class Program
{
    static void Main(string[] args)
    {
        var consoleWorldOS = new ConsoleWorldOS();
        consoleWorldOS.Start();
    }
}
