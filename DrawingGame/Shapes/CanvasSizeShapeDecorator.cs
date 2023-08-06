using Avalonia.Controls;
using DrawingGame.Shapes.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace DrawingGame.Shapes;

public class CanvasSizeShapeDecorator : IShape
{
    private readonly IShape shape;
    private readonly Image canvasImage;

    public CanvasSizeShapeDecorator(IShape shape, Image canvasImage)
    {
        this.shape = shape;
        this.canvasImage = canvasImage;
    }

    public IEnumerable<Pixel> GetShapePixels() => 
        shape
            .GetShapePixels()
            .Where(pixel => pixel.IsWithinImageBoundaries(canvasImage));
}
