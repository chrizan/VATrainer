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
        public void Test_Execute_Unconfident_No_Question_Present()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(new List<Question>());
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.ExecuteUnconfident();

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(0);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(0);
            flashCardManager.CardsOnConfidentStack.Should().Be(0);
            flashCardManager.NextQuestion.Should().BeNull();
        }

        [Fact]
        public void Test_Execute_Unconfident_Left_Stack_One_Question_Present_Questions_On_Middle_Stack()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Id = 1, Stack = (int)CardStack.Left, Order = 1, IsNext = true },
                new Question() { Id = 2, Stack = (int)CardStack.Middle, Order = 1, IsNext = false },
                new Question() { Id = 3, Stack = (int)CardStack.Middle, Order = 2, IsNext = false }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.ExecuteUnconfident();

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(1);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(2);
            flashCardManager.CardsOnConfidentStack.Should().Be(0);
            flashCardManager.NextQuestion.Id.Should().Be(2);
        }

        [Fact]
        public void Test_Execute_Unconfident_Middle_Stack_One_Question_Present_Questions_On_Left_Stack()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Id = 1, Stack = (int)CardStack.Middle, Order = 1, IsNext = true },
                new Question() { Id = 2, Stack = (int)CardStack.Left, Order = 1, IsNext = false },
                new Question() { Id = 3, Stack = (int)CardStack.Left, Order = 2, IsNext = false }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.ExecuteUnconfident();

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(2);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(1);
            flashCardManager.CardsOnConfidentStack.Should().Be(0);
            flashCardManager.NextQuestion.Id.Should().Be(2);
        }

        [Fact]
        public void Test_Execute_Unconfident_Left_Stack_One_Question_Present_No_Questions_On_Middle_Stack()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Id = 1, Stack = (int)CardStack.Left, Order = 1, IsNext = true }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.ExecuteUnconfident();

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(1);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(0);
            flashCardManager.CardsOnConfidentStack.Should().Be(0);
            flashCardManager.NextQuestion.Id.Should().Be(1);
        }

        [Fact]
        public void Test_Execute_Unconfident_Middle_Stack_One_Question_Present_No_Questions_On_Left_Stack()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Id = 10, Stack = (int)CardStack.Middle, Order = 1, IsNext = true }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.ExecuteUnconfident();

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(0);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(1);
            flashCardManager.CardsOnConfidentStack.Should().Be(0);
            flashCardManager.NextQuestion.Id.Should().Be(10);
        }

        [Fact]
        public void Test_Execute_Unconfident_Left_Stack_Several_Questions_Next_Question_Present()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Id = 1, Stack = (int)CardStack.Left, Order = 1, IsNext = false },
                new Question() { Id = 2, Stack = (int)CardStack.Left, Order = 1, IsNext = true },
                new Question() { Id = 3, Stack = (int)CardStack.Left, Order = 1, IsNext = false },
                new Question() { Id = 4, Stack = (int)CardStack.Left, Order = 1, IsNext = false }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.ExecuteUnconfident();

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(4);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(0);
            flashCardManager.CardsOnConfidentStack.Should().Be(0);
            flashCardManager.NextQuestion.Id.Should().Be(3);
        }

        [Fact]
        public void Test_Execute_Unconfident_Middle_Stack_Several_Questions_Next_Question_Present()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Id = 1, Stack = (int)CardStack.Middle, Order = 1, IsNext = false },
                new Question() { Id = 2, Stack = (int)CardStack.Middle, Order = 2, IsNext = false },
                new Question() { Id = 3, Stack = (int)CardStack.Middle, Order = 3, IsNext = true },
                new Question() { Id = 4, Stack = (int)CardStack.Middle, Order = 4, IsNext = false }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.ExecuteUnconfident();

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(0);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(4);
            flashCardManager.CardsOnConfidentStack.Should().Be(0);
            flashCardManager.NextQuestion.Id.Should().Be(4);
        }

        [Fact]
        public void Test_Execute_Unconfident_Left_Stack_Several_Questions_No_Next_Question_Present_Questions_On_Middel_Stack_Present()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Id = 1, Stack = (int)CardStack.Left, Order = 1, IsNext = false },
                new Question() { Id = 2, Stack = (int)CardStack.Left, Order = 1, IsNext = false },
                new Question() { Id = 3, Stack = (int)CardStack.Left, Order = 1, IsNext = false },
                new Question() { Id = 4, Stack = (int)CardStack.Left, Order = 1, IsNext = true },
                new Question() { Id = 5, Stack = (int)CardStack.Middle, Order = 1, IsNext = false },
                new Question() { Id = 6, Stack = (int)CardStack.Middle, Order = 1, IsNext = false }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.ExecuteUnconfident();

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(4);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(2);
            flashCardManager.CardsOnConfidentStack.Should().Be(0);
            flashCardManager.NextQuestion.Id.Should().Be(5);
        }

        [Fact]
        public void Test_Execute_Unconfident_Middle_Stack_Several_Questions_No_Next_Question_Present_Questions_On_Left_Stack_Present()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Id = 2, Stack = (int)CardStack.Middle, Order = 1, IsNext = false },
                new Question() { Id = 3, Stack = (int)CardStack.Middle, Order = 2, IsNext = false },
                new Question() { Id = 4, Stack = (int)CardStack.Middle, Order = 3, IsNext = true },
                new Question() { Id = 6, Stack = (int)CardStack.Left, Order = 4, IsNext = false }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.ExecuteUnconfident();

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(1);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(3);
            flashCardManager.CardsOnConfidentStack.Should().Be(0);
            flashCardManager.NextQuestion.Id.Should().Be(6);
        }

        [Fact]
        public void Test_Execute_Unconfident_Left_Stack_Several_Questions_No_Next_Question_Present_No_Questions_On_Middel_Stack_Present()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Id = 1, Stack = (int)CardStack.Left, Order = 3, IsNext = false },
                new Question() { Id = 2, Stack = (int)CardStack.Left, Order = 6, IsNext = false },
                new Question() { Id = 3, Stack = (int)CardStack.Left, Order = 12, IsNext = true }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.ExecuteUnconfident();

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(3);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(0);
            flashCardManager.CardsOnConfidentStack.Should().Be(0);
            flashCardManager.NextQuestion.Id.Should().Be(1);
        }

        [Fact]
        public void Test_Execute_Unconfident_Middle_Stack_Several_Questions_No_Next_Question_Present_No_Questions_On_Left_Stack_Present()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Id = 6, Stack = (int)CardStack.Middle, Order = 1, IsNext = false },
                new Question() { Id = 7, Stack = (int)CardStack.Middle, Order = 2, IsNext = false },
                new Question() { Id = 8, Stack = (int)CardStack.Middle, Order = 3, IsNext = true }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.ExecuteUnconfident();

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(0);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(3);
            flashCardManager.CardsOnConfidentStack.Should().Be(0);
            flashCardManager.NextQuestion.Id.Should().Be(6);
        }

        [Fact]
        public void Test_Execute_Unconfident_Right_Stack()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Id = 8, Stack = (int)CardStack.Right, Order = 1, IsNext = true }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.ExecuteUnconfident();

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(0);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(0);
            flashCardManager.CardsOnConfidentStack.Should().Be(1);
            flashCardManager.NextQuestion.Should().BeNull();
        }

        [Fact]
        public void Test_Execute_Confident_No_Question_Present()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(new List<Question>());
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.ExecuteConfident();

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(0);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(0);
            flashCardManager.CardsOnConfidentStack.Should().Be(0);
            flashCardManager.NextQuestion.Should().BeNull();
        }

        [Fact]
        public void Test_Execute_Confident_Left_Stack_One_Question_Present_No_Questions_On_Middle_Stack()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Id = 8, Stack = (int)CardStack.Left, Order = 1, IsNext = true }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.ExecuteConfident();

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(0);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(1);
            flashCardManager.CardsOnConfidentStack.Should().Be(0);
            flashCardManager.NextQuestion.Id.Should().Be(8);
        }

        [Fact]
        public void Test_Execute_Confident_Middle_Stack_One_Question_Present_No_Questions_On_Left_Stack()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Id = 8, Stack = (int)CardStack.Middle, Order = 1, IsNext = true }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.ExecuteConfident();

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(0);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(0);
            flashCardManager.CardsOnConfidentStack.Should().Be(1);
            flashCardManager.NextQuestion.Should().BeNull();
        }

        [Fact]
        public void Test_Execute_Confident_Left_Stack_One_Question_Present_Questions_On_Middle_Stack()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Id = 8, Stack = (int)CardStack.Left, Order = 1, IsNext = true },
                new Question() { Id = 1, Stack = (int)CardStack.Middle, Order = 4, IsNext = false },
                new Question() { Id = 2, Stack = (int)CardStack.Middle, Order = 5, IsNext = false }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.ExecuteConfident();

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(0);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(3);
            flashCardManager.CardsOnConfidentStack.Should().Be(0);
            flashCardManager.NextQuestion.Id.Should().Be(1);
        }

        [Fact]
        public void Test_Execute_Confident_Middle_Stack_One_Question_Present_Questions_On_Left_Stack_Present()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Id = 8, Stack = (int)CardStack.Middle, Order = 1, IsNext = true },
                new Question() { Id = 1, Stack = (int)CardStack.Left, Order = 4, IsNext = false },
                new Question() { Id = 2, Stack = (int)CardStack.Left, Order = 5, IsNext = false }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.ExecuteConfident();

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(2);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(0);
            flashCardManager.CardsOnConfidentStack.Should().Be(1);
            flashCardManager.NextQuestion.Id.Should().Be(1);
        }

        [Fact]
        public void Test_Execute_Confident_Left_Stack_Several_Questions_Next_Question_Present()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Id = 1, Stack = (int)CardStack.Left, Order = 1, IsNext = false },
                new Question() { Id = 2, Stack = (int)CardStack.Left, Order = 2, IsNext = true },
                new Question() { Id = 3, Stack = (int)CardStack.Left, Order = 3, IsNext = false },
                new Question() { Id = 4, Stack = (int)CardStack.Left, Order = 4, IsNext = false }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.ExecuteConfident();

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(3);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(1);
            flashCardManager.CardsOnConfidentStack.Should().Be(0);
            flashCardManager.NextQuestion.Id.Should().Be(3);
        }

        [Fact]
        public void Test_Execute_Confident_Middle_Stack_Several_Questions_Next_Question_Present()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Id = 1, Stack = (int)CardStack.Middle, Order = 1, IsNext = false },
                new Question() { Id = 2, Stack = (int)CardStack.Middle, Order = 2, IsNext = false },
                new Question() { Id = 3, Stack = (int)CardStack.Middle, Order = 3, IsNext = true },
                new Question() { Id = 4, Stack = (int)CardStack.Middle, Order = 4, IsNext = false }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.ExecuteConfident();

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(0);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(3);
            flashCardManager.CardsOnConfidentStack.Should().Be(1);
            flashCardManager.NextQuestion.Id.Should().Be(4);
        }

        [Fact]
        public void Test_Execute_Confident_Left_Stack_Several_Questions_No_Next_Question_Present_Questions_On_Middle_Stack()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Id = 1, Stack = (int)CardStack.Left, Order = 1, IsNext = false },
                new Question() { Id = 2, Stack = (int)CardStack.Left, Order = 2, IsNext = false },
                new Question() { Id = 3, Stack = (int)CardStack.Left, Order = 3, IsNext = true },
                new Question() { Id = 41, Stack = (int)CardStack.Middle, Order = 4, IsNext = false }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.ExecuteConfident();

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(2);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(2);
            flashCardManager.CardsOnConfidentStack.Should().Be(0);
            flashCardManager.NextQuestion.Id.Should().Be(41);
        }

        [Fact]
        public void Test_Execute_Confident_Middle_Stack_Several_Questions_No_Next_Question_Present_Questions_On_Left_Stack()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Id = 1, Stack = (int)CardStack.Middle, Order = 1, IsNext = false },
                new Question() { Id = 2, Stack = (int)CardStack.Middle, Order = 2, IsNext = false },
                new Question() { Id = 3, Stack = (int)CardStack.Middle, Order = 3, IsNext = true },
                new Question() { Id = 11, Stack = (int)CardStack.Left, Order = 8, IsNext = false },
                new Question() { Id = 13, Stack = (int)CardStack.Left, Order = 9, IsNext = false }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.ExecuteConfident();

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(2);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(2);
            flashCardManager.CardsOnConfidentStack.Should().Be(1);
            flashCardManager.NextQuestion.Id.Should().Be(11);
        }

        [Fact]
        public void Test_Execute_Confident_Left_Stack_Several_Questions_No_Next_Question_Present_No_Questions_On_Middle_Stack()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Id = 1, Stack = (int)CardStack.Left, Order = 1, IsNext = false },
                new Question() { Id = 2, Stack = (int)CardStack.Left, Order = 2, IsNext = false },
                new Question() { Id = 3, Stack = (int)CardStack.Left, Order = 3, IsNext = true }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.ExecuteConfident();

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(2);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(1);
            flashCardManager.CardsOnConfidentStack.Should().Be(0);
            flashCardManager.NextQuestion.Id.Should().Be(3);
        }

        [Fact]
        public void Test_Execute_Confident_Middle_Stack_Several_Questions_No_Next_Question_Present_No_Questions_On_Left_Stack()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Id = 1, Stack = (int)CardStack.Middle, Order = 1, IsNext = false },
                new Question() { Id = 2, Stack = (int)CardStack.Middle, Order = 2, IsNext = false },
                new Question() { Id = 5, Stack = (int)CardStack.Middle, Order = 20, IsNext = false },
                new Question() { Id = 6, Stack = (int)CardStack.Middle, Order = 31, IsNext = true }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.ExecuteConfident();

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(0);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(3);
            flashCardManager.CardsOnConfidentStack.Should().Be(1);
            flashCardManager.NextQuestion.Id.Should().Be(1);
        }

        [Fact]
        public void Test_Execute_Confident_Right_Stack()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Id = 8, Stack = (int)CardStack.Right, Order = 1, IsNext = true }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.ExecuteConfident();

            // Assert
            flashCardManager.CardsOnUnconfidentStack.Should().Be(0);
            flashCardManager.CardsOnSemiConfidentStack.Should().Be(0);
            flashCardManager.CardsOnConfidentStack.Should().Be(1);
            flashCardManager.NextQuestion.Should().BeNull();
        }

        [Fact]
        public void Test_SaveState_CurrentQuestion_Has_IsNext_Flag_Only_One_Question()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Id = 5, Stack = (int)CardStack.Middle, IsNext = true }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.SaveState();

            // Assert
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => l.Count == 1)), Times.Once);
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => l[0].IsNext)), Times.Once);
        }

        [Fact]
        public void Test_SaveState_No_CurrentQuestion_Only_One_Question()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Id = 5, Stack = (int)CardStack.Middle, IsNext = false }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.SaveState();

            // Assert
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => l.Count == 1)), Times.Once);
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => !l[0].IsNext)), Times.Once);
        }

        [Fact]
        public void Test_SaveState_CurrentQuestion_Has_IsNext_Flag_Several_Questions()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Id = 4, Stack = (int)CardStack.Left, IsNext = false },
                new Question() { Id = 5, Stack = (int)CardStack.Left, IsNext = true },
                new Question() { Id = 6, Stack = (int)CardStack.Right, IsNext = false },
                new Question() { Id = 7, Stack = (int)CardStack.Middle, IsNext = false }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.SaveState();

            // Assert
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => l.Count == 4)), Times.Once);
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => l[0].Id == 4)), Times.Once);
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => !l[0].IsNext)), Times.Once);
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => l[1].Id == 5)), Times.Once);
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => l[1].IsNext)), Times.Once);
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => l[2].Id == 6)), Times.Once);
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => !l[2].IsNext)), Times.Once);
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => l[3].Id == 7)), Times.Once);
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => !l[3].IsNext)), Times.Once);
        }

        [Fact]
        public void Test_SaveState_No_CurrentQuestion_Several_Questions()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Id = 4, Stack = (int)CardStack.Left, IsNext = false },
                new Question() { Id = 5, Stack = (int)CardStack.Left, IsNext = false },
                new Question() { Id = 6, Stack = (int)CardStack.Right, IsNext = false },
                new Question() { Id = 7, Stack = (int)CardStack.Middle, IsNext = false }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.SaveState();

            // Assert
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => l.Count == 4)), Times.Once);
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => l[0].Id == 4)), Times.Once);
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => !l[0].IsNext)), Times.Once);
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => l[1].Id == 5)), Times.Once);
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => !l[1].IsNext)), Times.Once);
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => l[2].Id == 6)), Times.Once);
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => !l[2].IsNext)), Times.Once);
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => l[3].Id == 7)), Times.Once);
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => !l[3].IsNext)), Times.Once);
        }

        [Fact]
        public void Test_SaveState_CurrentQuestion_On_Right_Only_One_Question()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Id = 6, Stack = (int)CardStack.Right, IsNext = true }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.SaveState();

            // Assert
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => l.Count == 1)), Times.Once);
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => l[0].Id == 6)), Times.Once);
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => !l[0].IsNext)), Times.Once);
        }

        [Fact]
        public void Test_SaveState_CurrentQuestion_On_Right_Stack_Several_Questions()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            var questions = new List<Question>()
            {
                new Question() { Id = 5, Stack = (int)CardStack.Right, IsNext = false },
                new Question() { Id = 6, Stack = (int)CardStack.Right, IsNext = true },
                new Question() { Id = 7, Stack = (int)CardStack.Right, IsNext = false }
            };
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(questions);
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.SaveState();

            // Assert
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => l.Count == 3)), Times.Once);
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => l[0].Id == 5)), Times.Once);
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => !l[0].IsNext)), Times.Once);
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => l[1].Id == 6)), Times.Once);
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => !l[1].IsNext)), Times.Once);
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => l[2].Id == 7)), Times.Once);
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => !l[2].IsNext)), Times.Once);
        }

        [Fact]
        public void Test_SaveState_No_Questions_Present()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.GetAllQuestionsOfTheme(Theme)).ReturnsAsync(new List<Question>());
            var flashCardManager = new FlashCardManager(repoMock.Object);

            // Act
            flashCardManager.Init(Theme);
            flashCardManager.SaveState();

            // Assert
            repoMock.Verify(r => r.SaveChanges(It.Is<List<Question>>(l => l.Count == 0)), Times.Once);
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
