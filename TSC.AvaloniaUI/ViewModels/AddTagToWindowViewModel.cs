using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using DynamicData;
using HarfBuzzSharp;
using ReactiveUI;
using TSC.AvaloniaUI.ViewModels.MainPages;
using TSC.AvaloniaUI.ViewModels.Pages;
using TSC.AvaloniaUI.ViewModels.TagPages;
using Tag = TSC.AvaloniaUI.Models.Tag;

namespace TSC.AvaloniaUI.ViewModels;

public class AddTagToWindowViewModel: ViewModelBase {
    private readonly AddTagToPageViewModel _addTagToPage;
    
    private TagPageViewModel _currentPage = null!;
    public TagPageViewModel CurrentPage { get => _currentPage; set => this.RaiseAndSetIfChanged(ref _currentPage, value); }

    public ReactiveCommand<Unit, IEnumerable<TagViewModel>> AddTagCommand => _addTagToPage.AddTagCommand;
    
    public AddTagToWindowViewModel(AddTagToPageViewModel addTagToPageViewModel) {
        _addTagToPage = addTagToPageViewModel;
        CurrentPage = _addTagToPage;
    }

    public AddTagToWindowViewModel UpdateTags(IEnumerable<Tag> tags) {
        _addTagToPage.UpdateTags(tags);
        return this;
    }
}