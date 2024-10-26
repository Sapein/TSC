using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TSC_AvaloniaUI.Models;

public class Entry {
    public string Path { get; set; } 
    public string FileName { get; set; }
    public string Extension { get; set; }

    public IEnumerable<(TagType, Tag)> Tags { get; set; } = [];
    
    
    public Entry(string path) {
        Path = path;
        FileName = System.IO.Path.GetFileName(path);
        Extension = System.IO.Path.GetExtension(path).Replace(".", "");
    }
    
    
    public static Task<IEnumerable<Entry>> GetFilesFromPath(string path) {
        return Task.FromResult(Directory
            .EnumerateFiles(path)
            .Where(x => !string.IsNullOrEmpty(new DirectoryInfo(x).Extension))
            .Select(x => new Entry(x)));
    }
}

