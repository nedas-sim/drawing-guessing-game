using System.Collections.Generic;

namespace DrawingGame.Shapes.Abstractions;

public interface IShape
{
    IEnumerable<Pixel> GetShapePixels();
}
