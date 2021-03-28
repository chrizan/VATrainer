using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using System.Windows.Input;
using VATrainer.Models;
using VATrainer.Views;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace VATrainer.ViewModels
{
    public class QAPageViewModel : BindableBase, IInitialize, IPageLifecycleAware, IApplicationLifecycleAware
    {
        private readonly IWebpageCreator _webpageCreator;
        private readonly IGeometryCalculator _geometryCalculator;
        private readonly ISettings _settings;
        private readonly IFlashCardManager _flashCardManager;

        private HtmlWebViewSource _question;
        private HtmlWebViewSource _answer;

        private FlipParams _flipAnimationParams;
        private NextAnimationParams _nextAnimationParams;

        public QAPageViewModel(IWebpageCreator webpageCreator,
            IGeometryCalculator geometryCalculator,
            ISettings settings,
            IFlashCardManager flashCardManager)
        {
            _webpageCreator = webpageCreator;
            _geometryCalculator = geometryCalculator;
            _settings = settings;
            _flashCardManager = flashCardManager;
            Init();
        }

        private void Init()
        {
            SwipedCommand = new Command<SwipedDirection>(ExecuteSwipedCommand);
            SwipeCommand = new Command<SwipedEventArgs>(ExecuteSwipeCommand);
            ConfidentCommand = new DelegateCommand(ConfidentCommanExecuted);
            UnconfidentCommand = new DelegateCommand(UnconfidentCommanExecuted);
        }

        public void Initialize(INavigationParameters parameters)
        {
            int theme = parameters.GetValue<int>("theme");
            _flashCardManager.Init(theme);
            SetContent();
            DisplayInstructionAsync();
        }

        private void SetContent()
        {
            var nextQuestion = _flashCardManager.GetNextQuestion();

            if (nextQuestion != null)
            {
                Question = new HtmlWebViewSource
                {
                    Html = _webpageCreator.CreateQuestionWebpage(nextQuestion)
                };
                Answer = new HtmlWebViewSource
                {
                    Html = _webpageCreator.CreateAnswerWebpage(nextQuestion.Answer)
                };
            }
            else
            {
                Question = new HtmlWebViewSource
                {
                    Html = "<p>Congratulations!<p>"
                };
                Answer = new HtmlWebViewSource
                {
                    Html = "<p>You finished this chapter!<p>"
                };
            }

            UpdateStackView();
        }

        private void UpdateStackView()
        {
            RaisePropertyChanged(nameof(UnconfidentNumber));
            RaisePropertyChanged(nameof(UnconfidentStack));
            
            RaisePropertyChanged(nameof(SemiConfidentNumber));
            RaisePropertyChanged(nameof(SemiConfidentStack));

            RaisePropertyChanged(nameof(ConfidentNumber));
            RaisePropertyChanged(nameof(ConfidentStack));
        }

        private async void DisplayInstructionAsync()
        {
            if (_settings.DisplayInstruction)
            {
                await PopupNavigation.Instance.PushAsync(new InstructionPopUp(), true);
            }
        }

        public uint AnimationDuration
        {
            get => (uint)_settings.AnimationDuration;
        }

        public FlipParams Flip
        {
            get { return _flipAnimationParams; }
            set { SetProperty(ref _flipAnimationParams, value); }
        }

        public NextAnimationParams Next
        {
            get { return _nextAnimationParams; }
            set { SetProperty(ref _nextAnimationParams, value); }
        }

        public HtmlWebViewSource Question
        {
            get { return _question; }
            private set { SetProperty(ref _question, value); }
        }

        public HtmlWebViewSource Answer
        {
            get { return _answer; }
            private set { SetProperty(ref _answer, value); }
        }

        public ICommand SwipeCommand { private set; get; }

        public ICommand SwipedCommand { private set; get; }

        private void ExecuteSwipedCommand(SwipedDirection direction)
        {
            if (direction == SwipedDirection.Left)
            {
                FlipLeft();
            }
            if (direction == SwipedDirection.Right)
            {
                FlipRight();
            }
        }

        private void ExecuteSwipeCommand(SwipedEventArgs swipedEventArgs)
        {
            if (swipedEventArgs.Direction == SwipeDirection.Left)
            {
                FlipLeft();
            }
            if (swipedEventArgs.Direction == SwipeDirection.Right)
            {
                FlipRight();
            }
        }

        private void FlipLeft()
        {
            Flip = new FlipParams(FlipDirection.Left);
        }

        private void FlipRight()
        {
            Flip = new FlipParams(FlipDirection.Right);
        }

        public int UnconfidentNumber => _flashCardManager.CardsOnUnconfidentStack;

        public GeometryGroup UnconfidentStack => _geometryCalculator.GetDeckGeometry(_flashCardManager.CardsOnUnconfidentStack);

        public int SemiConfidentNumber => _flashCardManager.CardsOnSemiConfidentStack;

        public GeometryGroup SemiConfidentStack => _geometryCalculator.GetDeckGeometry(_flashCardManager.CardsOnSemiConfidentStack);

        public int ConfidentNumber => _flashCardManager.CardsOnConfidentStack;

        public GeometryGroup ConfidentStack => _geometryCalculator.GetDeckGeometry(_flashCardManager.CardsOnConfidentStack);

        public DelegateCommand ConfidentCommand { get; private set; }

        public DelegateCommand UnconfidentCommand { get; private set; }

        private void ConfidentCommanExecuted()
        {
            _flashCardManager.ExecuteConfident();
            Next = new NextAnimationParams(Card.MoveOut, Confidence.Confident, NextFinishedCallback);
        }

        private void UnconfidentCommanExecuted()
        {
            _flashCardManager.ExecuteUnconfident();
            Next = new NextAnimationParams(Card.MoveOut, Confidence.Unconfident, NextFinishedCallback);
        }

        private void NextFinishedCallback()
        {
            if (Card.MoveOut == Next.Card)
            {
                SetContent();
                Next = new NextAnimationParams(Card.MoveIn, Confidence.None, NextFinishedCallback);
            }
        }

        public void OnAppearing()
        {
            //TODO -> Load current state -> probably not needed
        }

        public void OnDisappearing()
        {
            _flashCardManager.SaveState();
        }

        public void OnResume()
        {
            //TODO -> maybe covered by OnAppearing()
        }

        public void OnSleep()
        {
            //TODO -> maybe covered by OnDisappearing()
        }
    }
}
