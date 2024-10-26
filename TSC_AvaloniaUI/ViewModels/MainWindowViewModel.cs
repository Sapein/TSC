using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive.Concurrency;
using ReactiveUI;
using TSC_AvaloniaUI.Models;

namespace TSC_AvaloniaUI.ViewModels;

public class MainWindowViewModel : ViewModelBase {
    private EntryViewModel? _selectedEntry = null;
    
    public ObservableCollection<EntryViewModel> Files { get; } = [];
    
    public EntryViewModel? SelectedEntry {
        get => _selectedEntry;
        set => this.RaiseAndSetIfChanged(ref _selectedEntry, value);
    }
    
    public MainWindowViewModel() {
        // Tags.Add(new TagViewModel(new Tag { TagName = "Test"}));
        // Tags.Add(new TagViewModel(new Tag { TagName = "Test2"}));
        // Tags.Add(new AddTagViewModel());
        RxApp.MainThreadScheduler.Schedule(LoadDir);
    }
    
    private async void LoadDir() {
        var files = (await Entry.GetFilesFromPath(Path.GetFullPath("/home/sapeint"))).Select(x => new EntryViewModel(x));

        foreach (var album in files) {
            Files.Add(album);
        }
    }
}