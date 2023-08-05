using Avalonia.Controls;
using System.Collections.Generic;

namespace DrawingGame.Shapes;

public record struct RectangleBoundary(Coordinate TopLeft, Coordinate BottomRight, Image CanvasImage)
{
    public readonly IEnumerable<Coordinate> GetAllCoordinates()
    {
        for (int x = TopLeft.X; x <= BottomRight.X; x++)
        {
            for (int y = TopLeft.Y; y <= BottomRight.Y; y++)
            {
                if (CanvasImage.IsWithinImageBoundaries(x, y))
                {
                    yield return new(x, y);
                }
            }
        }
    }
}
