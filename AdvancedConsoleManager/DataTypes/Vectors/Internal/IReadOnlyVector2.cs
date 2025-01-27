using System.Numerics;

namespace ColdWind.AdvancedConsoleManager.Internal;

public interface IReadOnlyVector2<T, TVector> : IReadOnlyHandyVector<TVector>
    where T : INumber<T>
    where TVector : Vector
{
    T X { get; }

    T Y { get; }
}
