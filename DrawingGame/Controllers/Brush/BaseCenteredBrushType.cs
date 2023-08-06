using Avalonia;
using DrawingGame.Shapes;
using DrawingGame.Shapes.Abstractions;
using System.Collections.Generic;

namespace DrawingGame.Controllers.Brush;

public abstract class BaseCenteredBrushType<TShape> : ICenteredBrushType
    where TShape : ICenteredShape
{
    protected readonly BrushController _brushController;

    public BaseCenteredBrushType(BrushController brushController)
    {
        _brushController = brushController;
    }

    public ICenteredShape OnDragStart(Point position)
    {
        TShape shape = CreateShape(position);
        return shape;
    }

    public IEnumerable<ICenteredShape> GetShapesBetweenDrawPoints(Point currentCenter, ICenteredShape? lastDrawShape)
    {
        TShape circle = CreateShape(currentCenter);

        if (lastDrawShape is null)
        {
            yield return circle;
            yield break;
        }

        foreach (Coordinate coordinateOnLine in ShapeExtensions.GetPointsOnLine(lastDrawShape.Center, currentCenter))
        {
            yield return CreateShape(coordinateOnLine);
        }
    }

    protected abstract TShape CreateShape(Point centerPoint);
    protected abstract TShape CreateShape(Coordinate centerPoint);
}
