using FluentAssertions;
using System.Collections.Generic;
using VATrainer.Models;
using VATrainer.ViewModels;
using Xunit;

namespace VATrainer.Test.ViewModels
{
    public class FlashCardStackTest
    {
        [Fact]
        public void Test_Ctor()
        {
            // Arrange
            List<Question> questions = GetQuestions(5, CardStack.Left);

            // Act
            var flashCardStack = new FlashCardStack(questions, CardStack.Left);

            // Assert
            flashCardStack.Questions.Count.Should().Be(5);
            flashCardStack.Stack.Should().Be(CardStack.Left);
        }

        [Fact]
        public void Test_Ctor_empty_Question_List()
        {
            // Arrange
            List<Question> questions = new List<Question>();

            // Act
            var flashCardStack = new FlashCardStack(questions, CardStack.Left);

            // Assert
            flashCardStack.Questions.Count.Should().Be(0);
            flashCardStack.Stack.Should().Be(CardStack.Left);
        }

        [Fact]
        public void Test_Ctor_Questions_from_an_other_stack_1()
        {
            // Arrange
            List<Question> questions = GetQuestions(5, CardStack.Middle);

            // Act
            var flashCardStack = new FlashCardStack(questions, CardStack.Left);

            // Assert
            flashCardStack.Questions.Count.Should().Be(0);
            flashCardStack.Stack.Should().Be(CardStack.Left);
        }

        [Fact]
        public void Test_Ctor_Questions_from_an_other_stack_2()
        {
            // Arrange
            List<Question> questions = GetQuestions(5, CardStack.Left);

            // Act
            var flashCardStack = new FlashCardStack(questions, CardStack.Right);

            // Assert
            flashCardStack.Questions.Count.Should().Be(0);
            flashCardStack.Stack.Should().Be(CardStack.Right);
        }

        [Fact]
        public void Test_Ctor_Order()
        {
            // Arrange
            List<Question> questions = new List<Question>()
            {
                new Question(){ Stack = (int)CardStack.Middle, Order = 10 },
                new Question(){ Stack = (int)CardStack.Middle, Order = 5 },
                new Question(){ Stack = (int)CardStack.Middle, Order = 7 },
                new Question(){ Stack = (int)CardStack.Middle, Order = 1 },
                new Question(){ Stack = (int)CardStack.Middle, Order = 3 },
                new Question(){ Stack = (int)CardStack.Middle, Order = 6 }
            };

            // Act
            var flashCardStack = new FlashCardStack(questions, CardStack.Middle);

            // Assert
            flashCardStack.Stack.Should().Be(CardStack.Middle);
            flashCardStack.Questions.Count.Should().Be(6);
            flashCardStack.Questions[0].Order.Should().Be(1);
            flashCardStack.Questions[1].Order.Should().Be(3);
            flashCardStack.Questions[2].Order.Should().Be(5);
            flashCardStack.Questions[3].Order.Should().Be(6);
            flashCardStack.Questions[4].Order.Should().Be(7);
            flashCardStack.Questions[5].Order.Should().Be(10);
        }

        [Fact]
        public void Test_Questions_and_Stack()
        {
            // Arrange
            List<Question> questions = new List<Question>()
            {
                new Question(){ Stack = (int)CardStack.Middle },
                new Question(){ Stack = (int)CardStack.Left},
                new Question(){ Stack = (int)CardStack.Left },
                new Question(){ Stack = (int)CardStack.Right },
                new Question(){ Stack = (int)CardStack.Middle },
                new Question(){ Stack = (int)CardStack.Middle }
            };

            // Act
            var flashCardStack = new FlashCardStack(questions, CardStack.Right);

            // Assert
            flashCardStack.Stack.Should().Be(CardStack.Right);
            flashCardStack.Questions.Count.Should().Be(1);
        }

        [Fact]
        public void Test_GetNextQuestion()
        {
            5.Should().Be(1);
        }

        [Fact]
        public void Test_GetFirstQuestion()
        {
            5.Should().Be(1);
        }

        [Fact]
        public void Test_AddQuestion()
        {
            5.Should().Be(1);
        }

        [Fact]
        public void Test_RemoveQuestion()
        {
            5.Should().Be(1);
        }

        private List<Question> GetQuestions(int number, CardStack stack)
        {
            List<Question> questions = new List<Question>();
            for (int i = 1; i <= number; i++)
            {
                questions.Add(new Question() { Stack = (int)stack, Order = i, IsNext = false });
            }
            return questions;
        }
    }
}
