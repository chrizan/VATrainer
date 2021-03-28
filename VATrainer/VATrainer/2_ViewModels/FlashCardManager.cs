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
        private IFlashCardStack _currentStack;

        public FlashCardManager(IRepository repository)
        {
            _repository = repository;
        }

        public void Init(int theme)
        {
            List<Question> questions = _repository.GetAllQuestionsOfTheme(theme).Result;
            _leftStack = new FlashCardStack(questions, 1);
            _middleStack = new FlashCardStack(questions, 2);
            _rightStack = new FlashCardStack(questions, 3);
            SetCurrentStack();
        }

        public int CardsOnUnconfidentStack => _leftStack == null ? 0 : _leftStack.Questions.Count;

        public int CardsOnSemiConfidentStack => _middleStack == null ? 0 : _middleStack.Questions.Count;

        public int CardsOnConfidentStack => _rightStack == null ? 0 : _rightStack.Questions.Count;

        public Question GetNextQuestion()
        {
            if (_currentStack == _rightStack)
            {
                return null;
            }
            else return _currentStack.CurrentQuestion;
        }

        public void ExecuteUnconfident()
        {
            _currentStack.ExecuteUnconfident(_leftStack, _middleStack, _rightStack, _currentStack);
        }

        public void ExecuteConfident()
        {
            _currentStack.ExecuteConfident(_leftStack, _middleStack, _rightStack, _currentStack);
        }

        public void SaveState()
        {
            List<Question> questions = new List<Question>();
            questions.AddRange(_leftStack.Questions);
            questions.AddRange(_rightStack.Questions);
            questions.AddRange(_middleStack.Questions);
            questions.ForEach(q => q.IsNext = false);
            questions.Find(q => q.Equals(_currentStack.CurrentQuestion)).IsNext = true;
            _repository.SaveChanges(questions);
        }

        private void SetCurrentStack()
        {
            if (_leftStack.CurrentQuestion != null)
            {
                _currentStack = _leftStack;
            }
            else if (_middleStack.CurrentQuestion != null)
            {
                _currentStack = _middleStack;
            }
            else _currentStack = _rightStack;
        }
    }
}
