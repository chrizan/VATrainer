using System.Collections.Generic;
using VATrainer.Models;

namespace VATrainer.ViewModels
{
    public interface IFlashCardStack : IList<Question>
    {
        List<Question> Questions { get; }

        int Stack { get; }

        Question CurrentQuestion { get; }

        public void AddQuestion(Question question);

        public void RemoveQuestion(Question question);

        void ExecuteUnconfident(IFlashCardStack leftStack, IFlashCardStack middleStack, IFlashCardStack rightStack, IFlashCardStack currentStack);
        
        void ExecuteConfident(IFlashCardStack leftStack, IFlashCardStack middleStack, IFlashCardStack rightStack, IFlashCardStack currentStack);
    }
}
