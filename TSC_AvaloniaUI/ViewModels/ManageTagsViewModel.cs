using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using DynamicData;
using ReactiveUI;
using Splat;
using TSC_AvaloniaUI.Models;
using TSC_AvaloniaUI.Services;

namespace TSC_AvaloniaUI.ViewModels;

public class ManageTagsViewModel: ViewModelBase {
    public ReactiveCommand<Tag, Unit> DeleteTagCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveChangesCommand { get; }
    public ObservableCollection<Tag> Tags { get; } = [];
    public List<Tag> RemovedTags { get; } = [];

    public ManageTagsViewModel() {
        var tagService = Locator.Current.GetService<ITagService>() ?? throw new NullReferenceException();
        
        DeleteTagCommand = ReactiveCommand.Create((Tag tag) => {
            RemovedTags.Add(tag);
            Tags.Remove(tag);
        });

        SaveChangesCommand = ReactiveCommand.Create(() => {
            foreach (var tag in RemovedTags) {
                tagService.AvailableTags.Remove(tag);
            }
            
            return Unit.Default;
        });

        Tags.AddRange(tagService.AvailableTags);
    }
}