using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using TSC.AvaloniaUI.Services;
using TSC.Splat.Extensions;

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


    public IEnumerable<Tag> FlattenParents() {
        if (_parents.Count == 0) {
            return new List<Tag> { this };
        }

        return Parents.SelectMany(parent => _parents).SelectMany(parent => parent.FlattenParents()).Prepend(this).Distinct();
    }
    
    public bool ContainsParentTagByName(string tagName) {
        return ContainsParentTag(Locator.Current.GetRequiredService<ITagService>().AvailableTags.FirstOrDefault(t => t.TagName == tagName));
    }
    
    public bool ContainsParentTag(Tag? tag) {
        return tag is not null && (tag == this || _parents.Any(p => p.ContainsParentTag(tag)));
    }

    public void AddChildTag(Tag tag) {
        _children.Add(tag);
    }

    public void RemoveParentTags(IEnumerable<Tag> tags) {
        _parents.RemoveAll(tags.Contains);
    }
    
    public void RemoveParentTag(Tag tag) {
        _parents.Remove(tag);
    }
    public void RemoveChildTags(IEnumerable<Tag> tags) {
        _children.RemoveAll(tags.Contains);
    }
    
    public void RemoveChildTag(Tag tag) {
        _children.Remove(tag);
    }
}

public enum TagType {
    Positive,
    Negative
}
