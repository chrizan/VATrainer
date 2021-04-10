using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace VATrainer.ViewModels
{
    public class BrowsingPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;

        public BrowsingPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateCommand = new DelegateCommand<string>(NavigateCommandExecuted);
        }

        public DelegateCommand<string> NavigateCommand { get; }

        private async void NavigateCommandExecuted(string theme)
        {
            var navigationParams = new NavigationParameters
            {
                { "theme", theme }
            };
            await _navigationService.NavigateAsync("ContentsPage", navigationParams);
        }
    }
}
