namespace ColdWind.AdvancedConsoleManager.Internal;

public interface IReadOnlyHandyVector<T>
    where T : Vector
{
    float Magnitude { get; }

    float SquaredMagnitude { get; }

    T Copy();
}
