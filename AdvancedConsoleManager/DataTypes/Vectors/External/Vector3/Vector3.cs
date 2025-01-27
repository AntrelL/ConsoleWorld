using ColdWind.AdvancedConsoleManager.Internal;

namespace ColdWind.AdvancedConsoleManager;

public class Vector3 : Vector3<float, Vector3>, IHandyVector<Vector3>, IReadOnlyVector3
{
    public Vector3(Vector3 vector3)
        : base(vector3)
    {
    }

    public Vector3(float x = default, float y = default, float z = default)
        : base(x, y, z)
    {
    }

    public static Vector3 Zero => new Vector3();

    public static Vector3 One => new Vector3(1f, 1f, 1f);

    public float Magnitude => GetMagnitude(X, Y, Z);

    public float SquaredMagnitude => GetSquaredMagnitude(X, Y, Z);

    public static float Distance(Vector3 vector1, Vector3 vector2) =>
        (vector1 - vector2).Magnitude;

    public Vector3 Copy() => new Vector3(this);

    public override bool Equals(object? obj) => BaseEquals(obj);

    public override int GetHashCode() => BaseGetHashCode();

    public override string ToString() => BaseToString();

    public static Vector3 operator +(Vector3 vector1, Vector3 vector2) =>
        new Vector3(vector1.X + vector2.X, vector1.Y + vector2.Y, vector1.Z + vector2.Z);

    public static Vector3 operator -(Vector3 vector1, Vector3 vector2) =>
        new Vector3(vector1.X - vector2.X, vector1.Y - vector2.Y, vector1.Z - vector2.Z);

    public static Vector3 operator *(Vector3 vector1, float multiplier) =>
        new Vector3(vector1.X * multiplier, vector1.Y * multiplier, vector1.Z * multiplier);

    public static Vector3 operator /(Vector3 vector1, float divisor) =>
        new Vector3(vector1.X / divisor, vector1.Y / divisor, vector1.Z / divisor);

    public static bool operator ==(Vector3 vector1, Vector3 vector2) =>
        Compare(vector1, vector2);

    public static bool operator !=(Vector3 vector1, Vector3 vector2) =>
        (vector1 == vector2) == false;

    public static explicit operator Vector2(Vector3 vector3) =>
        new Vector2(vector3.X, vector3.Y);

    public static explicit operator Vector2Int(Vector3 vector3) =>
        new Vector2Int((int)vector3.X, (int)vector3.Y);

    public static explicit operator Vector3Int(Vector3 vector3) =>
        new Vector3Int((int)vector3.X, (int)vector3.Y, (int)vector3.Z);
}
