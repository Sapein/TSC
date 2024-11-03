using System.Collections.Generic;
using System.Collections.ObjectModel;
using DynamicData;
using TSC.AvaloniaUI.Views;
using TSC.AvaloniaUI.Models;

namespace TSC.AvaloniaUI.Services;


public interface ITagService {
    public ObservableCollection<Tag> AvailableTags { get; }
}


public class TagService: ITagService {
    public ObservableCollection<Tag> AvailableTags { get; } = new();
}