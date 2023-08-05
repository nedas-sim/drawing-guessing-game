using Avalonia.Controls;
using Avalonia.Media;

namespace DrawingGame.Shapes;

public record struct Pixel(int X, int Y, Color Color)
{
    public readonly bool IsWithinImageBoundaries(Image image) => image.IsWithinImageBoundaries(X, Y);
}