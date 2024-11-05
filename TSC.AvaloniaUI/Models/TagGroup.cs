using System.Collections.Generic;
using CSharpFunctionalExtensions;
using DynamicData;
using ReactiveUI;

namespace TSC.AvaloniaUI.Models;

public class TagGroup: ReactiveObject {
    public Tag MainTag { get; }
    public SourceList<Tag> Tags { get;}

    public TagGroup(Tag mainTag, SourceList<Tag> tags) {
        MainTag = mainTag;
        Tags = tags;
    }

    public Result AddTags(IEnumerable<Tag> newTags) {
        Tags.AddRange(newTags);
        return Result.Success();
    }

    public Result RemoveTags(IEnumerable<Tag> removedTags) {
        Tags.RemoveMany(removedTags);
        return Result.Success();
    }
}