using System.Globalization;
using System.IO;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace TSC.AvaloniaUI.Models;

public interface IFilePreview {
    public Bitmap? FilePreview { get;  }
}

public class ImageFilePreview : IFilePreview {

    public Bitmap? FilePreview { get; }

    public ImageFilePreview(string filePath) {
        using var file = File.OpenRead(filePath);

        // FilePreview = Bitmap.DecodeToHeight(file, 500);
        FilePreview = new(file);
    }
}

public class TextFilePreview : IFilePreview {
    public Bitmap? FilePreview { get; }

    public TextFilePreview(string filePath) {
        using var stream = new MemoryStream();
        var bitmap = new RenderTargetBitmap(new PixelSize().WithHeight(500).WithWidth(500));
        var drawingContext = bitmap.CreateDrawingContext();
            
        drawingContext
            .DrawRectangle(Brushes.FloralWhite, null, new Rect().WithHeight(500).WithWidth(500));
            
        drawingContext
            .DrawText(
                new(string.Join("\n", File.ReadAllLines(filePath)),
                    CultureInfo.CurrentCulture,
                    CultureInfo.CurrentCulture.TextInfo.IsRightToLeft ? FlowDirection.RightToLeft : FlowDirection.LeftToRight,
                    Typeface.Default,
                    20,
                    Brushes.Black),
                new(20, 0));

        bitmap.Save(stream);
        stream.Seek(0, SeekOrigin.Begin);
        FilePreview = new(stream);
    }
}