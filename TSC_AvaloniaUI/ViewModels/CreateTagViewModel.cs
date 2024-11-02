using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Windows.Input;
using DynamicData;
using ReactiveUI;
using Splat;
using TSC_AvaloniaUI.Models;
using TSC_AvaloniaUI.Services;

namespace TSC_AvaloniaUI.ViewModels;

public class CreateTagViewModel : ViewModelBase {
    private string _tagName = string.Empty;
    public string TagName {
        get => _tagName;
        set => this.RaiseAndSetIfChanged(ref _tagName, value);
    }
    
    public ReactiveCommand<Unit, Unit> CreateTagCommand { get; }
    public ObservableCollection<Tag> Tags { get; } = [];
    public ObservableCollection<Tag> SelectedParentTags { get; set; } = [];
    
    public CreateTagViewModel(ITagService tagService) {
        
        Tags.AddRange(tagService.AvailableTags);
        
        CreateTagCommand = ReactiveCommand.Create(() => { 
            tagService.AvailableTags.Add(new() { TagName = TagName, Children = SelectedParentTags.ToList()});
            
            return Unit.Default;
        });
    }
}