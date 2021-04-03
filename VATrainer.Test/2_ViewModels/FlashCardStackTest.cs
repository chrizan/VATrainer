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
        public void Test_GetNextQuestion_No_Question_Available()
        {
            // Arrange
            Question currentQuestion = new Question() { Stack = (int)CardStack.Middle, IsNext = true, Order = 1 };
            List<Question> questions = new List<Question>();
            var flashCardStack = new FlashCardStack(questions, CardStack.Middle);

            // Act
            var nextQuestion = flashCardStack.GetNextQuestion(currentQuestion);

            // Assert
            nextQuestion.Should().BeNull();
        }

        [Fact]
        public void Test_GetNextQuestion_Only_CurrentQuestion_Available()
        {
            // Arrange
            Question currentQuestion = new Question() { Stack = (int)CardStack.Middle, IsNext = true, Order = 1 };
            List<Question> questions = new List<Question>() { currentQuestion };
            var flashCardStack = new FlashCardStack(questions, CardStack.Middle);

            // Act
            var nextQuestion = flashCardStack.GetNextQuestion(currentQuestion);

            // Assert
            nextQuestion.Should().BeNull();
        }

        [Fact]
        public void Test_GetNextQuestion_Question_After_CurrentQuestion_Available()
        {
            // Arrange
            Question currentQuestion = new Question() { Stack = (int)CardStack.Middle, Order = 3 };

            List<Question> questions = new List<Question>()
            {
                new Question(){ Stack = (int)CardStack.Middle, Order = 4 },
                new Question(){ Stack = (int)CardStack.Middle, Order = 1 },
                currentQuestion,
                new Question(){ Stack = (int)CardStack.Middle, Order = 5  },
                new Question(){ Stack = (int)CardStack.Middle, Order = 6  },
                new Question(){ Stack = (int)CardStack.Middle, Order = 2  }
            };

            var flashCardStack = new FlashCardStack(questions, CardStack.Middle);

            // Act
            var nextQuestion = flashCardStack.GetNextQuestion(currentQuestion);

            // Assert
            nextQuestion.Order.Should().Be(4);
        }

        [Fact]
        public void Test_GetNextQuestion_No_Question_After_CurrentQuestion_Available()
        {
            // Arrange
            Question currentQuestion = new Question() { Stack = (int)CardStack.Middle, Order = 6 };

            List<Question> questions = new List<Question>()
            {
                new Question(){ Stack = (int)CardStack.Middle, Order = 4 },
                new Question(){ Stack = (int)CardStack.Middle, Order = 1 },
                currentQuestion,
                new Question(){ Stack = (int)CardStack.Middle, Order = 5  },
                new Question(){ Stack = (int)CardStack.Middle, Order = 3  },
                new Question(){ Stack = (int)CardStack.Middle, Order = 2  }
            };

            var flashCardStack = new FlashCardStack(questions, CardStack.Middle);

            // Act
            var nextQuestion = flashCardStack.GetNextQuestion(currentQuestion);

            // Assert
            nextQuestion.Should().BeNull();
        }

        [Fact]
        public void Test_GetFirstQuestion_One_Question_Available()
        {
            // Arrange
            Question question = new Question() { Stack = (int)CardStack.Middle, Order = 6 };
            List<Question> questions = new List<Question>() { question };
            var flashCardStack = new FlashCardStack(questions, CardStack.Middle);

            // Act
            var firstQuestion = flashCardStack.GetFirstQuestion();

            // Assert
            firstQuestion.Should().Be(question);
        }

        [Fact]
        public void Test_GetFirstQuestion_Several_Questions_Available()
        {
            // Arrange
            Question question1 = new Question() { Stack = (int)CardStack.Middle, Order = 1 };
            Question question2 = new Question() { Stack = (int)CardStack.Middle, Order = 2 };
            Question question3 = new Question() { Stack = (int)CardStack.Middle, Order = 3 };
            List<Question> questions = new List<Question>() { question1, question2, question3 };
            var flashCardStack = new FlashCardStack(questions, CardStack.Middle);

            // Act
            var firstQuestion = flashCardStack.GetFirstQuestion();

            // Assert
            firstQuestion.Should().Be(question1);
        }

        [Fact]
        public void Test_GetFirstQuestion_No_Question_Available()
        {
            // Arrange
            List<Question> questions = new List<Question>();
            var flashCardStack = new FlashCardStack(questions, CardStack.Left);

            // Act
            var firstQuestion = flashCardStack.GetFirstQuestion();

            // Assert
            firstQuestion.Should().BeNull();
        }

        [Fact]
        public void Test_AddQuestion_No_Question_Present()
        {
            // Arrange
            Question question = new Question() { Stack = (int)CardStack.Left, Order = 3 };
            List<Question> questions = new List<Question>();
            var flashCardStack = new FlashCardStack(questions, CardStack.Left);

            // Act
            flashCardStack.AddQuestion(question);

            // Assert
            flashCardStack.Questions.Count.Should().Be(1);
            flashCardStack.Questions.Contains(question).Should().Be(true);
            flashCardStack.Questions[0].Order.Should().Be(1);
        }

        [Fact]
        public void Test_AddQuestion_One_Question_Present()
        {
            // Arrange
            Question newQuestion = new Question() { Stack = (int)CardStack.Right, Order = 2 };
            Question presentQuestion = new Question() { Stack = (int)CardStack.Right, Order = 9 };
            List<Question> questions = new List<Question>() { presentQuestion };
            var flashCardStack = new FlashCardStack(questions, CardStack.Right);

            // Act
            flashCardStack.AddQuestion(newQuestion);

            // Assert
            flashCardStack.Questions.Count.Should().Be(2);
            flashCardStack.Questions.Contains(newQuestion).Should().Be(true);
            flashCardStack.Questions.Contains(presentQuestion).Should().Be(true);
            flashCardStack.Questions[1].Order.Should().Be(10);
        }

        [Fact]
        public void Test_AddQuestion_Several_Questions_Present()
        {
            // Arrange
            Question newQuestion = new Question() { Stack = (int)CardStack.Left, Order = 1 };
            Question presentQuestion1 = new Question() { Stack = (int)CardStack.Middle, Order = 21 };
            Question presentQuestion2 = new Question() { Stack = (int)CardStack.Middle, Order = 10 };
            Question presentQuestion3 = new Question() { Stack = (int)CardStack.Middle, Order = 13};
            List<Question> questions = new List<Question>() { presentQuestion1, presentQuestion2, presentQuestion3 };
            var flashCardStack = new FlashCardStack(questions, CardStack.Middle);

            // Act
            flashCardStack.AddQuestion(newQuestion);

            // Assert
            flashCardStack.Questions.Count.Should().Be(4);
            flashCardStack.Questions.Contains(newQuestion).Should().Be(true);
            flashCardStack.Questions.Contains(presentQuestion1).Should().Be(true);
            flashCardStack.Questions.Contains(presentQuestion2).Should().Be(true);
            flashCardStack.Questions.Contains(presentQuestion3).Should().Be(true);
            flashCardStack.Questions[0].Order.Should().Be(10);
            flashCardStack.Questions[1].Order.Should().Be(13);
            flashCardStack.Questions[2].Order.Should().Be(21);
            flashCardStack.Questions[3].Order.Should().Be(22);
            flashCardStack.Questions[3].Stack.Should().Be((int)CardStack.Middle);
        }

        [Fact]
        public void Test_RemoveQuestion_No_Questions_Available()
        {
            // Arrange
            Question currentQuestion = new Question() { Stack = (int)CardStack.Middle, IsNext = true, Order = 1 };
            List<Question> questions = new List<Question>();
            var flashCardStack = new FlashCardStack(questions, CardStack.Middle);

            // Act
            flashCardStack.RemoveQuestion(currentQuestion);

            // Assert
            flashCardStack.Questions.Count.Should().Be(0);
        }

        [Fact]
        public void Test_RemoveQuestion_Question_Not_in_Questions()
        {
            // Arrange
            Question currentQuestion = new Question() { Stack = (int)CardStack.Middle, IsNext = true, Order = 1 };
            List<Question> questions = new List<Question>()
            {
                new Question(){ Stack = (int)CardStack.Middle, Order = 2 },
                new Question(){ Stack = (int)CardStack.Middle, Order = 3 },
            };
            var flashCardStack = new FlashCardStack(questions, CardStack.Middle);

            // Act
            flashCardStack.RemoveQuestion(currentQuestion);

            // Assert
            flashCardStack.Questions.Count.Should().Be(2);
        }

        [Fact]
        public void Test_RemoveQuestion_Only_One_Question_Available()
        {
            // Arrange
            Question currentQuestion = new Question() { Stack = (int)CardStack.Middle, IsNext = true, Order = 1 };
            List<Question> questions = new List<Question>() { currentQuestion };
            var flashCardStack = new FlashCardStack(questions, CardStack.Middle);

            // Act
            flashCardStack.RemoveQuestion(currentQuestion);

            // Assert
            flashCardStack.Questions.Count.Should().Be(0);
        }

        [Fact]
        public void Test_RemoveQuestion_Several_Questions_Available()
        {
            // Arrange
            Question question1 = new Question() { Stack = (int)CardStack.Middle, Order = 1 };
            Question question2 = new Question() { Stack = (int)CardStack.Middle, Order = 2 };
            Question question3 = new Question() { Stack = (int)CardStack.Middle, Order = 3 };
            List<Question> questions = new List<Question>() { question1, question2, question3 };
            var flashCardStack = new FlashCardStack(questions, CardStack.Middle);

            // Act
            flashCardStack.RemoveQuestion(question2);

            // Assert
            flashCardStack.Questions.Count.Should().Be(2);
            flashCardStack.Questions.Contains(question1).Should().Be(true);
            flashCardStack.Questions.Contains(question2).Should().Be(false);
            flashCardStack.Questions.Contains(question3).Should().Be(true);
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
