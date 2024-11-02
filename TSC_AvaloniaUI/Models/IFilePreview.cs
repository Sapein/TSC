using System;
using System.IO;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Microsoft.VisualBasic;

namespace TSC_AvaloniaUI.Models;

public interface IFilePreview {
    public Bitmap? FilePreview { get;  }
}

public class ImageFilePreview : IFilePreview {

    public Bitmap? FilePreview { get; }

    public ImageFilePreview(string filePath) {
        using var file = File.OpenRead(filePath);

        try {
            // FilePreview = Bitmap.DecodeToWidth(file, 150);
            FilePreview = new Bitmap(file);
        }
        catch (ArgumentException) {
            FilePreview = null;
        }
        catch (NullReferenceException) {
            FilePreview = null;
        }
    }
}