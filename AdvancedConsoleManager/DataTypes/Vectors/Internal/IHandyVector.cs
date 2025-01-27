namespace ColdWind.AdvancedConsoleManager.Internal;

public interface IHandyVector<T> : IReadOnlyHandyVector<T>
    where T : Vector
{
    static abstract T Zero { get; }

    static abstract T One { get; }

    static abstract float Distance(T vector1, T vector2);
}
