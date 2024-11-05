using ReactiveUI;
using TSC.AvaloniaUI.ViewModels.MainPages;
using TSC.AvaloniaUI.ViewModels.Pages;

namespace TSC.AvaloniaUI.ViewModels;

public class MainWindowViewModel : ViewModelBase {
    private MainPageViewModel _currentPage = null!;
    public MainPageViewModel CurrentPage { get => _currentPage; set => this.RaiseAndSetIfChanged(ref _currentPage, value); }
    
    public MainWindowViewModel(EntryPageView entryPageView) {
        CurrentPage = entryPageView;
    }
}