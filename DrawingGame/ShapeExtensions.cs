using Avalonia;
using Avalonia.Controls;
using DrawingGame.Shapes;
using DrawingGame.Shapes.Abstractions;
using System;
using System.Collections.Generic;

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

    public static IEnumerable<Coordinate> GetPointsOnLine(int x1, int y1, int x2, int y2)
    {
        int dx = x2 - x1;
        int dy = y2 - y1;
        int steps = Math.Abs(dx) > Math.Abs(dy) ? Math.Abs(dx) : Math.Abs(dy);
        double xInc = (double)dx / steps;
        double yInc = (double)dy / steps;
        double x = x1;
        double y = y1;

        for (int i = 0; i < steps; i++)
        {
            Coordinate position = new((int)x, (int)y);

            yield return position;

            x += xInc;
            y += yInc;
        }
    }

    public static IEnumerable<Coordinate> GetPointsOnLine(Coordinate point1, Point point2)
        => GetPointsOnLine(point1.X, point1.Y, (int)point2.X, (int)point2.Y);

    public static IEnumerable<Coordinate> GetPointsOnLine(Coordinate point1, Coordinate point2)
        => GetPointsOnLine(point1.X, point1.Y, point2.X, point2.Y);
}