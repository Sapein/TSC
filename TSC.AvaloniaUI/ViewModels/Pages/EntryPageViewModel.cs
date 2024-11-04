using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using System.Windows.Input;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using Splat;
using TSC.AvaloniaUI.Models;
using TSC.AvaloniaUI.Services;
using TSC.Splat.Extensions;

namespace TSC.AvaloniaUI.ViewModels.Pages;

public class EntryPageViewModel: PageViewModel {
    public bool IsBusy { get; set; }
    private readonly IEntryService _entryService;
    private readonly ITagService _tagService;

    private EntryViewModel? _selectedEntry;
    private ReadOnlyObservableCollection<TagViewModelBase>? _tags;
    private string _searchText = string.Empty;
    public string SearchText { get => _searchText; set => this.RaiseAndSetIfChanged(ref _searchText, value); }

    private SearchModeEnum _searchMode = SearchModeEnum.TagAnd;
    public SearchModeEnum SearchMode { get => _searchMode; set => this.RaiseAndSetIfChanged(ref _searchMode, value); }
    
    public ReactiveCommand<Unit, Unit> SearchCommand { get; }


    private ReadOnlyObservableCollection<EntryViewModel>? _files;
    public ReadOnlyObservableCollection<EntryViewModel>? Files => _files;
    public ReadOnlyObservableCollection<TagViewModelBase>? Tags => _tags;
    
    public Interaction<Unit, Unit> CreateTagInteraction { get; }
    public Interaction<IEnumerable<Tag>, IEnumerable<TagViewModel>> AddTagInteraction { get; }
    public Interaction<Unit, Unit> ManageTagsInteraction { get; }
    public ICommand CreateTagCommand { get; }
    public ICommand AddTagCommand { get; }
    public ICommand ManageTagsCommand { get; }
    
    public EntryViewModel? SelectedEntry {
        get => _selectedEntry;
        set {
            this.RaiseAndSetIfChanged(ref _selectedEntry, value);

            if (value != null) {
                _selectedEntry?.Tags.ToObservableChangeSet().Bind(out _tags).Subscribe();
            } else {
                _tags = null;
            }

            this.RaisePropertyChanged(nameof(Tags));
        }
    }
    
    public EntryPageViewModel(ITagService? tagService = null, IEntryService? entryService = null) {
        CreateTagInteraction = new();
        AddTagInteraction = new();
        ManageTagsInteraction = new();

        _tagService = Locator.Current.GetRequiredService(tagService);
        _entryService = Locator.Current.GetRequiredService(entryService);

        CreateTagCommand = ReactiveCommand.Create(async () => {
            await CreateTagInteraction.Handle(Unit.Default);
        });

        ManageTagsCommand = ReactiveCommand.Create(async () => {
            try {
                await ManageTagsInteraction.Handle(Unit.Default);
            } catch (Exception) {
                // ignored
            }
            await _entryService.RemoveInvalidTags();
        });

        SearchCommand = ReactiveCommand.Create(() => Unit.Default);
        
        AddTagCommand = ReactiveCommand.Create(async () =>
        {
            var tags = tagService!.AvailableTags.Where(x => !SelectedEntry?.Tags.Select(vm => (vm as TagViewModel)?.Tag).Where(t => t != null).Contains(x) ?? false);
            var result = (await AddTagInteraction.Handle(tags)).ToList();

            if (result.Count != 0) {
                SelectedEntry?.AddTags(result);
            }
        });

        RxApp.TaskpoolScheduler.Schedule(LoadDir);
    }
    
    private async void LoadDir() {
        try {
            await _entryService.LoadEntries(Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)));
        } catch (Exception) {
            try {
                await _entryService.LoadEntries(Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.Personal)));
            }
            catch (Exception) {
                await _entryService.LoadEntries(Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)));
            }
        }

        _entryService
            .Entries
            .ToObservableChangeSet()
            .AutoRefreshOnObservable(_ => this.WhenPropertyChanged(t => t.SearchText).Throttle(TimeSpan.FromMilliseconds(400)))
            .AutoRefreshOnObservable(_ => this.WhenPropertyChanged(t => t.SearchMode).Throttle(TimeSpan.FromMilliseconds(100)))
            .Filter(FilterEntries)
            .Transform(x => new EntryViewModel(x))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out _files)
            .Subscribe();
    }

    private bool FilterEntries(Entry entry) {
        if (string.IsNullOrWhiteSpace(SearchText)) {
            if (SearchMode == SearchModeEnum.AnyTag) return entry.Tags.Count > 0;
            if (SearchMode == SearchModeEnum.NoTag) return entry.Tags.Count == 0;
            return true;
        }
        
        return SearchMode switch {
            SearchModeEnum.TagAnd => entry.HasAllTagsByName(SearchText.Split(",").Select(n => n.Normalize()).Where(_tagService.AvailableTags.Select(x => x.TagName).Contains)),
            SearchModeEnum.TagOr => entry.HasAnyTagByName(SearchText.Split(",").Select(n => n.Normalize()).Where(_tagService.AvailableTags.Select(x => x.TagName).Contains)),
            SearchModeEnum.AnyTag => entry.Tags.Count > 0,
            SearchModeEnum.NoTag => entry.Tags.Count == 0,
            SearchModeEnum.FileName => entry.Name.Contains(SearchText),
            SearchModeEnum.FileExt => entry.Extension.Contains(SearchText),
        };
    }
}

public enum SearchModeEnum {
    TagAnd,
    TagOr,
    FileName,
    FileExt,
    AnyTag,
    NoTag,
}