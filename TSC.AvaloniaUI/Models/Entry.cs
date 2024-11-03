using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using DynamicData;
using TSC.AvaloniaUI.Views;

namespace TSC.AvaloniaUI.Models;

public class Entry {
    public string Name { get; }
    public string Path { get; } 
    public string Extension { get; }


    public SourceList<(TagType, Tag)> Tags { get; } = new();
    
    public Entry(string path) {
        Name = System.IO.Path.GetFileName(path);
        Path = path;
        Extension = System.IO.Path.GetExtension(path).Replace(".", "");
    }
    
    public void AddTag((TagType, Tag) tag) {
        Tags.Add(tag);
    }

    public void AddTags(IEnumerable<(TagType, Tag)> tags) {
        Tags.AddRange(tags);
    }
    
    public void RemoveTag(Tag tag) {
        var idx = Tags.Items.Select(x => x.Item2).IndexOf(tag);
        Tags.RemoveAt(idx);
    }
}

