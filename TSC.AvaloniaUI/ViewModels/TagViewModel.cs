using System.Reactive;
using System.Windows.Input;
using ReactiveUI;
using TSC.AvaloniaUI.Models;

namespace TSC.AvaloniaUI.ViewModels;

public class TagViewModel : TagViewModelBase {
    
    private readonly Tag _tag;

    public string Name => _tag.TagName;
    public TagType TagType { get; set; } = TagType.Positive;
    public Tag Tag => _tag;

    public ICommand ToggleRelationshipCommand { get; }
    public ReactiveCommand<TagViewModel, Unit>? RemoveTagCommand { get; set; }


    public TagViewModel(Tag tag) {
        _tag = tag;
        ToggleRelationshipCommand = ReactiveCommand.Create(() => {
            TagType = TagType switch {
                TagType.Positive => TagType.Negative,
                TagType.Negative => TagType.Positive,
            };
        });
    }
    
    public TagViewModel(Tag tag, TagType type) {
        _tag = tag;
        TagType = type;
        ToggleRelationshipCommand = ReactiveCommand.Create(() => {
            TagType = TagType switch {
                TagType.Positive => TagType.Negative,
                TagType.Negative => TagType.Positive,
            };
        });
    }
}