using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using DrawingGame.Shapes;
using System;
using System.Collections.Generic;

namespace DrawingGame;

public partial class MainWindow : Window
{
    const int BRUSH_THICKNESS = 10;

    private Image canvasImage;
    private WriteableBitmap writeableBitmap;
    private int canvasWidth = 800;
    private int canvasHeight = 600;

    public MainWindow()
    {
        InitializeComponent();

        canvasImage = this.FindControl<Image>("ImageElement") ?? throw new Exception("ImageElement not found");
        writeableBitmap = new WriteableBitmap(new PixelSize(canvasWidth, canvasHeight), new Vector(96, 96), Avalonia.Platform.PixelFormat.Bgra8888);
        canvasImage.Source = writeableBitmap;

        List<IShape> shapes = new()
        {
            Rectangle.FromTopLeftAndDimensions(200, 200, 200, 100, Color.FromRgb(0, 0, 0), canvasImage),
            new Circle(new Coordinate(400, 300), 100, Color.FromRgb(255, 0, 0), canvasImage),
        };

        writeableBitmap.DrawShapes(shapes);

        canvasImage.PointerPressed += CanvasImage_PointerPressed;
        canvasImage.PointerMoved += CanvasImage_PointerMoved;
        canvasImage.PointerReleased += CanvasImage_PointerReleased;
    }

    

    bool pressed = false;
    Circle? lastDrawCircle = null;

    private void CanvasImage_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        pressed = true;

        Point position = e.GetPosition(canvasImage);
        Circle circle = new(position, BRUSH_THICKNESS, Color.FromRgb(0, 255, 0), canvasImage);
        writeableBitmap.DrawShapes(circle);
        canvasImage.InvalidateVisual();
    }

    private void CanvasImage_PointerMoved(object? sender, Avalonia.Input.PointerEventArgs e)
    {
        if (pressed is false)
        {
            return;
        }

        Point position = e.GetPosition(canvasImage);

        writeableBitmap.DrawShapes(GetCirclesBetweenDrawPoints(position));
        
        canvasImage.InvalidateVisual();
    }

    private IEnumerable<IShape> GetCirclesBetweenDrawPoints(Point currentCenter)
    {
        

        Circle circle = new(currentCenter, BRUSH_THICKNESS, Color.FromRgb(0, 255, 0), canvasImage);
        yield return circle;

        if (lastDrawCircle is null)
        {
            lastDrawCircle = circle;
            yield break;
        }

        Coordinate prevCircleCenter = lastDrawCircle.Value.Center;

        int dx = (int)currentCenter.X - prevCircleCenter.X;
        int dy = (int)currentCenter.Y - prevCircleCenter.Y;
        
        int steps = Math.Abs(dx) > Math.Abs(dy) ? Math.Abs(dx) : Math.Abs(dy);
        double xInc = (double)dx / steps;
        double yInc = (double)dy / steps;

        double x = prevCircleCenter.X;
        double y = prevCircleCenter.Y;

        for (int i = 0; i < steps; i++)
        {
            Coordinate position = new((int)x, (int)y);
            circle = new(position, BRUSH_THICKNESS, Color.FromRgb(0, 255, 0), canvasImage);
            
            yield return circle;

            x += xInc;
            y += yInc;
        }

        lastDrawCircle = circle;
    }

    private void CanvasImage_PointerReleased(object? sender, Avalonia.Input.PointerReleasedEventArgs e)
    {
        pressed = false;
        lastDrawCircle = null;
    }
}