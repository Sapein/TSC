using System;
using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using TSC.AvaloniaUI.ViewModels;

namespace TSC.AvaloniaUI.Views;

public partial class CreateTagWindow : ReactiveWindow<CreateTagViewModel>
{
    public CreateTagWindow() {
        InitializeComponent();
        if (Design.IsDesignMode) return;
        
        this.WhenActivated(action => action(ViewModel!.CreateTagCommand.Subscribe(_ => Close())));
    }
}