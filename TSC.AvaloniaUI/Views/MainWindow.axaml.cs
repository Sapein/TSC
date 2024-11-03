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

namespace TSC.AvaloniaUI.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel> {
    public MainWindow() {
        InitializeComponent();
        if (Design.IsDesignMode) return;

        this.WhenActivated(action => { });
    }
}