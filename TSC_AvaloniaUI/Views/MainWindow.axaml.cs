using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.ReactiveUI;
using ReactiveUI;
using TSC_AvaloniaUI.Models;
using TSC_AvaloniaUI.ViewModels;

namespace TSC_AvaloniaUI.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel> {
    public MainWindow() {
        InitializeComponent();
        this.WhenActivated(action => {
            action(ViewModel!.CreateTagInteraction.RegisterHandler(DoCreateTagAsync));
            action(ViewModel!.AddTagInteraction.RegisterHandler(DoAddTagAsync));
        });
    }
    private async Task DoCreateTagAsync(InteractionContext<IEnumerable<Tag>, Tag?> interaction) {
        var dialog = new CreateTagWindow();
        dialog.DataContext = new CreateTagViewModel(interaction.Input);

        var result = await dialog.ShowDialog<Tag?>(this);
        interaction.SetOutput(result);
    }
    private async Task DoAddTagAsync(InteractionContext<IEnumerable<Tag>, IEnumerable<TagViewModel>> interaction) {
        var dialog = new AddTagToWindow();
        dialog.DataContext = new AddTagToViewModel(interaction.Input);

        var result = await dialog.ShowDialog<IEnumerable<TagViewModel>>(this);
        interaction.SetOutput(result);
    }
}