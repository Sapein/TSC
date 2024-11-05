using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using TSC.AvaloniaUI.ViewModels.TagPages;

namespace TSC.AvaloniaUI.Views.TagPages;

public partial class AddTagToPageView : ReactiveUserControl<AddTagToPageViewModel> {
    public AddTagToPageView() {
        InitializeComponent();
        if (Design.IsDesignMode) return;

        this.WhenActivated(_ => {
        });
    }
}