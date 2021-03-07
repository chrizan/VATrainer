using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace VATrainer.ViewModels
{
    public class TrainingPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly IDeckGeometryCalc _deckGeometryCalc;

        public TrainingPageViewModel(INavigationService navigationService, IDeckGeometryCalc deckGeometryCalc)
        {
            _navigationService = navigationService;
            _deckGeometryCalc = deckGeometryCalc;
            NavigateCommand = new DelegateCommand<string>(NavigateCommandExecuted);
        }

        public PathGeometry Path => _deckGeometryCalc.GetDeckGeometry(15);

        public DelegateCommand<string> NavigateCommand { get; }

        private async void NavigateCommandExecuted(string view)
        {
            await _navigationService.NavigateAsync(view);
        }
    }
}
