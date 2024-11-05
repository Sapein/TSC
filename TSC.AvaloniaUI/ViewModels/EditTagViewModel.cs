using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using TSC.AvaloniaUI.Models;
using TSC.AvaloniaUI.Services;

namespace TSC.AvaloniaUI.ViewModels;

public class EditTagViewModel : ViewModelBase {
    private readonly ITagService _tagService;
    private readonly SourceList<Tag> _currentTag = new();
    private Tag _tag = null!;
    public Tag Tag {
        get => _tag;
        set {
            SelectedParentTags.Clear();
            SelectedParentTags.AddRange(value.Parents);
            TagName = value.TagName;
            this.RaiseAndSetIfChanged(ref _tag, value);
            
            // There has to be a better way of doing this so we don't
            // Include the current tag when modifying tags.
            _currentTag.Clear();
            _currentTag.Add(value);
        }
    }

    private string _tagName = string.Empty;
    public string TagName {
        get => _tagName;
        set => this.RaiseAndSetIfChanged(ref _tagName, value);
    }


    public ReactiveCommand<Unit, Unit> SaveTagCommand { get; }
    private ReadOnlyObservableCollection<Tag> _tags;
    public ReadOnlyObservableCollection<Tag> Tags => _tags;
    public ObservableCollection<Tag> SelectedParentTags { get; set; } = [];

    public EditTagViewModel(ITagService tagService) {
        _tagService = tagService;
        _tags = null!;
        Tag = new() { TagName = string.Empty };
        SelectedParentTags.AddRange(Tag.Parents);

        tagService
            .AvailableTags
            .ToObservableChangeSet()
            .Except(_currentTag.Connect())
            .Except(tagService.UnavailableTags.Connect())
            .Bind(out _tags)
            .RepeatWhen(_ => this.WhenPropertyChanged(t => t.Tag))
            .Subscribe();


        SaveTagCommand = ReactiveCommand.Create(() => {
            foreach (var parent in SelectedParentTags) {
                parent.AddChildTag(Tag);
            }
            
            Tag.Parents = SelectedParentTags.AsEnumerable();
            Tag.TagName = TagName;
            tagService.AddTag(Tag);
            
            return Unit.Default;
        });
    }
}