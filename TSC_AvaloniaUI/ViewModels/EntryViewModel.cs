using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using TSC_AvaloniaUI.Models;
namespace TSC_AvaloniaUI.ViewModels;

public class EntryViewModel : ViewModelBase {
    private readonly Entry _entry;
    
    public string Extension => _entry.Extension;
    public string Name => _entry.FileName;
    
    // We use TagViewModel base explicitly because we need to add in a final tag, that is always at the end. If we
    // did not, we have no way of actually adding the + button.
    public ObservableCollection<TagViewModelBase> Tags => new(_entry
        .Tags
        .Select(i => new TagViewModel(i.Item2))
        .Append(new AddTagViewModel() as TagViewModelBase));
    
    public EntryViewModel(Entry entry) {
        _entry = entry;
    }

}