namespace TSC.AvaloniaUI.ViewModels;

// This exists solely because we need to do this because Avalonia, for some godsforsaken reason does not have CompositeCollection.
// In WPF I could just use CompositeCollection in the XAML to add the last button. However, this is needed to be done.
// TagViewModel base is just a fake view model that inherits from the Base so we can use it in the XAML for data templates.
public class AddTagViewModel : TagViewModelBase {
    private EntryViewModel _entry;
    
    public AddTagViewModel(EntryViewModel entry) {
        _entry = entry;
    }
}