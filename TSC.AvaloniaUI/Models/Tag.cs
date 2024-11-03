using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ReactiveUI;

namespace TSC.AvaloniaUI.Models;

public class Tag: ReactiveObject {
    internal readonly Guid Id = Guid.NewGuid(); 
    
    private readonly List<Tag> _children = new();
    private List<Tag> _parents = new();
    private string _name = null!;
    public required string TagName { get => _name; set => this.RaiseAndSetIfChanged(ref _name, value); }

    public IEnumerable<Tag> Parents {
        get => _parents;
        set => _parents = value.ToList();
    }

    public IEnumerable<Tag> Children => _children;

    public void AddChildTag(Tag tag) {
        _children.Add(tag);
    }

    public void RemoveChildTag(Tag tag) {
        _children.Remove(tag);
    }
}

public enum TagType {
    Positive,
    Negative
}
