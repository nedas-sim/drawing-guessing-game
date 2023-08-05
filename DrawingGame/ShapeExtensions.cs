using Avalonia.Controls;
using DrawingGame.Shapes;
using System;

namespace DrawingGame;

public static class ShapeExtensions
{
    public static int CalculateManhattanDistance(this Coordinate point1, Coordinate point2)
    {
        int manhattanDistance = Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y);
        return manhattanDistance;
    }

    public static int CalculateDistanceSquared(this Coordinate point1, Coordinate point2)
    {
        int dx = point1.X - point2.X;
        int dy = point1.Y - point2.Y;

        return dx * dx + dy * dy;
    }

    public static IShape WrapWithCanvasSize(this IShape shape, Image canvas)
        => new CanvasSizeShapeDecorator(shape, canvas);

    public static bool IsWithinImageBoundaries(this Image image, int x, int y) =>
        x >= 0 && x < image.Width &&
        y >= 0 && y < image.Height;
}