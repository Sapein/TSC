using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
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
    private readonly ITagService _tagService;
    public ReactiveCommand<Tag, Unit> DeleteTagCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveChangesCommand { get; }
    
    public Interaction<Tag, Unit> EditTagInteraction { get; }
    public ICommand EditTagCommand { get; }
    private readonly SourceList<Tag> _removedTags = new();
    private ReadOnlyObservableCollection<Tag> _tags;
    public ReadOnlyObservableCollection<Tag> Tags => _tags;
    private string _searchText = string.Empty;
    public string SearchText { get => _searchText; set => this.RaiseAndSetIfChanged(ref _searchText, value); }

    public ManageTagsViewModel(ITagService? tagService = null) {
        _tagService = Locator.Current.GetRequiredService(tagService);
        EditTagInteraction = new();
        
        
        DeleteTagCommand = ReactiveCommand.Create((Tag tag) => {
            _removedTags.Add(tag);
        });

        SaveChangesCommand = ReactiveCommand.Create(() => {
            _tagService.RemoveTags(_removedTags.Items);
            
            return Unit.Default;
        });

        EditTagCommand = ReactiveCommand.Create(async (Tag tag) => {
            _tagService.UnavailableTags.AddRange(_removedTags.Items);
            await EditTagInteraction.Handle(tag);
            _tagService.UnavailableTags.Clear();
        });

        _tagService 
            .AvailableTags
            .ToObservableChangeSet()
            .AutoRefreshOnObservable(_ => this.WhenValueChanged(m => m.SearchText))
            .Filter(t => t.TagName.Contains(SearchText))
            .Except(_removedTags.Connect())
            .Bind(out _tags)
            .Subscribe();
    }
}