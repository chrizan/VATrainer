using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows.Input;
using VATrainer.Models;
using VATrainer.Views;
using Xamarin.Forms;

namespace VATrainer.ViewModels
{
    public class QAPageViewModel : BindableBase, IDisposable
    {
        private event EventHandler OnAnimationFinished;

        private readonly IRepository _repository;
        private readonly IWebpageCreator _webpageCreator;
        private HtmlWebViewSource _question;
        private HtmlWebViewSource _answer;
        private Question _currentQuestion;
        private string _counter;
        private bool _isButtonVisible;
        private FlipAnimationParams _flipAnimationParams;

        public QAPageViewModel(IRepository repository, IWebpageCreator webpageCreator)
        {
            _repository = repository;
            _webpageCreator = webpageCreator;
            InitCommands();
            SetContent();
            OnAnimationFinished += OnFlipCallBackChanged;
            ButtonCommand = new DelegateCommand<string>(ButtonCommanExecuted);
        }

        private void InitCommands()
        {
            SwipeCommand = new Command<SwipedEventArgs>(ExecuteSwipeCommand);
            PannedCommand = new Command<PannedDirection>(ExecutePannedCommand);
        }

        private void SetContent()
        {
            if (_currentQuestion == null)
            {
                _currentQuestion = _repository.GetQuestionForId(1).Result;
            }
            else
            {
                _currentQuestion = _repository.GetNextQuestionOfSameTheme(_currentQuestion).Result;
                if(_currentQuestion == null)
                {
                    _currentQuestion = _repository.GetQuestionForId(1).Result;
                }
            }
            Question = new HtmlWebViewSource
            {
                Html = _webpageCreator.CreateQuestionWebpage(_currentQuestion)
            };
            Answer = new HtmlWebViewSource
            {
                Html = _webpageCreator.CreateAnswerWebpage(_currentQuestion.Answer)
            };
            Counter = _currentQuestion.Id.ToString();
        }

        public FlipAnimationParams Flip
        {
            get { return _flipAnimationParams; }
            set { SetProperty(ref _flipAnimationParams, value); }
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

        public string Counter
        {
            get { return _counter; }
            private set { SetProperty(ref _counter, value); }
        }

        public bool IsButtonVisible
        {
            get { return _isButtonVisible; }
            private set { SetProperty(ref _isButtonVisible, value); }
        }

        public ICommand SwipeCommand { private set; get; }

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

        public ICommand PannedCommand { private set; get; }

        private void ExecutePannedCommand(PannedDirection direction)
        {
            if (direction == PannedDirection.Left)
            {
                FlipLeft();
            }
            if (direction == PannedDirection.Right)
            {
                FlipRight();
            }
        }
        private void FlipLeft()
        {
            IsButtonVisible = false;
            Flip = new FlipAnimationParams(FlipDirection.Left, FlipStep.FirstAndSecondQuarter, OnAnimationFinished);
        }

        private void FlipRight()
        {
            IsButtonVisible = false;
            Flip = new FlipAnimationParams(FlipDirection.Right, FlipStep.FirstAndSecondQuarter, OnAnimationFinished);
        }

        public DelegateCommand<string> ButtonCommand { get; }

        private void ButtonCommanExecuted(string btn)
        {
            if (btn.Equals("left") || btn.Equals("right"))
            {
                IsButtonVisible = false;
                if (FlipDirection.Left == Flip.Direction)
                {
                    Flip = new FlipAnimationParams(FlipDirection.Left, FlipStep.FirstQuarter, OnAnimationFinished);
                }
                else
                {
                    Flip = new FlipAnimationParams(FlipDirection.Right, FlipStep.FirstQuarter, OnAnimationFinished);
                }
            }
        }

        private void OnFlipCallBackChanged(object sender, EventArgs eventArgs)
        {
            if (!((FlipAnimationEventArgs)eventArgs).IsFrontviewVisible)
            {
                IsButtonVisible = true;
            }
            if (FlipStep.FirstQuarter == Flip.Step)
            {
                SetContent();
                if (FlipDirection.Left == Flip.Direction)
                {
                    Flip = new FlipAnimationParams(FlipDirection.Left, FlipStep.SecondQuarter, OnAnimationFinished);
                }
                else
                {
                    Flip = new FlipAnimationParams(FlipDirection.Right, FlipStep.SecondQuarter, OnAnimationFinished);
                }
            }
        }

        public void Dispose()
        {
            OnAnimationFinished -= OnFlipCallBackChanged;
        }
    }
}
