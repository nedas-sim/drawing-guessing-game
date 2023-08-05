using Avalonia;

namespace DrawingGame.Shapes;

public record struct Coordinate(int X, int Y)
{
    public Coordinate(Point point) : this((int)point.X, (int)point.Y)
    {
    }
}
