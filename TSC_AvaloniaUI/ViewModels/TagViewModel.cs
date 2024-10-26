using TSC_AvaloniaUI.Models;
using TSC_AvaloniaUI.Views;

namespace TSC_AvaloniaUI.ViewModels;

public class TagViewModel : TagViewModelBase {
    
    private readonly Tag _tag;
    private readonly TagType _tagType = TagType.Positive;
    
    public string Name => _tag!.TagName;
    public TagType TagType => _tagType;


    public TagViewModel(Tag tag) {
        _tag = tag;
    }

}