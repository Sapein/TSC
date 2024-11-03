using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using Splat;
using TSC.AvaloniaUI.Models;
using TSC.AvaloniaUI.Services;
using TSC.Splat.Extensions;

namespace TSC.AvaloniaUI.ViewModels;

public class ManageTagsViewModel: ViewModelBase {
    public ReactiveCommand<Tag, Unit> DeleteTagCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveChangesCommand { get; }
    
    public Interaction<Tag, Unit> EditTagInteraction { get; }
    public ICommand EditTagCommand { get; }
    private readonly ReadOnlyObservableCollection<Tag> _tags;
    public ReadOnlyObservableCollection<Tag> Tags => _tags;

    public ManageTagsViewModel(ITagService? tagService = null) {
        tagService = Locator.Current.GetRequiredService(tagService);
        EditTagInteraction = new();
        
        var removedTags = tagService.UnavailableTags;
        
        DeleteTagCommand = ReactiveCommand.Create((Tag tag) => {
            removedTags.Add(tag);
        });

        SaveChangesCommand = ReactiveCommand.Create(() => {
            foreach (var tag in removedTags.Items) {
                tagService.RemoveTag(tag);
            }
            
            return Unit.Default;
        });

        EditTagCommand = ReactiveCommand.Create(async (Tag tag) => {
            await EditTagInteraction.Handle(tag);
        });

        tagService 
            .AvailableTags
            .ToObservableChangeSet()
            .Except(removedTags.Connect())
            .Bind(out _tags)
            .Subscribe();
    }
}