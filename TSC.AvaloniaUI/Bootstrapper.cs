using Avalonia.Controls;
using Splat;
using TSC.AvaloniaUI.Services;
using TSC.AvaloniaUI.ViewModels;
using TSC.AvaloniaUI.ViewModels.Pages;
using TSC.AvaloniaUI.Views;
using TSC.AvaloniaUI.Views.Pages;
#pragma warning disable CS8321 // Local function is declared but never used

namespace TSC.AvaloniaUI;

public struct Bootstrapper {

    public static void RegisterAll() {
        Bootstrapper me;
        me
            .RegisterServices()
            .RegisterViewModels()
            .RegisterViews();
        SplatRegistrations.SetupIOC();
    }

    public Bootstrapper RegisterServices() {
        SplatRegistrations.RegisterLazySingleton<ITagService, TagService>();
        SplatRegistrations.RegisterLazySingleton<IEntryService, EntryService>();
        
        return this;
    }
    
    public Bootstrapper RegisterViewModels() {
        SplatRegistrations.Register<MainWindowViewModel>();
        SplatRegistrations.Register<ManageTagsViewModel>();
        SplatRegistrations.Register<CreateTagViewModel>();
        SplatRegistrations.Register<AddTagToViewModel>();
        SplatRegistrations.Register<EntryPageViewModel>();
        
        return this;
    }

    public Bootstrapper RegisterViews() {
        RegisterWindow<AddTagToWindow>();
        RegisterWindowWithViewModel<CreateTagWindow, CreateTagViewModel>();
        RegisterWindowWithViewModel<ManageTagsWindow, ManageTagsViewModel>();
        RegisterPageWithViewModel<EntryPageView, EntryPageViewModel>();
        
        return this;

        void RegisterWindowWithViewModel<TW, TVm>() where TW : Window, new() {
            Locator.CurrentMutable.Register<TW>(() => new() {
                DataContext = Locator.Current.GetService<TVm>(),
            });
        }

        void RegisterWindow<TW>() where TW : Window, new() {
            Locator.CurrentMutable.Register<TW>(() => new());
        }

        void RegisterPage<TP>() where TP : UserControl, new() {
            Locator.CurrentMutable.Register<TP>(() => new TP());
        }

        void RegisterPageWithViewModel<TP, TVm>() where TP : UserControl, new() {
            Locator.CurrentMutable.Register<TP>(() => new() {
                DataContext = Locator.Current.GetService<TVm>(),
            });
        }
    }
}