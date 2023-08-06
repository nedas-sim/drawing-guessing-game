using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using DrawingGame.PixelManipulation;
using DrawingGame.Shapes;
using System.Collections.Generic;

namespace DrawingGame.Controllers.Brush;

public enum BrushType
{
    Circle,
    Square,
}

public class BrushController
{
    #region Initialization
    private readonly Image _canvasImage;
    private readonly WriteableBitmap _writeableBitmap;

    public BrushController(Image canvasImage)
    {
        _canvasImage = canvasImage;

        _writeableBitmap = new WriteableBitmap(
            new PixelSize(Constants.CanvasWidth, Constants.CanvasHeight),
            new Vector(96, 96),
            Avalonia.Platform.PixelFormat.Bgra8888);
        _canvasImage.Source = _writeableBitmap;
    }
    #endregion

    bool pressed = false;
    Circle? lastDrawCircle = null;
    Rectangle? lastDrawRectangle = null;

    BrushType BRUSH_TYPE = BrushType.Square;

    public void OnDragStart(PointerPressedEventArgs e)
    {
        pressed = true;

        Point position = e.GetPosition(_canvasImage);

        if (BRUSH_TYPE == BrushType.Circle)
        {
            Circle circle = CreateCircle(position);
            lastDrawCircle = circle;
            _writeableBitmap.DrawShapes(circle);
        }
        else if (BRUSH_TYPE == BrushType.Square)
        {
            Rectangle rect = CreateRectangle(position);
            lastDrawRectangle = rect;
            _writeableBitmap.DrawShapes(rect);
        }

        _canvasImage.InvalidateVisual();
    }

    public void OnDrag(PointerEventArgs e)
    {
        if (pressed is false)
        {
            return;
        }

        Point position = e.GetPosition(_canvasImage);

        if (BRUSH_TYPE == BrushType.Circle)
        {
            _writeableBitmap.DrawShapes(GetCirclesBetweenDrawPoints(position));
        }
        else if (BRUSH_TYPE == BrushType.Square)
        {
            _writeableBitmap.DrawShapes(GetSquaresBetweenDrawPoints(position));
        }

        _canvasImage.InvalidateVisual();
    }

    public void OnDragEnd()
    {
        pressed = false;
        lastDrawCircle = null;
        lastDrawRectangle = null;
    }

    private IEnumerable<IShape> GetCirclesBetweenDrawPoints(Point currentCenter)
    {
        Circle circle = CreateCircle(currentCenter);

        if (lastDrawCircle is null)
        {
            lastDrawCircle = circle;
            yield return circle;
            yield break;
        }

        foreach (Coordinate coordinateOnLine in ShapeExtensions.GetPointsOnLine(lastDrawCircle.Value.Center, currentCenter))
        {
            yield return CreateCircle(coordinateOnLine);
        }

        lastDrawCircle = circle;
    }

    private IEnumerable<IShape> GetSquaresBetweenDrawPoints(Point currentCenter)
    {
        Rectangle rect = CreateRectangle(currentCenter);

        if (lastDrawRectangle is null)
        {
            lastDrawRectangle = rect;
            yield return rect;
            yield break;
        }

        foreach (Coordinate coordinateOnLine in ShapeExtensions.GetPointsOnLine(lastDrawRectangle.Value.Center, currentCenter))
        {
            yield return CreateRectangle(coordinateOnLine);
        }

        lastDrawRectangle = rect;
    }

    private Circle CreateCircle(Point centerPoint)
        => new(centerPoint, Constants.BrushThickness, Color.FromRgb(0, 255, 0), _canvasImage);

    private Circle CreateCircle(Coordinate centerPoint)
        => new(centerPoint, Constants.BrushThickness, Color.FromRgb(0, 255, 0), _canvasImage);

    private Rectangle CreateRectangle(Point centerPoint)
        => Rectangle.SquareFromCenterAndSideLength(centerPoint, Constants.BrushThickness, Color.FromRgb(0, 255, 0), _canvasImage);

    private Rectangle CreateRectangle(Coordinate centerPoint)
        => Rectangle.SquareFromCenterAndSideLength(centerPoint, Constants.BrushThickness, Color.FromRgb(0, 255, 0), _canvasImage);
}
