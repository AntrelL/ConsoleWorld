using ColdWind.AdvancedConsoleManager.Internal;

namespace ColdWind.AdvancedConsoleManager;

public class Vector3Int : Vector3<int, Vector3Int>, IHandyVector<Vector3Int>, IReadOnlyVector3Int
{
    public Vector3Int(Vector3Int vector3Int)
        : base(vector3Int)
    {
    }

    public Vector3Int(int x = default, int y = default, int z = default)
        : base(x, y, z)
    {
    }

    public static Vector3Int Zero => new Vector3Int();

    public static Vector3Int One => new Vector3Int(1, 1, 1);

    public float Magnitude => GetMagnitude(X, Y, Z);

    public float SquaredMagnitude => GetSquaredMagnitude(X, Y, Z);

    public static float Distance(Vector3Int vector1, Vector3Int vector2) =>
        (vector1 - vector2).Magnitude;

    public Vector3Int Copy() => new Vector3Int(this);

    public override bool Equals(object? obj) => BaseEquals(obj);

    public override int GetHashCode() => BaseGetHashCode();

    public override string ToString() => BaseToString();

    public static Vector3Int operator +(Vector3Int vector1, Vector3Int vector2) =>
        new Vector3Int(vector1.X + vector2.X, vector1.Y + vector2.Y, vector1.Z + vector2.Z);

    public static Vector3Int operator -(Vector3Int vector1, Vector3Int vector2) =>
        new Vector3Int(vector1.X - vector2.X, vector1.Y - vector2.Y, vector1.Z - vector2.Z);

    public static Vector3Int operator *(Vector3Int vector1, float multiplier) =>
        new Vector3Int((int)(vector1.X * multiplier), (int)(vector1.Y * multiplier), (int)(vector1.Z * multiplier));

    public static Vector3Int operator /(Vector3Int vector1, float divisor) =>
        new Vector3Int((int)(vector1.X / divisor), (int)(vector1.Y / divisor), (int)(vector1.Z / divisor));

    public static bool operator ==(Vector3Int vector1, Vector3Int vector2) =>
        Compare(vector1, vector2);

    public static bool operator !=(Vector3Int vector1, Vector3Int vector2) =>
        (vector1 == vector2) == false;

    public static explicit operator Vector2(Vector3Int vector3Int) =>
        new Vector2(vector3Int.X, vector3Int.Y);

    public static explicit operator Vector2Int(Vector3Int vector3Int) =>
        new Vector2Int(vector3Int.X, vector3Int.Y);

    public static implicit operator Vector3(Vector3Int vector3Int) =>
        new Vector3(vector3Int.X, vector3Int.Y, vector3Int.Z);
}
