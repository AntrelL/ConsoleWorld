namespace ColdWind.AdvancedConsoleManager;

public interface IFPSCounterDisplay
{
    public bool DisplayEnabled { get; }

    public void EnableDisplay();

    public void DisableDisplay();
}
