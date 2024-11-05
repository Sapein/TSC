using System;
using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using TSC.AvaloniaUI.ViewModels;
using TSC.AvaloniaUI.ViewModels.TagPages;

namespace TSC.AvaloniaUI.Views;

public partial class AddTagToWindow : ReactiveWindow<AddTagToWindowViewModel>
{
    public AddTagToWindow() {
        InitializeComponent();
        if (Design.IsDesignMode) return;
        
        this.WhenActivated(action => action(ViewModel!.AddTagCommand.Select(x => x).Subscribe(Close)));
    }
}