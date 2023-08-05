using Avalonia.Controls;
using Avalonia.Media;
using System.Collections.Generic;

namespace DrawingGame.Shapes;

public class Rectangle : IShape
{
    private Coordinate topLeft, bottomRight;
    private Color color;

    private RectangleBoundary rectBoundary;

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

    public IEnumerable<Pixel> GetShapePixels()
    {
        foreach (Coordinate coordinate in rectBoundary.GetAllCoordinates())
        {
            yield return new(coordinate.X, coordinate.Y, color);
        }
    }
}