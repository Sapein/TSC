using System;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Splat;
using TSC_AvaloniaUI.Models;
using TSC_AvaloniaUI.ViewModels;

namespace TSC_AvaloniaUI.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel> {
    public MainWindow() {
        InitializeComponent();
        if (Design.IsDesignMode) return;

        this.WhenActivated(action => {
            action(ViewModel!.CreateTagInteraction.RegisterHandler(DoCreateTagAsync));
            action(ViewModel!.AddTagInteraction.RegisterHandler(DoAddTagAsync));
            action(ViewModel!.ManageTagsInteraction.RegisterHandler(DoManageTagsAsync));
        });
    }
    private async Task DoCreateTagAsync(IInteractionContext<Unit, Unit> interaction) {
        var dialog = Locator.Current.GetService<CreateTagWindow>();
        if (dialog is null) throw new NullReferenceException();

        await dialog.ShowDialog<Tag?>(this);
    }
    
    private async Task DoAddTagAsync(IInteractionContext<IEnumerable<Tag>, IEnumerable<TagViewModel>> interaction) {
        var dialog = Locator.Current.GetService<AddTagToWindow>();
        var viewModel = Locator.Current.GetService<AddTagToViewModel>()?.UpdateTags(interaction.Input);
        if (dialog is null || viewModel is null) throw new NullReferenceException();
        dialog.DataContext = viewModel;

        var result = await dialog.ShowDialog<IEnumerable<TagViewModel>>(this);
        interaction.SetOutput(result);
    }
    private async Task DoManageTagsAsync(IInteractionContext<Unit, Unit> interaction) {
        var dialog = Locator.Current.GetService<ManageTagsWindow>();
        if (dialog is null) throw new NullReferenceException();

       await dialog.ShowDialog<Unit>(this);
    }
}