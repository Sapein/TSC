using ReactiveUI;
using TSC.AvaloniaUI.ViewModels.Pages;

namespace TSC.AvaloniaUI.ViewModels;

public class MainWindowViewModel : ViewModelBase {
    private PageViewModel _currentPage = null!;
    public PageViewModel CurrentPage { get => _currentPage; set => this.RaiseAndSetIfChanged(ref _currentPage, value); }
    
    public MainWindowViewModel(EntryPageViewModel entryPageViewModel) {
        CurrentPage = entryPageViewModel;
    }
}