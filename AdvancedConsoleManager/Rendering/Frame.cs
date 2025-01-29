namespace ColdWind.AdvancedConsoleManager;

public class Frame
{
    private readonly Vector2Int _size;

    public Frame(Vector2Int size, Cell cellTemplate = default)
        : this(size.X, size.Y, cellTemplate)
    {
    }

    public Frame(int width, int height, Cell cellTemplate = default)
    {
        _size = new Vector2Int(width, height);
        Cells = new Cell[width, height];

        for (int i = 0; i < _size.X; i++)
        {
            for (int j = 0; j < _size.Y; j++)
            {
                Cells[i, j] = cellTemplate;
            }
        }
    }

    public Frame(Frame frame)
    {
        _size = frame.Size;
        Cells = new Cell[_size.X, _size.Y];

        for (int i = 0; i < _size.X; i++)
        {
            for (int j = 0; j < _size.Y; j++)
            {
                Cells[i, j] = frame.Cells[i, j];
            }
        }
    }

    public Cell[,] Cells { get; init; }

    public Vector2Int Size => _size.Copy();

    public Frame Copy => new Frame(this);

    public void Overlay(Frame topFrame)
    {
        for (int i = 0; i < _size.X; i++)
        {
            for (int j = 0; j < _size.Y; j++)
            {
                Cells[i, j].Overlay(topFrame.Cells[i, j]);
            }
        }
    }

    public struct Cell
    {
        public Cell(
            char symbol = default,
            Color foregroundColor = default,
            Color backgroundColor = default)
        {
            Symbol = symbol;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }

        public char Symbol { get; set; }

        public Color ForegroundColor { get; set; }

        public Color BackgroundColor { get; set; }

        public void Overlay(Cell topCell)
        {
            Symbol = topCell.Symbol == default ? Symbol : topCell.Symbol;
            ForegroundColor = OverlayColor(ForegroundColor, topCell.ForegroundColor);
            BackgroundColor = OverlayColor(BackgroundColor, topCell.BackgroundColor);
        }

        private Color OverlayColor(Color baseColor, Color topColor)
        {
            return topColor == Color.Transparent ? baseColor : topColor;
        }
    }
}
