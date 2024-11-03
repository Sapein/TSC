using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DynamicData;
using DynamicData.Binding;
using Splat;
using TSC.AvaloniaUI.Models;
using TSC.Splat.Extensions;

namespace TSC.AvaloniaUI.Services;

public interface IEntryService {
    public ObservableCollectionExtended<Entry> Entries { get; set; }
    
    public Task LoadEntries(string path);
    public Task RemoveInvalidTags();
}

public class EntryService : IEntryService {
    private readonly ITagService _tagService;
    public ObservableCollectionExtended<Entry> Entries { get; set; } = new ();

    public EntryService(ITagService? tagService = null) {
        _tagService = Locator.Current.GetRequiredService(tagService);
    }
    
    public async Task LoadEntries(string path) {
        Entries = new(await GetFilesFromPath(path));
    }
    
    public Task RemoveInvalidTags() {
        foreach (var entry in Entries) {
            var invalidTags = entry.Tags.Items.Where(t => !_tagService.AvailableTags.Contains(t.Item2));

            foreach (var tag in invalidTags) {
                entry.Tags.Remove(tag);
            }
        }
        
        return Task.CompletedTask;
    }

    private static Task<IEnumerable<Entry>> GetFilesFromPath(string path) {
        return Task.FromResult(Directory
            .EnumerateFiles(path)
            .Where(x => !string.IsNullOrEmpty(new DirectoryInfo(x).Extension))
            .Select(x => new Entry(x)));
    }
}