using System.Collections.Generic;

namespace DrawingGame.Shapes;

public interface IShape
{
    IEnumerable<Pixel> GetShapePixels();
}
