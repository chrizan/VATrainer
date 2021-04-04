using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace VATrainer.ViewModels
{
    public class ModePageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;

        public ModePageViewModel(INavigationService navigationService)
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
