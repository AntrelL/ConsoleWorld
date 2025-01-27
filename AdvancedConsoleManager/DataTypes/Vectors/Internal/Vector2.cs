using System.Numerics;

namespace ColdWind.AdvancedConsoleManager.Internal;

public abstract class Vector2<T, TVector> : Vector
    where T : INumber<T>
    where TVector : Vector2<T, TVector>
{
    protected Vector2(Vector2<T, TVector> vector2)
        : this(vector2.X, vector2.Y)
    {
    }

    protected Vector2(T x, T y)
    {
        X = x;
        Y = y;
    }

    public T X { get; set; }

    public T Y { get; set; }

    protected virtual bool BaseEquals(object? obj)
    {
        if (obj == null)
            return false;

        return obj is TVector vector2
            ? (X == vector2.X && Y == vector2.Y)
            : false;
    }

    protected virtual int BaseGetHashCode() => (X, Y).GetHashCode();

    protected virtual string BaseToString() => (X, Y).ToString();
}
