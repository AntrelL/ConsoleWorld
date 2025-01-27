using ColdWind.AdvancedConsoleManager.Internal;

namespace ColdWind.AdvancedConsoleManager;

public class Vector2 : Vector2<float, Vector2>, IHandyVector<Vector2>, IReadOnlyVector2
{
    public Vector2(Vector2 vector2)
        : base(vector2)
    {
    }

    public Vector2(float x = default, float y = default)
        : base(x, y)
    {
    }

    public static Vector2 Zero => new Vector2();

    public static Vector2 One => new Vector2(1f, 1f);

    public float Magnitude => GetMagnitude(X, Y);

    public float SquaredMagnitude => GetSquaredMagnitude(X, Y);

    public static float Distance(Vector2 vector1, Vector2 vector2) => 
        (vector1 - vector2).Magnitude;

    public Vector2 Copy() => new Vector2(this);

    public override bool Equals(object? obj) => BaseEquals(obj);

    public override int GetHashCode() => BaseGetHashCode();

    public override string ToString() => BaseToString();

    public static Vector2 operator +(Vector2 vector1, Vector2 vector2) =>
        new Vector2(vector1.X + vector2.X, vector1.Y + vector2.Y);

    public static Vector2 operator -(Vector2 vector1, Vector2 vector2) =>
        new Vector2(vector1.X - vector2.X, vector1.Y - vector2.Y);

    public static Vector2 operator *(Vector2 vector1, float multiplier) =>
        new Vector2(vector1.X * multiplier, vector1.Y * multiplier);

    public static Vector2 operator /(Vector2 vector1, float divisor) =>
        new Vector2(vector1.X / divisor, vector1.Y / divisor);

    public static bool operator ==(Vector2 vector1, Vector2 vector2) =>
        Compare(vector1, vector2);

    public static bool operator !=(Vector2 vector1, Vector2 vector2) =>
        (vector1 == vector2) == false;

    public static explicit operator Vector2Int(Vector2 vector2) =>
        new Vector2Int((int)vector2.X, (int)vector2.Y);

    public static implicit operator Vector3(Vector2 vector2) =>
        new Vector3(vector2.X, vector2.Y);

    public static explicit operator Vector3Int(Vector2 vector2) =>
        new Vector3Int((int)vector2.X, (int)vector2.Y);
}
