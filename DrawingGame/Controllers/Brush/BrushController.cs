using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using DrawingGame.PixelManipulation;
using DrawingGame.Shapes.Abstractions;
using System.Collections.Generic;
using System.Linq;

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

    public Image CanvasImage => _canvasImage;

    public BrushController(Image canvasImage)
    {
        _canvasImage = canvasImage;

        _writeableBitmap = new WriteableBitmap(
            new PixelSize(Constants.CanvasWidth, Constants.CanvasHeight),
            new Vector(96, 96),
            Avalonia.Platform.PixelFormat.Bgra8888);
        _canvasImage.Source = _writeableBitmap;

        brushType = new SquareBrushType(this);
        //brushType = new CircleBrushType(this);
    }
    #endregion

    readonly ICenteredBrushType brushType;

    bool pressed = false;
    ICenteredShape? lastDrawShape = null;

    public void OnDragStart(PointerPressedEventArgs e)
    {
        pressed = true;

        Point position = e.GetPosition(_canvasImage);
        ICenteredShape shape = brushType.OnDragStart(position);
        lastDrawShape = shape;
        _writeableBitmap.DrawShapes(shape);

        _canvasImage.InvalidateVisual();
    }

    public void OnDrag(PointerEventArgs e)
    {
        if (pressed is false)
        {
            return;
        }

        Point position = e.GetPosition(_canvasImage);

        List<ICenteredShape> shapesToDraw = brushType
            .GetShapesBetweenDrawPoints(position, lastDrawShape)
            .ToList();

        if (shapesToDraw.Any() is false)
        {
            return;
        }

        _writeableBitmap.DrawShapes(shapesToDraw);
        lastDrawShape = shapesToDraw[^1];

        _canvasImage.InvalidateVisual();
    }

    public void OnDragEnd()
    {
        pressed = false;
        lastDrawShape = null;
    }
}
