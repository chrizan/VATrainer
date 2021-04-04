using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;

namespace VATrainer.ViewModels
{
    public class ResetPopUpViewModel : BindableBase
    {
        public ResetPopUpViewModel()
        {
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
            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void CancelCommandExecuted()
        {
            await PopupNavigation.Instance.PopAsync(true);
        }
    }
}
