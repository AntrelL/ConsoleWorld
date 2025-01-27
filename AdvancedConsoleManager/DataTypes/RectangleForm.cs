namespace ColdWind.AdvancedConsole.Graphics;

public struct RectangleForm
{
    public RectangleForm(
        int left = default, 
        int top = default, 
        int right = default, 
        int bottom = default)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }

    public int Left { get; set; }

    public int Top { get; set; }

    public int Right { get; set; }

    public int Bottom { get; set; }
}
