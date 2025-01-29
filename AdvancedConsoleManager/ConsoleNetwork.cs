using System.IO.Pipes;

namespace ColdWind.AdvancedConsoleManager;

public class ConsoleNetwork
{
    private const string Input = nameof(Input);
    private const string Output = nameof(Output);

    private int _consoleId;
    private bool _connected;
    private bool _isServer;

    private Queue<string> _messagesToSend;

    public ConsoleNetwork(int consoleId, bool isServer)
    {
        _consoleId = consoleId;
        _isServer = isServer;
        _connected = false;

        _messagesToSend = new Queue<string>();
    }

    public event Action<string>? MessageReceived;

    public void Connect()
    {
        _connected = true;
        CreateIOPipes();
    }

    public void Disconnect()
    {
        while (_messagesToSend.Count > 0)
            Thread.Sleep(1);

        _connected = false;
    }

    public void SendMessage(string message, bool lastMessage = false)
    {
        if (lastMessage)
            _messagesToSend.Clear();

        _messagesToSend.Enqueue(message);
    }

    public void SendMessage(ConsoleNetworkProtocol.ServerCommand serverCommand, string arguments = "", bool lastMessage = false) =>
        SendStandardizedMessage(ConsoleNetworkProtocol.ConvertToString(serverCommand), arguments, lastMessage);

    public void SendMessage(ConsoleNetworkProtocol.ClientResponse clientResponse, string arguments = "", bool lastMessage = false) => 
        SendStandardizedMessage(ConsoleNetworkProtocol.ConvertToString(clientResponse), arguments, lastMessage);

    private void SendStandardizedMessage(string command, string arguments, bool lastMessage)
    {
        string separator = string.IsNullOrEmpty(arguments) 
            ? string.Empty 
            : ConsoleNetworkProtocol.ArgumentSeparationText;

        SendMessage(string.Join(separator, [command, arguments]), lastMessage);
    }
    
    private string GetPipeName(int id, bool isInput) => 
        $"ConsoleNetwork-{id}-{(isInput ? Input : Output)}";

    private void CreateIOPipes()
    {
        new Task(() =>
        {
            using (var server = new NamedPipeServerStream(GetPipeName(_consoleId, _isServer)))
            {
                Console.WriteLine("init server");
                Console.WriteLine("Waiting for connection...");
                server.WaitForConnection();
                Console.WriteLine("connected");

                using (var reader = new StreamReader(server))
                {
                    while (_connected)
                    {
                        string? line = reader.ReadLine();

                        if (string.IsNullOrEmpty(line) == false)
                            MessageReceived?.Invoke(line);
                    }
                }
            }
        }).Start();

        new Task(() =>
        {
            using (var client = new NamedPipeClientStream(".", GetPipeName(_consoleId, !_isServer), PipeDirection.Out))
            {
                Console.WriteLine("init client");
                client.Connect();

                using (var writer = new StreamWriter(client))
                {
                    while (_connected)
                    {
                        if (_messagesToSend.Count == 0 || (client.CanWrite == false))
                            continue;

                        writer.AutoFlush = true;
                        //Console.WriteLine(_messagesToSend.Peek());
                        writer.WriteLine(_messagesToSend.Dequeue());
                    }
                }
            }
        }).Start();
    }
}
