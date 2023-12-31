﻿using Avalonia.Controls;
using System.Collections.Generic;

namespace DrawingGame.Shapes;

public record struct RectangleBoundary(Coordinate TopLeft, Coordinate BottomRight, Image CanvasImage)
{
    private readonly Coordinate _center = new((TopLeft.X + BottomRight.X) / 2, (TopLeft.Y + BottomRight.Y) / 2);
    public readonly Coordinate Center => _center;

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
