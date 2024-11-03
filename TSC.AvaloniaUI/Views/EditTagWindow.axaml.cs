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

namespace TSC.AvaloniaUI.Views;

public partial class EditTagWindow : ReactiveWindow<EditTagViewModel>
{
    public EditTagWindow() {
        InitializeComponent();
        if (Design.IsDesignMode) return;

        this.WhenActivated(action => {
            action(ViewModel!.SaveTagCommand.Subscribe(_ => Close()));
        });
    }
}