namespace ColdWind.AdvancedConsoleManager.Internal;

public abstract class Vector
{
    protected static bool Compare<T>(T vector1, T vector2)
        where T : Vector
    {
        return vector1 is null ? vector2 is null : vector1.Equals(vector2);
    }

    protected float GetMagnitude(params float[] coordinates)
    {
        return (float)Math.Sqrt(GetSquaredMagnitude(coordinates));
    }

    protected float GetSquaredMagnitude(params float[] coordinates)
    {
        return coordinates.Sum(x => x * x);
    }
}
