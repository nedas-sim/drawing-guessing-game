using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using DrawingGame.PixelManipulation;
using DrawingGame.Shapes;
using System.Collections.Generic;

namespace DrawingGame.Controllers;

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

    public void OnDragStart(PointerPressedEventArgs e)
    {
        pressed = true;

        Point position = e.GetPosition(_canvasImage);
        Circle circle = CreateCircle(position);
        lastDrawCircle = circle;
        _writeableBitmap.DrawShapes(circle);
        _canvasImage.InvalidateVisual();
    }

    public void OnDrag(PointerEventArgs e)
    {
        if (pressed is false)
        {
            return;
        }

        Point position = e.GetPosition(_canvasImage);

        _writeableBitmap.DrawShapes(GetCirclesBetweenDrawPoints(position));

        _canvasImage.InvalidateVisual();
    }

    public void OnDragEnd()
    {
        pressed = false;
        lastDrawCircle = null;
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

    private Circle CreateCircle(Point centerPoint)
        => new(centerPoint, Constants.BrushThickness, Color.FromRgb(0, 255, 0), _canvasImage);

    private Circle CreateCircle(Coordinate centerPoint)
        => new(centerPoint, Constants.BrushThickness, Color.FromRgb(0, 255, 0), _canvasImage);
}
