using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    
    public EntryViewModel(Entry entry) {
        _entry = entry;
        Tags.AddRange(_entry.Tags.Select(i => new TagViewModel(i.Item2, i.Item1)).Append<TagViewModelBase>(new AddTagViewModel(this)));
        
        AddTagCommand = ReactiveCommand.Create(() =>
        {
            var tag = new Tag { TagName = "New Tag!" };
            Tags.Insert(0, new TagViewModel(tag));
        });
    }

    public void AddTag(Tag tag, TagType relationship) {
        Tags.Add(new TagViewModel(tag, relationship));
    }

    public void AddTags(IEnumerable<TagViewModel> result) {
        var last = Tags.Last();
        Tags.Remove(last);
        Tags.AddRange(result);
        Tags.Add(last);
    }
}