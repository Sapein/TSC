namespace TSC.AvaloniaUI.Models;

public interface ILibrary;

public class ManagedLibrary: ILibrary;
public class HybridLibrary: ILibrary;
public class ExternalLibrary: ILibrary;