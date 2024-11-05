using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using TSC.AvaloniaUI.ViewModels;

namespace TSC.AvaloniaUI.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel> {
    public MainWindow() {
        InitializeComponent();
        if (Design.IsDesignMode) return;

        this.WhenActivated(_ => { });
    }
}