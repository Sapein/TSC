using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows.Input;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using Splat;
using TSC_AvaloniaUI.Models;
using TSC_AvaloniaUI.Services;
using TSC.Splat.Extensions;

namespace TSC_AvaloniaUI.ViewModels;

public class MainWindowEntryViewModel : ViewModelBase {
    private readonly IEntryService _entryService;
    
    private EntryViewModel? _selectedEntry;
    private IDisposable? _disposableTags;
    private ReadOnlyObservableCollection<EntryViewModel>? _files;
    private ReadOnlyObservableCollection<TagViewModelBase>? _tags;

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
                _disposableTags = _selectedEntry?.Tags.ToObservableChangeSet().Bind(out _tags).Subscribe();
            } else {
                _disposableTags?.Dispose();
                _tags = null;
                _disposableTags = null;
            }

            this.RaisePropertyChanged(nameof(Tags));
        }
    }
    
    public MainWindowEntryViewModel(ITagService? tagService = null, IEntryService? entryService = null) {
        CreateTagInteraction = new();
        AddTagInteraction = new();
        ManageTagsInteraction = new();

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

        AddTagCommand = ReactiveCommand.Create(async () => {
            var tags = tagService!.AvailableTags.Where(x => !SelectedEntry?.Tags.Select(vm => (vm as TagViewModel)?.Tag).Where(t => t != null).Contains(x) ?? false);
            var result = (await AddTagInteraction.Handle(tags)).ToList();

            if (result.Count != 0) {
                SelectedEntry?.AddTags(result);
            }
        });
        RxApp.MainThreadScheduler.Schedule(LoadDir);
    }
    
    private async void LoadDir() {
        await _entryService.LoadEntries(Path.GetFullPath("/home/sapeint"));
        _entryService
            .Entries
            .ToObservableChangeSet()
            .Transform(x => new EntryViewModel(x))
            .Bind(out _files)
            .Subscribe();
    }
}