using System;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Splat;
using TSC.AvaloniaUI.Models;
using TSC.AvaloniaUI.ViewModels;
using TSC.AvaloniaUI.ViewModels.Pages;

namespace TSC.AvaloniaUI.Views.Pages;

public partial class EntryPageView : ReactiveUserControl<EntryPageViewModel> {
    public EntryPageView() {
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

        //NOTE: There has to be a better way to do this, I just don't really know how.
        await dialog.ShowDialog<Tag?>((TopLevel.GetTopLevel(this) as Window)!);
    }
    
    private async Task DoAddTagAsync(IInteractionContext<IEnumerable<Tag>, IEnumerable<TagViewModel>> interaction) {
        var dialog = Locator.Current.GetService<AddTagToWindow>();
        var viewModel = Locator.Current.GetService<AddTagToViewModel>()?.UpdateTags(interaction.Input);
        if (dialog is null || viewModel is null) throw new NullReferenceException();
        dialog.DataContext = viewModel;

        //NOTE: There has to be a better way to do this, I just don't really know how.
        var result = await dialog.ShowDialog<IEnumerable<TagViewModel>>((TopLevel.GetTopLevel(this) as Window)!);
        interaction.SetOutput(result);
    }
    private async Task DoManageTagsAsync(IInteractionContext<Unit, Unit> interaction) {
        var dialog = Locator.Current.GetService<ManageTagsWindow>();
        if (dialog is null) throw new NullReferenceException();

        //NOTE: There has to be a better way to do this, I just don't really know how.
       await dialog.ShowDialog<Unit>((TopLevel.GetTopLevel(this) as Window)!);
    }
}