#pragma warning disable CS0219 // Variable is assigned but its value is never used
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Windows.Input;
using DynamicData;
using ReactiveUI;
using TSC_AvaloniaUI.Models;

namespace TSC_AvaloniaUI.ViewModels;

public class AddTagToViewModel: ViewModelBase {
    public ReactiveCommand<Unit, IEnumerable<TagViewModel>> AddTagCommand { get; }
    public ObservableCollection<TagViewModel> Tags { get; } = new();
    public ObservableCollection<TagViewModel> SelectedTags { get; set; } = new();

    public AddTagToViewModel() {
        AddTagCommand = ReactiveCommand.Create(() => {
            return SelectedTags.AsEnumerable();
        });

    }

    public AddTagToViewModel UpdateTags(IEnumerable<Tag> tags) {
        Tags.AddRange(tags.Select(i => new TagViewModel(i)));
        return this;
    }
    
}