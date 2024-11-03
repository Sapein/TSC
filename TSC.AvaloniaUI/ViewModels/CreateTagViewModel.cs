using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Windows.Input;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using DynamicData;
using ReactiveUI;
using Splat;
using TSC.AvaloniaUI.Models;
using TSC.AvaloniaUI.Services;

namespace TSC.AvaloniaUI.ViewModels;

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
            // TODO Properly handle tag relationships
            tagService.AvailableTags.Add(new() { TagName = TagName, Parents = SelectedParentTags.ToList()});
            
            return Unit.Default;
        });
    }
}