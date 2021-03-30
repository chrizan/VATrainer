using System;
using System.Collections.Generic;
using VATrainer.Models;

namespace VATrainer.ViewModels
{
    public class FlashCardManager : IFlashCardManager
    {
        private readonly IRepository _repository;

        private IFlashCardStack _leftStack;
        private IFlashCardStack _middleStack;
        private IFlashCardStack _rightStack;

        private CardStack _currentStack;
        private Question _currentQuestion;

        public FlashCardManager(IRepository repository)
        {
            _repository = repository;
        }

        public void Init(int theme)
        {
            List<Question> questions = _repository.GetAllQuestionsOfTheme(theme).Result;
            _leftStack = new FlashCardStack(questions, CardStack.Left);
            _middleStack = new FlashCardStack(questions, CardStack.Middle);
            _rightStack = new FlashCardStack(questions, CardStack.Right);
            SetCurrentState();
        }

        public Question NextQuestion => _currentQuestion;

        public int CardsOnUnconfidentStack => _leftStack.Questions.Count;

        public int CardsOnSemiConfidentStack => _middleStack.Questions.Count;

        public int CardsOnConfidentStack => _rightStack.Questions.Count;

        public void ExecuteUnconfident()
        {
            if (_currentStack == _leftStack.Stack)
            {
                ExecuteUnconfidentForLeftStack();
            }
            if (_currentStack == _middleStack.Stack)
            {
                ExecuteUnconfidentForMiddleStack();
            }
            else
            {
                throw new NotImplementedException("Unconfident Button should be locked at this stage!");
            }
        }

        private void ExecuteUnconfidentForLeftStack()
        {
            var nextQuestion = _leftStack.GetNextQuestion(_currentQuestion);
            if (nextQuestion != _currentQuestion && nextQuestion != null)
            {
                _currentQuestion = nextQuestion;
            }
            else if (nextQuestion == _currentQuestion)
            {
                if(_middleStack.Count == 0)
                {
                    _currentQuestion = nextQuestion;
                }
                else
                {
                    _currentStack = _middleStack.Stack;
                    _currentQuestion = _middleStack.GetFirstQuestion();
                }

            }
            else
            {
                _currentStack = _middleStack.Stack;
                _currentQuestion = _middleStack.GetFirstQuestion();
            }
        }

        private void ExecuteUnconfidentForMiddleStack()
        {
            var nextQuestion = _middleStack.GetNextQuestion(_currentQuestion);
            if (nextQuestion != _currentQuestion && nextQuestion != null)
            {
                _middleStack.RemoveQuestion(_currentQuestion);
                _leftStack.AddQuestion(_currentQuestion);
                _currentQuestion = nextQuestion;
            }
            else if (nextQuestion == _currentQuestion)
            {
                if (_middleStack.Count == 0)
                {
                    _currentQuestion = nextQuestion;
                }
                else
                {
                    _currentStack = _middleStack.Stack;
                    _currentQuestion = _middleStack.GetFirstQuestion();
                }

            }
            else
            {
                _middleStack.RemoveQuestion(_currentQuestion);
                _leftStack.AddQuestion(_currentQuestion);
                _currentStack = _leftStack.Stack;
                _currentQuestion = _leftStack.GetFirstQuestion();
            }
        }


        public void ExecuteConfident()
        {
            if (_currentStack == _leftStack.Stack)
            {
                var nextQuestion = _leftStack.GetNextQuestion(_currentQuestion);
                if (nextQuestion != null)
                {
                    _leftStack.RemoveQuestion(_currentQuestion);
                    _middleStack.AddQuestion(_currentQuestion);
                    _currentQuestion = nextQuestion;
                }
                else
                {
                    _leftStack.RemoveQuestion(_currentQuestion);
                    _middleStack.AddQuestion(_currentQuestion);
                    _currentStack = _middleStack.Stack;
                    _currentQuestion = _middleStack.GetFirstQuestion();
                }
            }
            if (_currentStack == _middleStack.Stack)
            {
                var nextQuestion = _middleStack.GetNextQuestion(_currentQuestion);
                if (nextQuestion != null)
                {
                    _middleStack.RemoveQuestion(_currentQuestion);
                    _rightStack.AddQuestion(_currentQuestion);
                    _currentQuestion = nextQuestion;
                }
                else
                {
                    if (_leftStack.Questions.Count != 0)
                    {
                        _middleStack.RemoveQuestion(_currentQuestion);
                        _rightStack.AddQuestion(_currentQuestion);
                        _currentStack = _leftStack.Stack;
                        _currentQuestion = _leftStack.GetFirstQuestion();
                    }
                    else
                    {
                        _middleStack.RemoveQuestion(_currentQuestion);
                        _rightStack.AddQuestion(_currentQuestion);
                        _currentStack = _rightStack.Stack;
                        _currentQuestion = null;
                    }
                }
            }
            else
            {
                throw new NotImplementedException("Unconfident Button should be locked at this stage!");
            }
        }

        public void SaveState()
        {
            List<Question> questions = new List<Question>();
            questions.AddRange(_leftStack.Questions);
            questions.AddRange(_rightStack.Questions);
            questions.AddRange(_middleStack.Questions);
            SetIsNextFlag(questions);
            _repository.SaveChanges(questions);
        }

        private void SetIsNextFlag(List<Question> questions)
        {
            questions.ForEach(q => q.IsNext = false);
            if (_currentQuestion != null)
            {
                questions.Find(q => q.Equals(_currentQuestion)).IsNext = true;
            }
        }

        private void SetCurrentState()
        {
            if (_leftStack.Questions.Exists(q => q.IsNext))
            {
                _currentQuestion = _leftStack.Questions.Find(q => q.IsNext);
                _currentStack = _leftStack.Stack;
            }
            else if (_middleStack.Questions.Exists(q => q.IsNext))
            {
                _currentQuestion = _middleStack.Questions.Find(q => q.IsNext);
                _currentStack = _middleStack.Stack;
            }
            else
            {
                _currentQuestion = null;
                _currentStack = _rightStack.Stack;
            }
        }
    }
}
