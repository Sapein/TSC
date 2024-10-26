namespace TSC_AvaloniaUI.Models;

public class FileEntry {
    public Entry Entry { get;  }
    public string Path { get; set; } 
    public string FileName { get; set; }
    public string Extension { get; set; }
    
    public FileEntry(string path, Entry entry) {
        Path = path;
        FileName = System.IO.Path.GetFileName(path);
        Extension = System.IO.Path.GetExtension(path).Replace(".", "");
        Entry = entry;
    }
}