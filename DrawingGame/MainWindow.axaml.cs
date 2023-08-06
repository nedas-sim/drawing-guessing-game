using Avalonia.Controls;
using DrawingGame.Controllers.Brush;
using System;

namespace DrawingGame;

public partial class MainWindow : Window
{
    private readonly BrushController brushController;

    public MainWindow()
    {
        InitializeComponent();

        Image canvasImage = this.FindControl<Image>("ImageElement") ?? throw new Exception("ImageElement not found");

        brushController = new(canvasImage);
        canvasImage.PointerPressed += CanvasImage_PointerPressed;
        canvasImage.PointerMoved += CanvasImage_PointerMoved;
        canvasImage.PointerReleased += CanvasImage_PointerReleased;
    }

    private void CanvasImage_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        brushController.OnDragStart(e);
    }

    private void CanvasImage_PointerMoved(object? sender, Avalonia.Input.PointerEventArgs e)
    {
        brushController.OnDrag(e);
    }

    private void CanvasImage_PointerReleased(object? sender, Avalonia.Input.PointerReleasedEventArgs e)
    {
        brushController.OnDragEnd();
    }
}