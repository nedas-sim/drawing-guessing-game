using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System.Collections.Generic;

namespace DrawingGame.Shapes;

public readonly struct Circle : IShape
{
    public Coordinate Center { get; }

    private readonly int radius;
    private readonly Color color;
    private readonly RectangleBoundary rectBoundary;

    public Circle(Coordinate center, int radius, Color color, Image canvasImage)
    {
        Center = center;

        this.radius = radius;
        this.color = color;
        Coordinate topLeft = new(center.X - radius, center.Y - radius);
        Coordinate bottomRight = new(center.X + radius, center.Y + radius);
        rectBoundary = new(topLeft, bottomRight, canvasImage);
    }

    public Circle(Point center, int radius, Color color, Image canvasImage) : this(new Coordinate(center), radius, color, canvasImage)
    {

    }

    public IEnumerable<Pixel> GetShapePixels()
    {
        int radiusSquared = radius * radius;

        foreach (Coordinate coordinate in rectBoundary.GetAllCoordinates())
        {
            int distanceSquared = coordinate.CalculateDistanceSquared(Center);
            if (distanceSquared < radiusSquared)
            {
                yield return new(coordinate.X, coordinate.Y, color);
            }
        }
    }
}
