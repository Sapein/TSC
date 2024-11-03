using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows.Input;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using Splat;
using TSC.AvaloniaUI.Models;
using TSC.AvaloniaUI.Services;
using TSC.AvaloniaUI.ViewModels.Pages;
using TSC.Splat.Extensions;

namespace TSC.AvaloniaUI.ViewModels;

public class MainWindowViewModel : ViewModelBase {
    private PageViewModel _currentPage = null!;
    public PageViewModel CurrentPage { get => _currentPage; set => this.RaiseAndSetIfChanged(ref _currentPage, value); }
    
    public MainWindowViewModel(EntryPageViewModel entryPageViewModel) {
        CurrentPage = entryPageViewModel;
    }
}