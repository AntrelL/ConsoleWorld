using ColdWind.AdvancedConsoleManager.Internal;

namespace ColdWind.AdvancedConsoleManager;

public class Vector2Int : Vector2<int, Vector2Int>, IHandyVector<Vector2Int>, IReadOnlyVector2Int
{
    public Vector2Int(Vector2Int vector2Int)
        : base(vector2Int)
    {
    }

    public Vector2Int(int x = default, int y = default)
        : base(x, y)
    {
    }

    public static Vector2Int Zero => new Vector2Int();

    public static Vector2Int One => new Vector2Int(1, 1);

    public float Magnitude => GetMagnitude(X, Y);

    public float SquaredMagnitude => GetSquaredMagnitude(X, Y);

    public static float Distance(Vector2Int vector1, Vector2Int vector2) =>
        (vector1 - vector2).Magnitude;

    public Vector2Int Copy() => new Vector2Int(this);

    public override bool Equals(object? obj) => BaseEquals(obj);

    public override int GetHashCode() => BaseGetHashCode();

    public override string ToString() => BaseToString();

    public static Vector2Int operator +(Vector2Int vector1, Vector2Int vector2) =>
        new Vector2Int(vector1.X + vector2.X, vector1.Y + vector2.Y);

    public static Vector2Int operator -(Vector2Int vector1, Vector2Int vector2) =>
        new Vector2Int(vector1.X - vector2.X, vector1.Y - vector2.Y);

    public static Vector2Int operator *(Vector2Int vector1, float multiplier) =>
        new Vector2Int((int)(vector1.X * multiplier), (int)(vector1.Y * multiplier));

    public static Vector2Int operator /(Vector2Int vector1, float divisor) =>
        new Vector2Int((int)(vector1.X / divisor), (int)(vector1.Y / divisor));

    public static bool operator ==(Vector2Int vector1, Vector2Int vector2) =>
        Compare(vector1, vector2);

    public static bool operator !=(Vector2Int vector1, Vector2Int vector2) =>
        (vector1 == vector2) == false;

    public static implicit operator Vector2(Vector2Int vector2Int) =>
        new Vector2(vector2Int.X, vector2Int.Y);

    public static implicit operator Vector3(Vector2Int vector2Int) =>
        new Vector3(vector2Int.X, vector2Int.Y);

    public static implicit operator Vector3Int(Vector2Int vector2Int) =>
        new Vector3Int(vector2Int.X, vector2Int.Y);
}
