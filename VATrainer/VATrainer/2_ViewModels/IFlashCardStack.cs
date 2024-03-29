﻿using System.Collections.Generic;
using VATrainer.Models;

namespace VATrainer.ViewModels
{
    public enum CardStack
    {
        Left = 1,
        Middle = 2,
        Right = 3
    }

    public interface IFlashCardStack : IList<Question>
    {
        List<Question> Questions { get; }

        CardStack Stack { get; }

        public Question GetNextQuestion(Question currentQuestion);

        public Question GetFirstQuestion();

        public void AddQuestion(Question question);

        public void RemoveQuestion(Question question);
    }
}
