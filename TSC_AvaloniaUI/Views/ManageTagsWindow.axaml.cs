using System;
using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using TSC_AvaloniaUI.ViewModels;

namespace TSC_AvaloniaUI.Views;

public partial class ManageTagsWindow : ReactiveWindow<ManageTagsViewModel>
{
    public ManageTagsWindow() {
        InitializeComponent();
        if (Design.IsDesignMode) return;
        
        this.WhenActivated(action => action(ViewModel!.SaveChangesCommand.Subscribe(_ => Close())));
    }
}