using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Splat;
using TSC_AvaloniaUI.Models;
using TSC_AvaloniaUI.ViewModels;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace TSC_AvaloniaUI.Views.MainViews;

public partial class MainWindowEntryView : ReactiveUserControl<MainWindowEntryViewModel> {
    public MainWindowEntryView() {
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

        // await dialog.ShowDialog<Tag?>(this);
    }
    
    private async Task DoAddTagAsync(IInteractionContext<IEnumerable<Tag>, IEnumerable<TagViewModel>> interaction) {
        var dialog = Locator.Current.GetService<AddTagToWindow>();
        var viewModel = Locator.Current.GetService<AddTagToViewModel>()?.UpdateTags(interaction.Input);
        if (dialog is null || viewModel is null) throw new NullReferenceException();
        dialog.DataContext = viewModel;

        // var result = await dialog.ShowDialog<IEnumerable<TagViewModel>>(this);
        // interaction.SetOutput(result);
    }
    private async Task DoManageTagsAsync(IInteractionContext<Unit, Unit> interaction) {
        var dialog = Locator.Current.GetService<ManageTagsWindow>();
        if (dialog is null) throw new NullReferenceException();

       // await dialog.ShowDialog<Unit>(this);
    }
}