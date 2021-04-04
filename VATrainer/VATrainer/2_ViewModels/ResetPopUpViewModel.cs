using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using VATrainer.Models;

namespace VATrainer.ViewModels
{
    public class ResetPopUpViewModel : BindableBase
    {
        private readonly IRepository _repository;
        private readonly string _theme;

        public ResetPopUpViewModel(IRepository repository, string theme)
        {
            _repository = repository;
            _theme = theme;
            OkCommand = new DelegateCommand(OkCommandExecuted);
            CancelCommand = new DelegateCommand(CancelCommandExecuted);
        }

        public DelegateCommand OkCommand { get; }

        public DelegateCommand CancelCommand { get; }

        public string Text
        {
            get => Resx.AppResources.Reset_Text;
        }

        public string Question
        {
            get => Resx.AppResources.Reset_Question;
        }

        private async void OkCommandExecuted()
        {
            await _repository.ResetTheme(int.Parse(_theme));
            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void CancelCommandExecuted()
        {
            await PopupNavigation.Instance.PopAsync(true);
        }
    }
}
