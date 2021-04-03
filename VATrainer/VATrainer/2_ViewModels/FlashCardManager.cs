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
                ExecuteUnconfidentForLeftStack(_leftStack.GetNextQuestion(_currentQuestion));
            }
            else if (_currentStack == _middleStack.Stack)
            {
                ExecuteUnconfidentForMiddleStack(_middleStack.GetNextQuestion(_currentQuestion));
            }
            else
            {
                _currentQuestion = null;
            }
        }

        public void ExecuteConfident()
        {
            if (_currentStack == _leftStack.Stack)
            {
                ExecuteConfidentForLeftStack(_leftStack.GetNextQuestion(_currentQuestion));
            }
            else if (_currentStack == _middleStack.Stack)
            {
                ExecuteConfidentForMiddleStack(_middleStack.GetNextQuestion(_currentQuestion));
            }
            else
            {
                _currentQuestion = null;
            }
        }

        private void ExecuteUnconfidentForLeftStack(Question nextQuestion)
        {
            if (nextQuestion != null)
            {
                _currentQuestion = nextQuestion;
            }
            else
            {
                if (_middleStack.Questions.Count != 0)
                {
                    _currentStack = _middleStack.Stack;
                    _currentQuestion = _middleStack.GetFirstQuestion();
                }
                else if (_leftStack.Questions.Count != 0)
                {
                    _currentQuestion = _leftStack.GetFirstQuestion();
                }
                else
                {
                    _currentQuestion = null;
                }
            }
        }

        private void ExecuteUnconfidentForMiddleStack(Question nextQuestion)
        {
            if (nextQuestion != null)
            {
                _currentQuestion = nextQuestion;
            }
            else
            {
                if (_leftStack.Questions.Count != 0)
                {
                    _currentStack = _leftStack.Stack;
                    _currentQuestion = _leftStack.GetFirstQuestion();
                }
                else if (_middleStack.Questions.Count != 0)
                {
                    _currentQuestion = _middleStack.GetFirstQuestion();
                }
                else
                {
                    _currentQuestion = null;
                }
            }
        }

        private void ExecuteConfidentForLeftStack(Question nextQuestion)
        {
            _leftStack.RemoveQuestion(_currentQuestion);
            _middleStack.AddQuestion(_currentQuestion);
            if (nextQuestion != null)
            {
                _currentQuestion = nextQuestion;
            }
            else
            {
                _currentStack = _middleStack.Stack;
                _currentQuestion = _middleStack.GetFirstQuestion();
            }
        }

        private void ExecuteConfidentForMiddleStack(Question nextQuestion)
        {
            _middleStack.RemoveQuestion(_currentQuestion);
            _rightStack.AddQuestion(_currentQuestion);
            if (nextQuestion != null)
            {
                _currentQuestion = nextQuestion;
            }
            else
            {
                if (_leftStack.Questions.Count != 0)
                {
                    _currentStack = _leftStack.Stack;
                    _currentQuestion = _leftStack.GetFirstQuestion();
                }
                else if (_middleStack.Questions.Count != 0)
                {
                    _currentQuestion = _middleStack.GetFirstQuestion();
                }
                else
                {
                    _currentQuestion = null;
                }
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
