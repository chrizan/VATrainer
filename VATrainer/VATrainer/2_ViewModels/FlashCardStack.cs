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
            return Questions[0];
        }

        //public void ExecuteUnconfident(IFlashCardStack leftStack, IFlashCardStack middleStack, IFlashCardStack rightStack, IFlashCardStack currentStack)
        //{
        //    if(currentStack == leftStack)
        //    {
        //        if (HasMoreQuestions(currentStack))
        //        {
        //            SetNextQuestion(currentStack);
        //        }
        //        else
        //        {
        //            if(middleStack.Count > 0)
        //            {
        //                currentStack = middleStack;
        //                SetNextFirstQuestion(currentStack);
        //            }
        //            else
        //            {
        //                SetNextFirstQuestion(currentStack);
        //            }
        //        }
        //    }
        //    else if(currentStack == middleStack)
        //    {
        //        if (HasMoreQuestions(currentStack))
        //        {
        //            var currentQuestion = CurrentQuestion;
        //            SetNextQuestion(currentStack);
        //            middleStack.RemoveQuestion(currentQuestion);
        //            leftStack.AddQuestion(currentQuestion);
        //        }
        //        else
        //        {
        //            middleStack.RemoveQuestion(CurrentQuestion);
        //            leftStack.AddQuestion(CurrentQuestion);
        //            currentStack = leftStack;
        //            SetNextFirstQuestion(currentStack);
        //        }
        //    }
        //    else throw new System.NotImplementedException("All questions are answered");
        //}

        //public void ExecuteConfident(IFlashCardStack leftStack, IFlashCardStack middleStack, IFlashCardStack rightStack, IFlashCardStack currentStack)
        //{
        //    if (currentStack == leftStack)
        //    {
        //        if (HasMoreQuestions(currentStack))
        //        {
        //            var currentQuestion = CurrentQuestion;
        //            SetNextQuestion(currentStack);
        //            leftStack.RemoveQuestion(currentQuestion);
        //            middleStack.AddQuestion(currentQuestion);
        //        }
        //        else
        //        {
        //            leftStack.RemoveQuestion(CurrentQuestion);
        //            middleStack.AddQuestion(CurrentQuestion);
        //            currentStack = middleStack;
        //            SetNextFirstQuestion(currentStack);
        //        }
        //    }
        //    else if (currentStack == middleStack)
        //    {
        //        if (HasMoreQuestions(currentStack))
        //        {
        //            var currentQuestion = CurrentQuestion;
        //            SetNextQuestion(currentStack);
        //            middleStack.RemoveQuestion(currentQuestion);
        //            rightStack.AddQuestion(currentQuestion);
        //        }
        //        else
        //        {
        //            middleStack.RemoveQuestion(CurrentQuestion);
        //            rightStack.AddQuestion(CurrentQuestion);
        //            if(leftStack.Count > 0)
        //            {
        //                currentStack = leftStack;
        //                SetNextFirstQuestion(currentStack);
        //            }
        //            else
        //            {
        //                CurrentQuestion = null;
        //            }
        //        }
        //    }
        //    else throw new System.NotImplementedException("All questions are answered");
        //}
    }
}
