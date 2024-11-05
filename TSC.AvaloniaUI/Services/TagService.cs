using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using DynamicData;
using DynamicData.Binding;
using TSC.AvaloniaUI.Models;

namespace TSC.AvaloniaUI.Services;


public interface ITagService {
    public ObservableCollectionExtended<Tag> AvailableTags { get; }
    public SourceList<Tag> UnavailableTags { get; }

    public bool TagExists(Tag tag);
    public Result ReplaceTag(Tag originalTag, Tag updatedTag);
    
    public Result AddTag(Tag tag);

    public Result RemoveTags(IEnumerable<Tag> tags);
    public Result RemoveTag(Tag tag);
}


public class TagService: ITagService {
    public ObservableCollectionExtended<Tag> AvailableTags { get; } = new();

    public SourceList<Tag> UnavailableTags { get; } = new();

    public bool TagExists(Tag tag) {
        return AvailableTags.Contains(tag) || AvailableTags.Select(i => i.Id).Contains(tag.Id);
    }
    
    public Result ReplaceTag(Tag originalTag, Tag updatedTag) {
        if (!TagExists(originalTag)) return Result.Failure("Tag not found");

        try {
            AvailableTags.Replace(originalTag, updatedTag);
        } catch (ArgumentNullException) {
            return Result.Failure("Unable to update tag!");
        }

        return Result.Success();
    }

    public Result AddTag(Tag tag) {
        if (TagExists(tag)) {
            return Result.Failure("Tag already exists!");
        }

        AvailableTags.Add(tag);
        return Result.Success();
    }

    public Result RemoveTags(IEnumerable<Tag> tags) {
        var itemsToRemove = tags.ToList();
        
        foreach (var tag in AvailableTags) {
            tag.RemoveChildTags(itemsToRemove);
            tag.RemoveParentTags(itemsToRemove);
        }
        
        AvailableTags.RemoveMany(itemsToRemove);
        return Result.Success();
    }
    
    public Result RemoveTag(Tag removedTag) {
        if (!TagExists(removedTag)) {
            return Result.Failure("Tag does not exist");
        }
        

        foreach (var tag in AvailableTags) {
            tag.RemoveChildTag(tag);
            tag.RemoveParentTag(tag);
        }
        
        AvailableTags.Remove(removedTag);
        return Result.Success();
    }
}