using System;
using System.Collections.Generic;
using System.Reactive;
using System.Linq;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using TSC_AvaloniaUI.Loaders;
using TSC_AvaloniaUI.Models;
using TSC_AvaloniaUI.Services;

namespace TSC_AvaloniaUI.ViewModels;

public class EntryViewModel : ViewModelBase {
    private readonly Entry _entry;
    
    // This exists so we can create a joint list.
    private readonly SourceList<TagViewModelBase> _addTag = new();
    
    public string Extension => _entry.Extension;
    public string Name => _entry.Name;
    public string Path => _entry.Path;
    private IFilePreview? _filePreview;
    public IFilePreview? Preview { get => _filePreview; set => this.RaiseAndSetIfChanged(ref _filePreview, value);}


    // We use TagViewModel base explicitly because we need to add in a final tag, that is always at the end. If we
    // did not, we have no way of actually adding the + button.
    public ObservableCollectionExtended<TagViewModelBase> Tags { get; } = [];

    private ReactiveCommand<TagViewModel, Unit> RemoveTagCommand { get; }
    
    public EntryViewModel(Entry entry) {
        _entry = entry;
        _addTag.Add(new AddTagViewModel(this));
        
        RemoveTagCommand = ReactiveCommand.Create((TagViewModel tag) => {
            _entry.RemoveTag(tag.Tag);
        });

        _entry.Tags
            .Connect()
            .Transform(TagViewModelBase (t) => {
                var tag = new TagViewModel(t.Item2, t.Item1) {
                    RemoveTagCommand = RemoveTagCommand,
                };

                return tag;
            })
            .Or(_addTag.Connect())
            .Bind(Tags)
            .Subscribe();


        Preview = PreviewLoader.GetFilePreviewFromPath(Path) switch {
            {IsSuccess: true, Value: var value} => value,
            _ => null,
        };
    }
    
    public void AddTags(IEnumerable<TagViewModel> result) {
        _addTag.RemoveAt(0);
        _entry.AddTags(result.Select(i => (i.TagType, i.Tag)));
        _addTag.Add(new AddTagViewModel(this));
    }
}