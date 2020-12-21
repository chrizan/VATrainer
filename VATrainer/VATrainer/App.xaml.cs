using Prism;
using Prism.Ioc;
using VATrainer.DataLayer;
using VATrainer.Models;
using VATrainer.ViewModels;
using VATrainer.Views;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace VATrainer
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            DependencyService.Get<IDatabaseService>().CopyDbToInternalStorage();

            await NavigationService.NavigateAsync("MainPage/NavigationPage/HomePage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.Register<ISessionManager, SessionManager>();
            containerRegistry.Register<IRepository, Repository>();
            containerRegistry.Register<IWebpageCreator, WebpageCreator>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<ResourcePage, ResourcePageViewModel>();
            containerRegistry.RegisterForNavigation<SettingPage, SettingPageViewModel>();
            containerRegistry.RegisterForNavigation<BrowsingPage, BrowsingPageViewModel>();
            containerRegistry.RegisterForNavigation<ModePage, ModePageViewModel>();
            containerRegistry.RegisterForNavigation<ContentsPage, ContentsPageViewModel>();
            containerRegistry.RegisterForNavigation<TrainingPage, TrainingPageViewModel>();
            containerRegistry.RegisterForNavigation<QAPage, QAPageViewModel>();
        }
    }
}
