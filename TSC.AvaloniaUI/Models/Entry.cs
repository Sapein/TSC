using System.Collections.Generic;
using System.Linq;
using DynamicData;

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

    public bool HasAllTagsByName(IEnumerable<string> tagNames) {
        if (Tags.Count == 0) return false;
        if (Tags.Items.Where(t => t.Item1 == TagType.Negative).Any(t => tagNames.Contains(t.Item2.TagName))) return false;
        if (Tags.Items.All(t => t.Item1 == TagType.Positive)) return Tags.Items.All(t => tagNames.All(t.Item2.ContainsParentTagByName));

        var positiveRelationships = Tags
            .Items
            .Where(t => t.Item1 == TagType.Positive)
            .Where(t => tagNames.Any(t.Item2.ContainsParentTagByName))
            .SelectMany(t => t.Item2.FlattenParents())
            .ToList();
        
        var negativeRelationships = Tags
            .Items
            .Where(t => t.Item1 == TagType.Negative)
            .SelectMany(t => t.Item2.FlattenParents());

        foreach (var nTag in negativeRelationships) {
            positiveRelationships.Remove(nTag);
        }

        return tagNames.All(n => positiveRelationships.Select(t => t.TagName).Contains(n));
    }

    public bool HasAnyTagByName(IEnumerable<string> tagNames) {
        if (Tags.Count == 0) return false;
        if (Tags.Items.Where(t => t.Item1 == TagType.Negative).Any(t => tagNames.Contains(t.Item2.TagName))) return false;


        var positiveRelationships = Tags
            .Items
            .Where(t => t.Item1 == TagType.Positive)
            .Where(t => tagNames.Any(t.Item2.ContainsParentTagByName))
            .SelectMany(t => t.Item2.FlattenParents())
            .ToList();
        
        var negativeRelationships = Tags
            .Items
            .Where(t => t.Item1 == TagType.Negative)
            .SelectMany(t => t.Item2.FlattenParents());

        foreach (var nTag in negativeRelationships) {
            positiveRelationships.Remove(nTag);
        }

        return positiveRelationships.Count != 0 && tagNames.Any(n => positiveRelationships.Select(t => t.TagName).Contains(n));
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

