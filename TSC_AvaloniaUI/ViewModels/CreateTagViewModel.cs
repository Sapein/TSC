using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Windows.Input;
using DynamicData;
using ReactiveUI;
using TSC_AvaloniaUI.Models;

namespace TSC_AvaloniaUI.ViewModels;

public class CreateTagViewModel : ViewModelBase {
    private string _tagName = string.Empty;
    public string TagName {
        get => _tagName;
        set => this.RaiseAndSetIfChanged(ref _tagName, value);
    }
    
    public ReactiveCommand<Unit, Tag> CreateTagCommand { get; }
    public ObservableCollection<Tag> Tags { get; } = new();
    public ObservableCollection<Tag> SelectedParentTags { get; set; } = [];
    
    public CreateTagViewModel(IEnumerable<Tag> tags) {
        Tags.AddRange(tags);
        CreateTagCommand = ReactiveCommand.Create(() => { 
            return new Tag { TagName = TagName, Children = SelectedParentTags.ToList()};
        });
    }
}