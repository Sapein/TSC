using System;
using System.IO;
using System.Linq;
using CSharpFunctionalExtensions;
using TSC_AvaloniaUI.Models;

namespace TSC_AvaloniaUI.Loaders;

public static class PreviewLoader {
    public static Result<IFilePreview> GetFilePreviewFromPath(string path) {
        if (Directory.Exists(path)) return Result.Failure<IFilePreview>("File is a directory");
        if (!File.Exists(path)) return Result.Failure<IFilePreview>("File not found");
        
        if (IsTextFile(path)) {
            var result = TryCreatePreview(() => new TextFilePreview(path));
            if (result.IsSuccess) {
                return result;
            } 
        }

        return TryCreatePreview(() => new ImageFilePreview(path)).MapError(_ => "Unable to determine file type for preview");
    }

    private static Result<IFilePreview> TryCreatePreview(Func<IFilePreview> createCb) {
        try {
            return Result.Success(createCb());
        }
        catch (ArgumentException) {
            return Result.Failure<IFilePreview>("Unable to create preview");
        }
        catch (FileNotFoundException) {
            return Result.Failure<IFilePreview>("File not found");
        }
    }

    
    private static bool IsTextFile(string filePath) {
        byte[] bytes = new byte[8000];

        try {
            using var file = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var maxBytes = file.Read(bytes, 0, bytes.Length);

            var wasNull = false;

            foreach (var b in bytes.Take(maxBytes)) {
                switch (b) {
                    case Byte.MinValue when !wasNull:
                        wasNull = true;
                        break;
                    case Byte.MinValue when wasNull:
                        return false;
                    default:
                        wasNull = false;
                        break;
                }
            }

            return true;
        }
        catch (FileNotFoundException) {
            return false;
        }
    }
}
