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

        [Fact]
        public void Test_Init_No_Question()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(new List<Question>());
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(0);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(0);
            flashCardManager.CardsOnConfidentStack.Should().Be(0);
            flashCardManager.NextQuestion.Should().BeNull();
        }

        [Fact]
        public void Test_Init_No_NextQuestion()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Stack = (int)CardStack.Left, Order = 1, IsNext = false },
                new Question() { Stack = (int)CardStack.Left, Order = 2, IsNext = false }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(2);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(0);
            flashCardManager.CardsOnConfidentStack.Should().Be(0);
            flashCardManager.NextQuestion.Should().BeNull();
        }

        [Fact]
        public void Test_Execute_Unconfident_Left_Stack_No_Question_Present()
        {

        }

        [Fact]
        public void Test_Execute_Unconfident_Left_Stack_One_Question_Present_Questions_On_Middle_Stack()
        {

        }

        [Fact]
        public void Test_Execute_Unconfident_Left_Stack_One_Question_Present_No_Questions_On_Middle_Stack()
        {

        }

        [Fact]
        public void Test_Execute_Unconfident_Left_Stack_Several_Questions_Next_Question_Present()
        {

        }

        [Fact]
        public void Test_Execute_Unconfident_Left_Stack_Several_Questions_No_Next_Question_Present_Questions_On_Middel_Stack_Present()
        {

        }

        [Fact]
        public void Test_Execute_Unconfident_Left_Stack_Several_Questions_No_Next_Question_Present_No_Questions_On_Middel_Stack_Present()
        {

        }

        [Fact]
        public void Test_Execute_Unconfident_Middle_Stack_No_Question_Present()
        {

        }

        [Fact]
        public void Test_Execute_Unconfident_Middle_Stack_One_Question_Present()
        {

        }

        [Fact]
        public void Test_Execute_Unconfident_Middle_Stack_One_Question_Present_Questions_On_Left_Stack_Present()
        {

        }

        [Fact]
        public void Test_Execute_Unconfident_Middle_Stack_One_Question_Present_No_Questions_On_Left_Stack_Present()
        {

        }

        [Fact]
        public void Test_Execute_Unconfident_Middle_Stack_Several_Questions_Present_Next_Question_Present()
        {

        }

        [Fact]
        public void Test_Execute_Unconfident_Middle_Stack_Several_Questions_Present_No_Next_Question_Present_Questions_On_Left_Stack_Present()
        {

        }

        [Fact]
        public void Test_Execute_Unconfident_Middle_Stack_Several_Questions_Present_No_Next_Question_Present_No_Questions_On_Left_Stack_Present()
        {

        }

        [Fact]
        public void Test_Execute_Unconfident_Right_Stack()
        {

        }

        [Fact]
        public void Test_Execute_Confident_Left_Stack_No_Question()
        {

        }

        [Fact]
        public void Test_Execute_Confident_Left_Stack_One_Question()
        {

        }

        [Fact]
        public void Test_Execute_Confident_Left_Stack_Several_Questions_Next_Question_Present()
        {

        }

        [Fact]
        public void Test_Execute_Confident_Left_Stack_Several_Questions_No_Next_Question_Present()
        {

        }

        [Fact]
        public void Test_Execute_Confident_Middle_Stack_No_Question()
        {

        }

        [Fact]
        public void Test_Execute_Confident_Middle_Stack_One_Question_Questions_On_Left_Stack_Present()
        {

        }

        [Fact]
        public void Test_Execute_Confident_Middle_Stack_One_Question_No_Questions_On_Left_Stack_Present()
        {

        }

        [Fact]
        public void Test_Execute_Confident_Middle_Stack_Several_Questions_Next_Question_Present()
        {

        }

        [Fact]
        public void Test_Execute_Confident_Middle_Stack_Several_Questions_No_Next_Question_Present_Questions_On_Left_Stack_Present()
        {

        }

        [Fact]
        public void Test_Execute_Confident_Middle_Stack_Several_Questions_No_Next_Question_Present_No_Questions_On_Left_Stack_Present()
        {

        }

        [Fact]
        public void Test_Execute_Confident_Right_Stack()
        {

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
