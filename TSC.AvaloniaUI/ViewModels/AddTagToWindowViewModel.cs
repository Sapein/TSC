using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using DynamicData;
using HarfBuzzSharp;
using ReactiveUI;
using Splat;
using TSC.AvaloniaUI.ViewModels.MainPages;
using TSC.AvaloniaUI.ViewModels.Pages;
using TSC.AvaloniaUI.ViewModels.TagPages;
using TSC.Splat.Extensions;
using Tag = TSC.AvaloniaUI.Models.Tag;

namespace TSC.AvaloniaUI.ViewModels;

public class AddTagToWindowViewModel: ViewModelBase, IActivatableViewModel {
    private readonly AddTagToPageViewModel _addTagToPage;
    
    public ViewModelActivator Activator { get; }

    private TagPageViewModel _currentPage = null!;
    public TagPageViewModel CurrentPage { get => _currentPage; set => this.RaiseAndSetIfChanged(ref _currentPage, value); }

    public ReactiveCommand<Unit, IEnumerable<TagViewModel>> AddTagCommand => _addTagToPage.AddTagCommand;

    public AddTagToWindowViewModel(AddTagToPageViewModel addTagToPageViewModel) {
        Activator = new();
        _addTagToPage = addTagToPageViewModel;
        CurrentPage = _addTagToPage;

        this.WhenActivated(actions => {
            actions(_addTagToPage.AddSubTagsInteraction.RegisterHandler(DoAddSubTags));
        });
    }

    public AddTagToWindowViewModel UpdateTags(IEnumerable<Tag> tags) {
        _addTagToPage.UpdateTags(tags);
        return this;
    }

    private void FinishAddSubTags(IInteractionContext<Unit, Unit> interaction) {
        CurrentPage = _addTagToPage;
        interaction.SetOutput(Unit.Default);
    }
    
    private void DoAddSubTags(IInteractionContext<Unit, Unit> interaction) {
        var page = Locator.Current.GetRequiredService<AddSubTagsPageViewModel>();
        page.FinishSubTagsInteraction.RegisterHandler(FinishAddSubTags);
        CurrentPage = page;
        interaction.SetOutput(Unit.Default);
    }
}