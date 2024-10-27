using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using TSC_AvaloniaUI.Models;
using TSC_AvaloniaUI.ViewModels;

namespace TSC_AvaloniaUI.Controls;

public class TagButton : TemplatedControl {
    public static readonly StyledProperty<string> TagTextProperty = AvaloniaProperty.Register<TagButton, string>(nameof(TagText), "If you see this report this as it's a bug.");
    public static readonly StyledProperty<TagType> TagTypeProperty = AvaloniaProperty.Register<TagButton, TagType>(nameof(TagType));

    public string TagText {
        get => GetValue(TagTextProperty);
        set => SetValue(TagTextProperty, value);
    }

    public TagType TagType {
        get => GetValue(TagTypeProperty);
        set => SetValue(TagTypeProperty, value);
    }
}