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
using TSC_AvaloniaUI.Models;
using TSC_AvaloniaUI.Services;
using TSC_AvaloniaUI.Views;
using TSC_AvaloniaUI.Views.MainViews;
using TSC.Splat.Extensions;

namespace TSC_AvaloniaUI.ViewModels;

public class MainWindowViewModel : ViewModelBase {
    
    public MainWindowEntryViewModel EntryViewModel { get; }
    
    public MainWindowViewModel(MainWindowEntryViewModel entryViewModel) {
        EntryViewModel = entryViewModel;
    }
}