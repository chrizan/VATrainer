using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using VATrainer.Views;
using Xamarin.Forms.Shapes;

namespace VATrainer.ViewModels
{
    public class TrainingPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly IGeometryCalculator _geometryCalculator;

        public TrainingPageViewModel(INavigationService navigationService, IGeometryCalculator geometryCalculator)
        {
            _navigationService = navigationService;
            _geometryCalculator = geometryCalculator;
            NavigateCommand = new DelegateCommand<string>(NavigateCommandExecuted);
            ResetCommand = new DelegateCommand<string>(ResetCommandExecuted);
        }

        public GeometryGroup DeckGeometry => _geometryCalculator.GetDeckGeometry(15);

        public DelegateCommand<string> NavigateCommand { get; }

        public DelegateCommand<string> ResetCommand { get; }

        private async void NavigateCommandExecuted(string view)
        {
            var navigationParams = new NavigationParameters { { "theme", 1 } };
            await _navigationService.NavigateAsync(view, navigationParams);
        }

        private async void ResetCommandExecuted(string chapter)
        {
            await PopupNavigation.Instance.PushAsync(new ResetPopUp(), true);
        }
    }
}
