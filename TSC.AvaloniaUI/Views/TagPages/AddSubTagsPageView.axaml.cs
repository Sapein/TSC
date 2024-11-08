using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using TSC.AvaloniaUI.ViewModels.TagPages;

namespace TSC.AvaloniaUI.Views.TagPages;

public partial class AddSubTagsPageView : ReactiveUserControl<AddSubTagsPageViewModel> {
    public AddSubTagsPageView() {
        InitializeComponent();
        if (Design.IsDesignMode) return;
        
        this.WhenActivated(_ => { });
    }
}