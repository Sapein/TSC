using System.Collections.Generic;
using System.Collections.ObjectModel;
using DynamicData;
using TSC_AvaloniaUI.Models;
using TSC_AvaloniaUI.Views;

namespace TSC_AvaloniaUI.Services;


public interface ITagService {
    public ObservableCollection<Tag> AvailableTags { get; }
}


public class TagService: ITagService {
    public ObservableCollection<Tag> AvailableTags { get; } = new();
}