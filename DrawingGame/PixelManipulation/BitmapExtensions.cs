using Avalonia.Media;
using Avalonia.Media.Imaging;
using DrawingGame.Shapes.Abstractions;
using System.Collections.Generic;

namespace DrawingGame.PixelManipulation;

public static class BitmapExtensions
{
    public static int ConvertColor(Color color)
    {
        var col = 0;

        if (color.A != 0)
        {
            var a = color.A + 1;
            col = color.A << 24
                  | (byte)(color.R * a >> 8) << 16
                  | (byte)(color.G * a >> 8) << 8
                  | (byte)(color.B * a >> 8);
        }

        return col;
    }

    public static void DrawShapes(this WriteableBitmap bmp, params IShape[] shapes)
    {
        using var context = new BitmapContext(bmp, ReadWriteMode.ReadWrite);
        foreach (IShape shape in shapes)
        {
            foreach (var pixel in shape.GetShapePixels())
            {
                int x = pixel.X;
                int y = pixel.Y;
                Color color = pixel.Color;

                context.Pixels[y * context.Width + x] = ConvertColor(color);
            }
        }
    }

    public static void DrawShapes(this WriteableBitmap bmp, IEnumerable<IShape> shapes)
    {
        using var context = new BitmapContext(bmp, ReadWriteMode.ReadWrite);
        foreach (IShape shape in shapes)
        {
            foreach (var pixel in shape.GetShapePixels())
            {
                int x = pixel.X;
                int y = pixel.Y;
                Color color = pixel.Color;

                context.Pixels[y * context.Width + x] = ConvertColor(color);
            }
        }
    }

    public static Color GetPixel(this WriteableBitmap bmp, int x, int y)
    {
        using (var context = new BitmapContext(bmp, ReadWriteMode.ReadOnly))
        {
            var c = context.Pixels[y * context.Width + x];
            var a = (byte)(c >> 24);

            // Prevent division by zero
            int ai = a;
            if (ai == 0)
            {
                ai = 1;
            }

            // Scale inverse alpha to use cheap integer mul bit shift
            ai = (255 << 8) / ai;
            return Color.FromArgb(a,
                (byte)((c >> 16 & 0xFF) * ai >> 8),
                (byte)((c >> 8 & 0xFF) * ai >> 8),
                (byte)((c & 0xFF) * ai >> 8));
        }
    }
}
