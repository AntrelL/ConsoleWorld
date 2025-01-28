using System.Diagnostics;

namespace ColdWind.AdvancedConsoleManager;

public class AdvancedConsole
{
    public const int DefaultWidth = 50;
    public const int DefaultHeight = 25;
    public const string DefaultTitle = "Advanced Console";

    private static int IdCounter = 0;

    private int _id;
    private ConsoleNetwork _consoleNetwork;

    private Vector2Int _position;
    private Vector2Int _size;
    private string _title;

    private bool _isWaitData;
    private Action<string>? _receivedDataProcessor;

    public AdvancedConsole()
    {
        _isWaitData = false;
        _receivedDataProcessor = null;

        _position = Vector2Int.One * -1;
        _size = new Vector2Int(DefaultWidth, DefaultHeight);
        _title = DefaultTitle;

        _id = IdCounter++;
        _consoleNetwork = new ConsoleNetwork(_id, true);

        CreateNewConsole(_id);

        _consoleNetwork.MessageReceived += OnMessageReceived;
        _consoleNetwork.Connect();
    }

    public string Title
    {
        get => _title;
        set
        {
            _consoleNetwork.SendMessage(ConsoleNetworkProtocol.ServerCommand.SetTitle, value);
            _title = value;
        }
    }

    public Vector2Int Size => _size.Copy();

    public static string PackArguments<T>(params T[] arguments)
    {
        return string.Join(ConsoleNetworkProtocol.ArgumentSeparationText, arguments);
    }

    public Vector2Int GetPosition()
    {
        _isWaitData = true;

        _receivedDataProcessor = (string message) =>
        {
            ConsoleNetworkProtocol.ClientResponse clientResponse = 
                ConsoleNetworkProtocol.ConvertToClientResponse(message);

            if (clientResponse != ConsoleNetworkProtocol.ClientResponse.Position)
                return;

            string[] parts = message.Split(ConsoleNetworkProtocol.ArgumentSeparationText);
            _position = new Vector2Int(int.Parse(parts[1]), int.Parse(parts[2]));
        };

        _consoleNetwork.SendMessage(ConsoleNetworkProtocol.ServerCommand.GetPosition);

        while (_isWaitData)
            Thread.Sleep(1);

        return _position.Copy();
    }

    public void MoveToCenter() => 
        _consoleNetwork.SendMessage(ConsoleNetworkProtocol.ServerCommand.MoveToCenter);

    public void MoveTo(Vector2Int position) =>
        MoveTo(position.X, position.Y);

    public void MoveTo(int x, int y)
    {
        string arguments = PackArguments(x, y);
        _consoleNetwork.SendMessage(ConsoleNetworkProtocol.ServerCommand.MoveToPosition, arguments);
    }

    public void SetSize(Vector2Int size) =>
        SetSize(size.X, size.Y);

    public void SetSize(int width, int height)
    {
        string arguments = PackArguments(width, height);
        _consoleNetwork.SendMessage(ConsoleNetworkProtocol.ServerCommand.SetSize, arguments);
    }

    public void Destroy()
    {
        _consoleNetwork.MessageReceived -= OnMessageReceived;
        _consoleNetwork.Disconnect();
    }

    private void CreateNewConsole(int id)
    {
        string advancedConsoleExePath = $"{AppContext.BaseDirectory}{nameof(AdvancedConsole)}.exe";

        Process.Start(new ProcessStartInfo("conhost.exe")
        {
            Arguments = $"cmd.exe /C \"{advancedConsoleExePath}\" {id}"
        });
    }

    private void OnMessageReceived(string message)
    {
        //Console.WriteLine($"Server get message: {message}");

        if (_isWaitData == false)
            return;
        
        _receivedDataProcessor?.Invoke(message);

        _receivedDataProcessor = null;
        _isWaitData = false;
    }
}
