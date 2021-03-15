using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
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
        }

        public GeometryGroup DeckGeometry => _geometryCalculator.GetDeckGeometry(15);

        public DelegateCommand<string> NavigateCommand { get; }

        private async void NavigateCommandExecuted(string view)
        {
            await _navigationService.NavigateAsync(view);
        }
    }
}
