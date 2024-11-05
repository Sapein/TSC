#pragma warning disable CS0219 // Variable is assigned but its value is never used
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using DynamicData;
using ReactiveUI;
using TSC.AvaloniaUI.Models;

namespace TSC.AvaloniaUI.ViewModels.TagPages;

public class AddTagToPageViewModel: TagPageViewModel {
    public ReactiveCommand<Unit, IEnumerable<TagViewModel>> AddTagCommand { get; }
    public ObservableCollection<TagViewModel> Tags { get; } = new();
    public ObservableCollection<TagViewModel> SelectedTags { get; set; } = new();

    public AddTagToPageViewModel() {
        AddTagCommand = ReactiveCommand.Create(() => SelectedTags.AsEnumerable());

    }

    public AddTagToPageViewModel UpdateTags(IEnumerable<Tag> tags) {
        Tags.AddRange(tags.Select(i => new TagViewModel(i)));
        return this;
    }
    
}