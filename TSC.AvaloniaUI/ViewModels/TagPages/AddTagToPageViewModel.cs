#pragma warning disable CS0219 // Variable is assigned but its value is never used
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using CSharpFunctionalExtensions;
using DynamicData;
using ReactiveUI;
using Splat;
using TSC.AvaloniaUI.Models;
using TSC.AvaloniaUI.Views.TagPages;

namespace TSC.AvaloniaUI.ViewModels.TagPages;

public class AddTagToPageViewModel : TagPageViewModel {
    public ReactiveCommand<Unit, IEnumerable<TagViewModel>> AddTagCommand { get; }
    public ReactiveCommand<TagViewModel, Unit> AddSubTagCommand { get; }
    public ObservableCollection<TagViewModel> Tags { get; } = new();
    public ObservableCollection<TagViewModel> SelectedTags { get; set; } = new();

    public Interaction<TagViewModel, Unit> AddSubTagsInteraction { get; }


    public AddTagToPageViewModel() {
        AddTagCommand = ReactiveCommand.Create(() => SelectedTags.AsEnumerable());
        AddSubTagsInteraction = new();
        AddSubTagCommand = ReactiveCommand.CreateFromTask(async (TagViewModel tag) => {
            // this.WhenActivated(action => action(ViewModel!.AddTagCommand.Select(x => x).Subscribe(Close)));
            
            _ = await AddSubTagsInteraction.Handle(tag);
        });
    }

    public AddTagToPageViewModel UpdateTags(IEnumerable<Tag> tags) {
        Tags.AddRange(tags.Select(i => new TagViewModel(i)));
        return this;
    }
}