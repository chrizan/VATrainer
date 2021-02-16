using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VATrainer.ViewModels
{
    public class BrowsingPageViewModel : BindableBase
    {
        private INavigationService NavigationService { get; }

        public BrowsingPageViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            NavigateCommand = new DelegateCommand<string>(NavigateCommandExecuted);
        }

        public DelegateCommand<string> NavigateCommand { get; }

        private async void NavigateCommandExecuted(string theme)
        {
            var navigationParams = new NavigationParameters
            {
                { "theme", theme }
            };
            await NavigationService.NavigateAsync("ContentsPage", navigationParams);
        }
    }
}
