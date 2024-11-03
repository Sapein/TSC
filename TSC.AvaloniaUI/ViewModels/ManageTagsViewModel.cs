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
    private readonly SourceList<Tag> _removedTags = new();
    private readonly ReadOnlyObservableCollection<Tag> _tags;
    public ReadOnlyObservableCollection<Tag> Tags => _tags;

    public ManageTagsViewModel(ITagService? tagService = null) {
        tagService = Locator.Current.GetRequiredService(tagService);
        EditTagInteraction = new();
        
        
        DeleteTagCommand = ReactiveCommand.Create((Tag tag) => {
            _removedTags.Add(tag);
        });

        SaveChangesCommand = ReactiveCommand.Create(() => {
            tagService.RemoveTags(_removedTags.Items);
            
            return Unit.Default;
        });

        EditTagCommand = ReactiveCommand.Create(async (Tag tag) => {
            tagService.UnavailableTags.AddRange(_removedTags.Items);
            await EditTagInteraction.Handle(tag);
            tagService.UnavailableTags.Clear();
        });

        tagService 
            .AvailableTags
            .ToObservableChangeSet()
            .Except(_removedTags.Connect())
            .Bind(out _tags)
            .Subscribe();
    }
}