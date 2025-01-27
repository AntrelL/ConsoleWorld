using System.Numerics;

namespace ColdWind.AdvancedConsoleManager.Internal;

public abstract class Vector3<T, TVector> : Vector2<T, Vector3<T, TVector>>
    where T : INumber<T>
    where TVector : Vector3<T, TVector>
{
    protected Vector3(Vector3<T, TVector> vector3)
        : this(vector3.X, vector3.Y, vector3.Z)
    {
    }

    protected Vector3(T x, T y, T z)
        : base(x, y)
    {
        Z = z;
    }

    public T Z { get; set; }

    protected override bool BaseEquals(object? obj)
    {
        if (obj == null)
            return false;

        return obj is TVector vector2
            ? (X == vector2.X && Y == vector2.Y && Z == vector2.Z)
            : false;
    }

    protected override int BaseGetHashCode() => (X, Y, Z).GetHashCode();

    protected override string BaseToString() => (X, Y, Z).ToString();
}
