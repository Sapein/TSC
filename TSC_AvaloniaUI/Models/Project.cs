using System.Collections.Generic;

namespace TSC_AvaloniaUI.Models;

public interface IProject {
    public string Name { get; set; }
}


public class OpenProject: IProject {
    public required string Name { get; set; }


}

public class ClosedProject : IProject {
    public required string Name { get; set; }
    
    public IEnumerable<Entry> Entries { get; set; } = new List<Entry>();
}