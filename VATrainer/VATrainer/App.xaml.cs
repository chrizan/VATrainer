using Prism;
using Prism.Ioc;
using Prism.Plugin.Popups;
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
            SetupDatabase();
            await NavigationService.NavigateAsync("MainPage/NavigationPage/HomePage");
        }

        private static void SetupDatabase()
        {
            IDatabaseService databaseService = DependencyService.Get<IDatabaseService>();
            databaseService.CopyDbToInternalStorage();
            databaseService.InitializeDbProvider();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterPopupDialogService();

            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterSingleton<IRepository, Repository>();
            containerRegistry.RegisterSingleton<ISettings, Settings>();
            containerRegistry.RegisterSingleton<IHtmlHelper, HtmlHelper>();
            containerRegistry.RegisterSingleton<IWebpageCreator, WebpageCreator>();
            containerRegistry.RegisterSingleton<IGeometryCalculator, GeometryCalculator>();

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
            containerRegistry.RegisterForNavigation<InstructionPopUp, InstructionPopUpViewModel>();
        }
    }
}
