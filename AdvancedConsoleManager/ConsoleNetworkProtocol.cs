namespace ColdWind.AdvancedConsoleManager;

public static class ConsoleNetworkProtocol
{
    public const string ArgumentSeparationText = " p:";

    private static Dictionary<string, ServerCommand> ServerCommandLabels = new()
    {
        { nameof(ServerCommand.SetSize), ServerCommand.SetSize },
        { nameof(ServerCommand.MoveToCenter), ServerCommand.MoveToCenter },
        { nameof(ServerCommand.MoveToPosition), ServerCommand.MoveToPosition },
        { nameof(ServerCommand.SetTitle), ServerCommand.SetTitle },
        { nameof(ServerCommand.GetPosition), ServerCommand.GetPosition },
    };

    private static Dictionary<string, ClientResponse> ClientResponseLabels = new()
    {
        { nameof(ClientResponse.Position), ClientResponse.Position },
    };

    public enum ServerCommand
    {
        SetSize,
        MoveToCenter,
        MoveToPosition,
        SetTitle,
        GetPosition,
    }

    public enum ClientResponse
    {
        Position,
    }

    public static ServerCommand ConvertToServerCommand(string text) => 
        ServerCommandLabels[text.Split(ArgumentSeparationText)[0]];

    public static ClientResponse ConvertToClientResponse(string text) => 
        ClientResponseLabels[text.Split(ArgumentSeparationText)[0]];

    public static string ConvertToString(ServerCommand serverCommand) => 
        ServerCommandLabels.FirstOrDefault(x => x.Value == serverCommand).Key;

    public static string ConvertToString(ClientResponse clientResponse) =>
        ClientResponseLabels.FirstOrDefault(x => x.Value == clientResponse).Key;
}
