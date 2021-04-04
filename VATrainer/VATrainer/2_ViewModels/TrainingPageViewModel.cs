using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using VATrainer.Models;
using VATrainer.Views;
using Xamarin.Forms.Shapes;

namespace VATrainer.ViewModels
{
    public class TrainingPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly IRepository _repository;
        private readonly IGeometryCalculator _geometryCalculator;

        public TrainingPageViewModel(INavigationService navigationService, 
            IRepository repository, 
            IGeometryCalculator geometryCalculator)
        {
            _navigationService = navigationService;
            _repository = repository;
            _geometryCalculator = geometryCalculator;
            NavigateCommand = new DelegateCommand<string>(NavigateCommandExecuted);
            ResetCommand = new DelegateCommand<string>(ResetCommandExecuted);
        }

        public GeometryGroup DeckGeometry => _geometryCalculator.GetDeckGeometry(15);

        public DelegateCommand<string> NavigateCommand { get; }

        public DelegateCommand<string> ResetCommand { get; }

        public int UnconfidentNumber => 15;

        public GeometryGroup UnconfidentStack => _geometryCalculator.GetDeckGeometry(UnconfidentNumber);

        public int SemiConfidentNumber => 8;

        public GeometryGroup SemiConfidentStack => _geometryCalculator.GetDeckGeometry(SemiConfidentNumber);

        public int ConfidentNumber => 11;

        public GeometryGroup ConfidentStack => _geometryCalculator.GetDeckGeometry(ConfidentNumber);

        private async void NavigateCommandExecuted(string view)
        {
            var navigationParams = new NavigationParameters { { "theme", 1 } };
            await _navigationService.NavigateAsync(view, navigationParams);
        }

        private async void ResetCommandExecuted(string themeId)
        {
            var resetPopUp = new ResetPopUp
            {
                BindingContext = new ResetPopUpViewModel(_repository, themeId)
            };
            await PopupNavigation.Instance.PushAsync(resetPopUp, true);
        }
    }
}
