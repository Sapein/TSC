using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Splat;
using TSC.AvaloniaUI.Models;
using TSC.AvaloniaUI.ViewModels;
using TSC.Splat.Extensions;

namespace TSC.AvaloniaUI.Views;

public partial class ManageTagsWindow : ReactiveWindow<ManageTagsViewModel> {
    public ManageTagsWindow() {
        InitializeComponent();
        if (Design.IsDesignMode) return;
        
        this.WhenActivated(action => {
            action(ViewModel!.SaveChangesCommand.Subscribe(_ => Close()));
            action(ViewModel!.EditTagInteraction.RegisterHandler(DoEditTagAsync));
        });
    }

    private async Task DoEditTagAsync(IInteractionContext<Tag, Unit> interaction) {
        var dialog = Locator.Current.GetRequiredService<EditTagWindow>();
        dialog.ViewModel!.Tag = interaction.Input;
        
        if (dialog is null) throw new NullReferenceException();

        await dialog.ShowDialog<Tag?>(this);
        interaction.SetOutput(Unit.Default);
    }
}