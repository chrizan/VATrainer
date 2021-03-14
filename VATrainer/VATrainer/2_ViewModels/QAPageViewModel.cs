﻿using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;
using VATrainer.Models;
using VATrainer.Views;
using Xamarin.Forms;

namespace VATrainer.ViewModels
{
    public class QAPageViewModel : BindableBase
    {
        private readonly IRepository _repository;
        private readonly IWebpageCreator _webpageCreator;

        private HtmlWebViewSource _question;
        private HtmlWebViewSource _answer;
        private Question _currentQuestion;

        private FlipParams _flipAnimationParams;
        private NextAnimationParams _nextAnimationParams;

        public QAPageViewModel(IRepository repository, IWebpageCreator webpageCreator)
        {
            _repository = repository;
            _webpageCreator = webpageCreator;
            Init();
        }

        private void Init()
        {
            SwipedCommand = new Command<SwipedDirection>(ExecuteSwipedCommand);
            SwipeCommand = new Command<SwipedEventArgs>(ExecuteSwipeCommand);
            ConfidentCommand = new DelegateCommand(ConfidentCommanExecuted);
            UnconfidentCommand = new DelegateCommand(UnconfidentCommanExecuted);
            SetContent();
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

        public DelegateCommand UnconfidentCommand { get; private set; }

        private void ConfidentCommanExecuted()
        {
            Next = new NextAnimationParams(NextStep.One, NextFinishedCallback);
        }

        private void UnconfidentCommanExecuted()
        {
            Next = new NextAnimationParams(NextStep.One, NextFinishedCallback);
        }

        private void NextFinishedCallback()
        {
            if (NextStep.One == Next.NextStep)
            {
                Next = new NextAnimationParams(NextStep.Two, NextFinishedCallback);
                SetContent();
            }
        }
    }
}
