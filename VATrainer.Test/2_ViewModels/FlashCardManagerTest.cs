using FluentAssertions;
using Moq;
using System.Collections.Generic;
using VATrainer.Models;
using VATrainer.ViewModels;
using Xunit;

namespace VATrainer.Test.ViewModels
{
    public class FlashCardManagerTest
    {
        private const int Theme = 1;

        [Fact]
        public void Test_Init_NextQuestion_On_Left_Stack()
        {
            // Arrange
            var repoMock = GetRepositoryMock(12, 0, 0, 0);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(12);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(0);
            flashCardManager.CardsOnConfidentStack.Should().Be(0);
            flashCardManager.NextQuestion.IsNext.Should().Be(true);
        }

        [Fact]
        public void Test_Init_NextQuestion_On_Middle_Stack()
        {
            // Arrange
            var repoMock = GetRepositoryMock(3, 2, 5, 3);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(3);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(2);
            flashCardManager.CardsOnConfidentStack.Should().Be(5);
            flashCardManager.NextQuestion.IsNext.Should().Be(true);
        }

        [Fact]
        public void Test_Init_NextQuestion_On_Right_Stack()
        {
            // Arrange
            var repoMock = GetRepositoryMock(2, 4, 3, 7);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(2);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(4);
            flashCardManager.CardsOnConfidentStack.Should().Be(3);
            flashCardManager.NextQuestion.Should().BeNull();
        }

        private Mock<IRepository> GetRepositoryMock(
            int questionsOnLeftStack,
            int questionsOnMiddleStack,
            int questionsOnRighStack,
            int posOfNextQuestion)
        {
            var repoMock = new Mock<IRepository>();

            List<Question> questions = new List<Question>();
            questions.AddRange(GetQuestions(questionsOnLeftStack, CardStack.Left));
            questions.AddRange(GetQuestions(questionsOnMiddleStack, CardStack.Middle));
            questions.AddRange(GetQuestions(questionsOnRighStack, CardStack.Right));
            questions[posOfNextQuestion].IsNext = true;

            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            return repoMock;
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
