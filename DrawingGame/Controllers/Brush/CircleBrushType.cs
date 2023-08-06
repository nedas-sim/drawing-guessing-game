using Avalonia;
using Avalonia.Media;
using DrawingGame.Shapes;

namespace DrawingGame.Controllers.Brush;

public class CircleBrushType : BaseCenteredBrushType<Circle>
{
    public CircleBrushType(BrushController brushController) : base(brushController)
    {
    }

    protected override Circle CreateShape(Point centerPoint)
        => new(centerPoint, Constants.BrushThickness, Color.FromRgb(0, 255, 0), _brushController.CanvasImage);

    protected override Circle CreateShape(Coordinate centerPoint)
        => new(centerPoint, Constants.BrushThickness, Color.FromRgb(0, 255, 0), _brushController.CanvasImage);
}
