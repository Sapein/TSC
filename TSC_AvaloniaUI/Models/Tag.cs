using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TSC_AvaloniaUI.Models;

public class Tag {
    private readonly List<Tag> _children = new();
    private List<Tag> _parents = new();
    public required string TagName { get; set; }

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
