using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using System.Windows.Input;
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

        private int _unconfidentNumber;
        private GeometryGroup _unconfidentStack;
        private int _semiConfidentNumber;
        private GeometryGroup _semiConfidentStack;
        private int _confidentNumber;
        private GeometryGroup _confidentStack;
        
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
            SetNextQuestion();
            DisplayInstructionAsync();
        }

        private void SetNextQuestion()
        {
            var nextQuestion = _flashCardManager.NextQuestion;

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

            UpdateFlashcardStacks();
        }

        private void UpdateFlashcardStacks()
        {
            UnconfidentNumber = _flashCardManager.CardsOnUnconfidentStack;
            UnconfidentStack = _geometryCalculator.GetDeckGeometry(UnconfidentNumber);

            SemiConfidentNumber = _flashCardManager.CardsOnSemiConfidentStack;
            SemiConfidentStack = _geometryCalculator.GetDeckGeometry(SemiConfidentNumber);

            ConfidentNumber = _flashCardManager.CardsOnConfidentStack;
            ConfidentStack = _geometryCalculator.GetDeckGeometry(ConfidentNumber);
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

        public int UnconfidentNumber
        {
            get { return _unconfidentNumber; }
            set { SetProperty(ref _unconfidentNumber, value); }
        }

        public GeometryGroup UnconfidentStack
        {
            get { return _unconfidentStack; }
            set { SetProperty(ref _unconfidentStack, value); }
        }

        public int SemiConfidentNumber
        {
            get { return _semiConfidentNumber; }
            set { SetProperty(ref _semiConfidentNumber, value); }
        }

        public GeometryGroup SemiConfidentStack
        {
            get { return _semiConfidentStack; }
            set { SetProperty(ref _semiConfidentStack, value); }
        }

        public int ConfidentNumber
        {
            get { return _confidentNumber; }
            set { SetProperty(ref _confidentNumber, value); }
        }

        public GeometryGroup ConfidentStack
        {
            get { return _confidentStack; }
            set { SetProperty(ref _confidentStack, value); }
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

        public DelegateCommand ConfidentCommand { get; private set; }

        private void ConfidentCommanExecuted()
        {
            _flashCardManager.ExecuteConfident();
            Next = new NextAnimationParams(Card.MoveOut, Confidence.Confident, NextFinishedCallback);
        }
        public DelegateCommand UnconfidentCommand { get; private set; }

        private void UnconfidentCommanExecuted()
        {
            _flashCardManager.ExecuteUnconfident();
            Next = new NextAnimationParams(Card.MoveOut, Confidence.Unconfident, NextFinishedCallback);
        }

        private void NextFinishedCallback()
        {
            if (Card.MoveOut == Next.Card)
            {
                SetNextQuestion();
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
