using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using DrawingGame.Shapes.Abstractions;
using System.Collections.Generic;

namespace DrawingGame.Shapes;

public readonly struct Rectangle : ICenteredShape
{
    public readonly Coordinate Center => rectBoundary.Center;

    private readonly Coordinate topLeft;
    private readonly Coordinate bottomRight;
    private readonly Color color;

    private readonly RectangleBoundary rectBoundary;

    private Rectangle(int topLeftX, int topLeftY, int height, int width, Color color, Image canvasImage)
    {
        topLeft = new(topLeftX, topLeftY);
        bottomRight = new(topLeftX + width, topLeftY + height);
        this.color = color;

        rectBoundary = new(topLeft, bottomRight, canvasImage);
    }

    public static Rectangle FromTopLeftAndDimensions(int topLeftX, int topLeftY, int height, int width, Color color, Image canvasImage)
    {
        return new(topLeftX, topLeftY, height, width, color, canvasImage);
    }

    public static Rectangle FromTopLeftAndDimensions(Coordinate topLeft, int height, int width, Color color, Image canvasImage)
    {
        return FromTopLeftAndDimensions(topLeft.X, topLeft.Y, height, width, color, canvasImage);
    }

    public static Rectangle SquareFromCenterAndSideLength(Point centerPoint, int sideLength, Color color, Image canvasImage)
    {
        int topLeftX = (int)centerPoint.X - sideLength / 2;
        int topLeftY = (int)centerPoint.Y - sideLength / 2;

        return FromTopLeftAndDimensions(topLeftX, topLeftY, sideLength, sideLength, color, canvasImage);
    }

    public static Rectangle SquareFromCenterAndSideLength(Coordinate centerPoint, int sideLength, Color color, Image canvasImage)
    {
        int topLeftX = centerPoint.X - sideLength / 2;
        int topLeftY = centerPoint.Y - sideLength / 2;

        return FromTopLeftAndDimensions(topLeftX, topLeftY, sideLength, sideLength, color, canvasImage);
    }

    public IEnumerable<Pixel> GetShapePixels()
    {
        foreach (Coordinate coordinate in rectBoundary.GetAllCoordinates())
        {
            yield return new(coordinate.X, coordinate.Y, color);
        }
    }
}