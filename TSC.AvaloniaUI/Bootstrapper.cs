using Avalonia.Controls;
using Splat;
using TSC.AvaloniaUI.Services;
using TSC.AvaloniaUI.ViewModels;
using TSC.AvaloniaUI.ViewModels.Pages;
using TSC.AvaloniaUI.ViewModels.TagPages;
using TSC.AvaloniaUI.Views;
using TSC.AvaloniaUI.Views.Pages;
using TSC.AvaloniaUI.Views.TagPages;

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
        SplatRegistrations.Register<EditTagViewModel>();
        SplatRegistrations.Register<AddTagToPageViewModel>();
        SplatRegistrations.Register<AddTagToWindowViewModel>();
        SplatRegistrations.Register<EntryPageViewModel>();
        SplatRegistrations.Register<AddSubTagsPageViewModel>();
        
        return this;
    }

    public Bootstrapper RegisterViews() {
        RegisterWindowWithViewModel<AddTagToWindow, AddTagToWindowViewModel>();
        RegisterWindowWithViewModel<EditTagWindow, EditTagViewModel>();
        RegisterWindowWithViewModel<ManageTagsWindow, ManageTagsViewModel>();
        RegisterPageWithViewModel<EntryPageView, EntryPageViewModel>();
        RegisterPageWithViewModel<AddTagToPageView, AddTagToPageViewModel>();
        RegisterPageWithViewModel<AddSubTagsPageView, AddSubTagsPageViewModel>();
        
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
            Locator.CurrentMutable.Register(() => new TP());
        }

        void RegisterPageWithViewModel<TP, TVm>() where TP : UserControl, new() {
            Locator.CurrentMutable.Register<TP>(() => new() {
                DataContext = Locator.Current.GetService<TVm>(),
            });
        }
    }
}