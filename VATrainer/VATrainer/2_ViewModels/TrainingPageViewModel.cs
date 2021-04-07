using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using VATrainer.Models;
using VATrainer.Views;
using Xamarin.Forms.Shapes;

namespace VATrainer.ViewModels
{
    public class TrainingPageViewModel : BindableBase, IPageLifecycleAware
    {
        private readonly INavigationService _navigationService;
        private readonly IRepository _repository;
        private readonly IGeometryCalculator _geometryCalculator;

        private int _unconfidentNumber;
        private int _semiConfidentNumber;
        private int _confidentNumber;

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

        public int UnconfidentNumber
        {
            get { return _unconfidentNumber; }
            set 
            { 
                SetProperty(ref _unconfidentNumber, value);
                RaisePropertyChanged(nameof(UnconfidentStack));
            }
        }

        public GeometryGroup UnconfidentStack => _geometryCalculator.GetDeckGeometry(UnconfidentNumber);

        public int SemiConfidentNumber
        {
            get { return _semiConfidentNumber; }
            set 
            { 
                SetProperty(ref _semiConfidentNumber, value);
                RaisePropertyChanged(nameof(SemiConfidentStack));
            }
        }

        public GeometryGroup SemiConfidentStack => _geometryCalculator.GetDeckGeometry(SemiConfidentNumber);

        public int ConfidentNumber
        {
            get { return _confidentNumber; }
            set 
            {
                SetProperty(ref _confidentNumber, value);
                RaisePropertyChanged(nameof(ConfidentStack));
            }
        }

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
                BindingContext = new ResetPopUpViewModel(_repository, themeId, UpdateCardStacks)
            };
            await PopupNavigation.Instance.PushAsync(resetPopUp, true);
            UpdateCardStacks();
        }

        public void OnAppearing()
        {
            UpdateCardStacks();
        }

        public void OnDisappearing()
        {
            //Do nothing
        }

        private async void UpdateCardStacks()
        {
            List<LearningProgress> progress = await _repository.GetProgress();
            UnconfidentNumber = progress.Find(t => t.ThemeId == 1).Unconfident;
            SemiConfidentNumber = progress.Find(t => t.ThemeId == 1).SemiConfident;
            ConfidentNumber = progress.Find(t => t.ThemeId == 1).Confident;
        }
    }
}
