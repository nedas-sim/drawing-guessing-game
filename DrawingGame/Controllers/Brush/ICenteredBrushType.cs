using Avalonia;
using DrawingGame.Shapes.Abstractions;
using System.Collections.Generic;

namespace DrawingGame.Controllers.Brush;

public interface ICenteredBrushType
{
    ICenteredShape OnDragStart(Point position);
    IEnumerable<ICenteredShape> GetShapesBetweenDrawPoints(Point currentCenter, ICenteredShape? lastDrawShape);
}
