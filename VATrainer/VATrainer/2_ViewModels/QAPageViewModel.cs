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
        
        private FlipAnimationParams _flipAnimationParams;

        public QAPageViewModel(IRepository repository, IWebpageCreator webpageCreator)
        {
            _repository = repository;
            _webpageCreator = webpageCreator;
            Init();
        }

        private void Init()
        {
            SetContent();
            OnAnimationFinished += OnFlipCallBackChanged;
            SwipedCommand = new Command<SwipedDirection>(ExecuteSwipedCommand);
            SwipeCommand = new Command<SwipedEventArgs>(ExecuteSwipeCommand);
            ConfidentCommand = new DelegateCommand(ConfidentCommanExecuted);
            UnconfidentCommand = new DelegateCommand(UnconfidentCommanExecuted);
            
            //TODO -> proper solution
            Flip = new FlipAnimationParams(FlipDirection.Left, FlipStep.FirstAndSecondQuarter, null);
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
                if (_currentQuestion == null)
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
            Flip = new FlipAnimationParams(FlipDirection.Left, FlipStep.FirstAndSecondQuarter, OnAnimationFinished);
        }

        private void FlipRight()
        {
            Flip = new FlipAnimationParams(FlipDirection.Right, FlipStep.FirstAndSecondQuarter, OnAnimationFinished);
        }

        public DelegateCommand ConfidentCommand { get; private set; }

        public DelegateCommand UnconfidentCommand { get; private set; }

        private void ConfidentCommanExecuted()
        {
            if (FlipDirection.Left == Flip.Direction)
            {
                Flip = new FlipAnimationParams(FlipDirection.Left, FlipStep.FirstQuarter, OnAnimationFinished);
            }
            else
            {
                Flip = new FlipAnimationParams(FlipDirection.Right, FlipStep.FirstQuarter, OnAnimationFinished);
            }
        }

        private void UnconfidentCommanExecuted()
        {
            if (FlipDirection.Left == Flip.Direction)
            {
                Flip = new FlipAnimationParams(FlipDirection.Left, FlipStep.FirstQuarter, OnAnimationFinished);
            }
            else
            {
                Flip = new FlipAnimationParams(FlipDirection.Right, FlipStep.FirstQuarter, OnAnimationFinished);
            }
        }

        private void OnFlipCallBackChanged(object sender, EventArgs eventArgs)
        {
            if (!((FlipAnimationEventArgs)eventArgs).IsFrontviewVisible)
            {
                //
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

        ~QAPageViewModel()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                OnAnimationFinished -= OnFlipCallBackChanged;
            }
        }
    }
}
