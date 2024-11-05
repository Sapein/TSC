using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.Primitives;
using TSC.AvaloniaUI.Models;

namespace TSC.AvaloniaUI.Controls;

public class TagButton : TemplatedControl {
    public static readonly StyledProperty<string> TagTextProperty = AvaloniaProperty.Register<TagButton, string>(nameof(TagText), "If you see this report this as it's a bug.");
    public static readonly StyledProperty<TagType> TagTypeProperty = AvaloniaProperty.Register<TagButton, TagType>(nameof(TagType));
    
    public static readonly StyledProperty<string> ButtonTextProperty = AvaloniaProperty.Register<TagButton, string>(nameof(ButtonText), "If you see this report this as it's a bug.");
    public static readonly StyledProperty<ICommand?> ButtonCommandProperty = AvaloniaProperty.Register<TagButton, ICommand?>(nameof(ButtonCommand));
    public static readonly StyledProperty<object?> ButtonCommandParameterProperty = AvaloniaProperty.Register<TagButton, object?>(nameof(ButtonCommandParameter));

    public string TagText {
        get => GetValue(TagTextProperty);
        set => SetValue(TagTextProperty, value);
    }
    
    public string ButtonText {
        get => GetValue(ButtonTextProperty);
        set => SetValue(ButtonTextProperty, value);
    }
    
    public ICommand? ButtonCommand {
        get => GetValue(ButtonCommandProperty);
        set => SetValue(ButtonCommandProperty, value);
    }
    
    public object? ButtonCommandParameter {
        get => GetValue(ButtonCommandProperty);
        set => SetValue(ButtonCommandProperty, value);
    }

    public TagType TagType {
        get => GetValue(TagTypeProperty);
        set => SetValue(TagTypeProperty, value);
    }
}