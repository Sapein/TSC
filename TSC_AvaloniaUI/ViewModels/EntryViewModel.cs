using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using TSC_AvaloniaUI.Models;
using TSC_AvaloniaUI.Views;

namespace TSC_AvaloniaUI.ViewModels;

public class EntryViewModel : ViewModelBase {
    private readonly Entry _entry;
    
    public string Extension => _entry.Extension;
    public string Name => _entry.FileName;
    
    // We use TagViewModel base explicitly because we need to add in a final tag, that is always at the end. If we
    // did not, we have no way of actually adding the + button.
    public ObservableCollection<TagViewModelBase> Tags { get; } = new();
    
    public ICommand AddTagCommand { get; }
    private ReactiveCommand<TagViewModel, Unit> RemoveTagCommand { get; }
    
    public EntryViewModel(Entry entry) {
        _entry = entry;
        
        AddTagCommand = ReactiveCommand.Create(() => {
            var tag = new TagViewModel(new Tag { TagName = "New Tag!" });
            tag.RemoveTagCommand = RemoveTagCommand;
            Tags.Insert(0, tag);
        });

        RemoveTagCommand = ReactiveCommand.Create((TagViewModel tag) => {
            Tags.Remove(tag);
        });
        
        Tags.AddRange(_entry.Tags.Select(i => new TagViewModel(i.Item2, i.Item1)).Select(i => { i.RemoveTagCommand = RemoveTagCommand; return i; }).Append<TagViewModelBase>(new AddTagViewModel(this)));
    }

    public void RemoveInvalidTags(IEnumerable<Tag> tags) {
        var tagsList = tags.ToList();
        foreach (var tag in Tags.Where(x => x is TagViewModel).Cast<TagViewModel>()) {
            if (tagsList.Contains(tag.Tag)) continue;
            
            Tags.Remove(tag);
        }
    }
    
    public void AddTags(IEnumerable<TagViewModel> result) {
        var last = Tags.Last();
        Tags.Remove(last);
        Tags.AddRange(result.Select(i => { i.RemoveTagCommand = RemoveTagCommand; return i; }));
        Tags.Add(last);
    }
}