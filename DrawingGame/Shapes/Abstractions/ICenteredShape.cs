namespace DrawingGame.Shapes.Abstractions;

public interface ICenteredShape : IShape
{
    Coordinate Center { get; }
}
