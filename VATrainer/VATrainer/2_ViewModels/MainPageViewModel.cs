using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VATrainer.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateCommand = new DelegateCommand<string>(NavigateCommandExecuted);
        }

        public DelegateCommand<string> NavigateCommand { get; }

        private async void NavigateCommandExecuted(string view)
        {
            await _navigationService.NavigateAsync($"NavigationPage/{view}");
        }
    }
}
