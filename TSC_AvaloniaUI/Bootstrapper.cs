using Avalonia.Controls;
using Splat;
using TSC_AvaloniaUI.Services;
using TSC_AvaloniaUI.ViewModels;
using TSC_AvaloniaUI.Views;
using TSC_AvaloniaUI.Views.MainViews;

namespace TSC_AvaloniaUI;

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
        SplatRegistrations.Register<MainWindowEntryViewModel>();
        
        return this;
    }

    public Bootstrapper RegisterViews() {
        RegisterWindow<AddTagToWindow>();
        RegisterWindowWithViewModel<CreateTagWindow, CreateTagViewModel>();
        RegisterWindowWithViewModel<ManageTagsWindow, ManageTagsViewModel>();
        RegisterControlWithViewModel<MainWindowEntryView, MainWindowEntryViewModel>();
        
        return this;

        void RegisterWindowWithViewModel<TW, TVm>() where TW : Window, new() {
            Locator.CurrentMutable.Register<TW>(() => new() {
                DataContext = Locator.Current.GetService<TVm>(),
            });
        }

        void RegisterWindow<TW>() where TW : Window, new() {
            Locator.CurrentMutable.Register<TW>(() => new());
        }
        
        void RegisterControlWithViewModel<TC, TVm>() where TC : UserControl, new() {
            Locator.CurrentMutable.Register<TC>(() => new() {
                DataContext = Locator.Current.GetService<TVm>(),
            });
        }
    }
}