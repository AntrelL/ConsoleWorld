using ColdWind.AdvancedConsoleManager;

using AdvancedConsoleAPI = ColdWind.AdvancedConsoleManager.AdvancedConsole;

namespace ColdWind.AdvancedConsole;

internal class Program
{
    private static ConsoleNetwork? ConsoleNetwork;
    private static int Id;

    private static Window? Window;

    static void Main(string[] args)
    {
        if (args.Length == 0)
            throw new ArgumentException("The Advanced Console should only be launched through Manager.");

        Window = new Window(AdvancedConsoleAPI.DefaultWidth, AdvancedConsoleAPI.DefaultHeight);

        Id = int.Parse(args[0]);
        ConsoleNetwork = new ConsoleNetwork(Id, false);

        ConsoleNetwork.MessageReceived += OnMessageReceived;
        ConsoleNetwork.Connect();

        while (true)
            Thread.Sleep(1000);
        
        //ConsoleNetwork.MessageReceived -= OnMessageReceived;
        //ConsoleNetwork.Disconnect();
    }

    private static void OnMessageReceived(string message)
    {
        //Console.WriteLine($"Console id={Id} get message: {message}");

        ConsoleNetworkProtocol.ServerCommand command = ConsoleNetworkProtocol.ConvertToServerCommand(message);

        switch (command)
        {
            case ConsoleNetworkProtocol.ServerCommand.SetSize:
                SetSize(message);
                break;
            case ConsoleNetworkProtocol.ServerCommand.MoveToCenter:
                Window?.MoveToCenter();
                break;
            case ConsoleNetworkProtocol.ServerCommand.MoveToPosition:
                MoveToPosition(message);
                break;
            case ConsoleNetworkProtocol.ServerCommand.SetTitle:
                SetTitle(message);
                break;
            case ConsoleNetworkProtocol.ServerCommand.GetPosition:
                SendPosition();
                break;
        }
    }

    private static void SendPosition()
    {
        Vector2Int? position = Window?.GetPosition();
        string arguments = AdvancedConsoleAPI.PackArguments([position?.X, position?.Y]);
        ConsoleNetwork?.SendMessage(ConsoleNetworkProtocol.ClientResponse.Position, arguments);
    }

    private static void SetSize(string command)
    {
        string[] parts = command.Split(ConsoleNetworkProtocol.ArgumentSeparationText);
        Window?.Screen.SetSize(int.Parse(parts[1]), int.Parse(parts[2]));
    }

    private static void MoveToPosition(string command)
    {
        string[] parts = command.Split(ConsoleNetworkProtocol.ArgumentSeparationText);
        Window?.MoveTo(new Vector2Int(int.Parse(parts[1]), int.Parse(parts[2])));
    }

    private static void SetTitle(string command)
    {
        string[] parts = command.Split(ConsoleNetworkProtocol.ArgumentSeparationText);
        Window?.SetTitle(parts[1]);
    }
}
