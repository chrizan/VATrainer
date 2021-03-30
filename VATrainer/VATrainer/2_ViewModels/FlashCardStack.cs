using System.Collections.Generic;
using VATrainer.Models;

namespace VATrainer.ViewModels
{
    public class OrderComparer : IComparer<Question>
    {
        public int Compare(Question x, Question y)
        {
            if (x.Order > y.Order)
            {
                return 1;
            }
            else if (x.Order < y.Order)
            {
                return -1;
            }
            else return 0;
        }
    }

    public class FlashCardStack : List<Question>, IFlashCardStack
    {
        public FlashCardStack(List<Question> questions, CardStack stack)
        {
            Questions = questions.FindAll(q => q.Stack == (int)stack);
            Questions.Sort(new OrderComparer());
            Stack = stack;
        }

        public List<Question> Questions { get; }

        public CardStack Stack { get; }

        public Question GetNextQuestion(Question currentQuestion)
        {
            int posCurrentQuestion = Questions.FindIndex(q => q.Equals(currentQuestion));
            if (posCurrentQuestion < Questions.Count - 1)
            {
                return Questions[posCurrentQuestion + 1];
            }
            else if (Questions.Count == 1)
            {
                return currentQuestion;
            }
            else return null;
        }

        public void AddQuestion(Question question)
        {
            if (Questions.Count == 0)
            {
                question.Order = 1;
                question.Stack = (int)Stack;
                Questions.Add(question);
            }
            else
            {
                question.Order = Questions[Questions.Count - 1].Order + 1;
                question.Stack = (int)Stack;
                Questions.Add(question);
            }
        }

        public void RemoveQuestion(Question question)
        {
            Questions.Remove(question);
        }

        public Question GetFirstQuestion()
        {
            if (Questions.Count > 0)
            {
                return Questions[0];
            }
            return null;
        }
    }
}
