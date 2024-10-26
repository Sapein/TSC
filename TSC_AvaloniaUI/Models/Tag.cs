using System.Collections;
using System.Collections.Generic;

namespace TSC_AvaloniaUI.Models;

public class Tag {
    public required string TagName { get; set; }
    public IEnumerable<Tag> Children { get; set; } = new List<Tag>();
}

public enum TagType {
    Positive,
    Negative
}
