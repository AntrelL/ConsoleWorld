using System.Numerics;

namespace ColdWind.AdvancedConsoleManager.Internal;

public interface IReadOnlyVector3<T, TVector> : IReadOnlyVector2<T, TVector>
    where T : INumber<T>
    where TVector : Vector
{
    T Z { get; }
}
