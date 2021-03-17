using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace VATrainer.ViewModels
{
    public class InstructionPopUpViewModel : BindableBase
    {
        private INavigationService _navigationService { get; }

        public InstructionPopUpViewModel(INavigationService navigationService)
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
