using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using CSharpFunctionalExtensions;
using DynamicData;
using ReactiveUI;
using TSC.AvaloniaUI.Models;

namespace TSC.AvaloniaUI.ViewModels.TagPages;

public class AddSubTagsPageViewModel : TagPageViewModel {
    public ReactiveCommand<Unit, Unit> AddSubTagCommand { get; }
    public TagViewModel Tag { get; set; } = null!;
    public ObservableCollection<TagViewModel> Tags { get; } = new();
    public ObservableCollection<TagViewModel> SelectedTags { get; set; } = new();
    public Interaction<Unit, Unit> FinishSubTagsInteraction { get; }


    public AddSubTagsPageViewModel() {
        FinishSubTagsInteraction = new();
        AddSubTagCommand = ReactiveCommand.CreateFromTask(async () => {
            _ = await FinishSubTagsInteraction.Handle(Unit.Default);
        });
    }

    public AddSubTagsPageViewModel UpdateTags(IEnumerable<TagViewModel> tags) {
        Tags.AddRange(tags);
        return this;
    }
}