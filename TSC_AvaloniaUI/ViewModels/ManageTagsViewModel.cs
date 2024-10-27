using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using DynamicData;
using ReactiveUI;
using TSC_AvaloniaUI.Models;

namespace TSC_AvaloniaUI.ViewModels;

public class ManageTagsViewModel: ViewModelBase {
    public ReactiveCommand<Tag, Unit> DeleteTagCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveChangesCommand { get; }
    public ObservableCollection<Tag> Tags { get; } = [];
    public List<Tag> RemovedTags { get; } = [];

    public ManageTagsViewModel(List<Tag> tags) {
        
        DeleteTagCommand = ReactiveCommand.Create((Tag tag) => {
            Tags.Remove(tag);
            RemovedTags.Add(tag);
        });

        SaveChangesCommand = ReactiveCommand.Create(() => {
            foreach (var tag in RemovedTags) {
                tags.Remove(tag);
            }
            
            return Unit.Default;
        });

        Tags.AddRange(tags);
    }
}