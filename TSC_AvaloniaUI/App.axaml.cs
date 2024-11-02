using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Splat;
using TSC_AvaloniaUI.Services;
using TSC_AvaloniaUI.ViewModels;
using TSC_AvaloniaUI.Views;

namespace TSC_AvaloniaUI;

public partial class App : Application {
    public override void Initialize() {
        AvaloniaXamlLoader.Load(this);
        Bootstrapper.RegisterAll();
    }

    public override void OnFrameworkInitializationCompleted() {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
            desktop.MainWindow = new MainWindow
            {
                DataContext = Locator.Current.GetService<MainWindowViewModel>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}