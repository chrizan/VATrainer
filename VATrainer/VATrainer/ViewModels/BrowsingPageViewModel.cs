﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VATrainer.ViewModels
{
    public class BrowsingPageViewModel : BindableBase
    {
        private INavigationService _navigationService { get; }

        public BrowsingPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateCommand = new DelegateCommand<string>(NavigateCommandExecuted);
        }

        public DelegateCommand<string> NavigateCommand { get; }

        private async void NavigateCommandExecuted(string view)
        {
            await _navigationService.NavigateAsync(view);
        }
    }
}
