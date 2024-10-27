using System;
using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using TSC_AvaloniaUI.ViewModels;

namespace TSC_AvaloniaUI.Views;

public partial class AddTagToWindow : ReactiveWindow<AddTagToViewModel>
{
    public AddTagToWindow() {
        InitializeComponent();
        if (Design.IsDesignMode) return;
        
        this.WhenActivated(action => action(ViewModel!.AddTagCommand.Select(x => x).Subscribe(Close)));
    }
}