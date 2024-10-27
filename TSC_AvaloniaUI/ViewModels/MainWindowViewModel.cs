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
using TSC_AvaloniaUI.Models;

namespace TSC_AvaloniaUI.ViewModels;

public class MainWindowViewModel : ViewModelBase {
    private EntryViewModel? _selectedEntry;
    
    public ObservableCollection<EntryViewModel> Files { get; } = [];
    public ObservableCollection<TagViewModelBase>? Tags => _selectedEntry?.Tags;
    public List<Tag> AvailableTags = new();
    
    public Interaction<IEnumerable<Tag>, Tag?> CreateTagInteraction { get; }
    public Interaction<IEnumerable<Tag>, IEnumerable<TagViewModel>> AddTagInteraction { get; }
    public ICommand CreateTagCommand { get; }
    public ICommand AddTagCommand { get; }
    
    public EntryViewModel? SelectedEntry {
        get => _selectedEntry;
        set {
            this.RaiseAndSetIfChanged(ref _selectedEntry, value);
            this.RaisePropertyChanged(nameof(Tags));
        }
    }

    
    public MainWindowViewModel() {
        RxApp.MainThreadScheduler.Schedule(LoadDir);
        CreateTagInteraction = new();
        AddTagInteraction = new();
        
        CreateTagCommand = ReactiveCommand.Create(async () => {
            var result = await CreateTagInteraction.Handle(AvailableTags);

            if (result != null) {
                AvailableTags.Add(result);
            }
        });

        AddTagCommand = ReactiveCommand.Create(async () =>
        {
            var tags = AvailableTags.Where(x => !SelectedEntry?.Tags.Select(x => (x as TagViewModel)?.Tag).Where(x => x != null).Contains(x) ?? false);
            var result = (await AddTagInteraction.Handle(tags)).ToList();

            if (result.Count != 0) {
                SelectedEntry?.AddTags(result);
            }
        });
    }
    
    private async void LoadDir() {
        var files = (await Entry.GetFilesFromPath(Path.GetFullPath("/home/sapeint"))).Select(x => new EntryViewModel(x));

        foreach (var album in files) {
            Files.Add(album);
        }
    }
}