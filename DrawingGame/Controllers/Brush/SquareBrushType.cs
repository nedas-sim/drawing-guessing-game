using Avalonia;
using Avalonia.Media;
using DrawingGame.Shapes;

namespace DrawingGame.Controllers.Brush;

public class SquareBrushType : BaseCenteredBrushType<Rectangle>
{
    public SquareBrushType(BrushController brushController) : base(brushController)
    {
    }

    protected override Rectangle CreateShape(Point centerPoint)
        => Rectangle.SquareFromCenterAndSideLength(centerPoint, Constants.BrushThickness, Color.FromRgb(0, 255, 0), _brushController.CanvasImage);

    protected override Rectangle CreateShape(Coordinate centerPoint)
        => Rectangle.SquareFromCenterAndSideLength(centerPoint, Constants.BrushThickness, Color.FromRgb(0, 255, 0), _brushController.CanvasImage);
}