using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TSC_AvaloniaUI.Models;

public class FileEntry {
    public FileEntry(string name, string path, string extension) {
        Name = name;
        Path = path;
        Extension = extension;
    }
    public string Name { get; }
    public string Path { get; } 
    public string Extension { get; }

    public IEnumerable<(TagType, Tag)> Tags { get; } = [];
}