using Moq;
using System.Collections.Generic;
using System.Text;
using VATrainer.Models;
using VATrainer.ViewModels;
using Xunit;

namespace VATrainer.Test.ViewModels
{
    public class WebpageCreatorTest
    {
        [Fact]
        public void Test_CreateQuestionWebpage()
        {
            // Arrange
            var settingsMock = new Mock<ISettings>();
            settingsMock.SetupGet(m => m.FontSize).Returns("small");

            string htmlQuestionText = "htmlQuestionText";
            string style = "style";
            string body = "body";

            var htmlHelperMock = new Mock<IHtmlHelper>();
            htmlHelperMock.Setup(m => m.BuildStyle(It.IsAny<ISettings>())).Returns(style);
            htmlHelperMock.Setup(m => m.BuildBody(htmlQuestionText, It.IsAny<List<Article>>())).Returns(body);

            var webpageCreator = new WebpageCreator(settingsMock.Object, htmlHelperMock.Object);

            Article article1 = new Article { Id = 1, Text = "article" };
            Article article2 = new Article { Id = 1, Text = "article" };

            Question question = new Question
            {
                Text = htmlQuestionText,
                ArticleQuestions = new List<ArticleQuestion>()
                {
                    new ArticleQuestion()
                    {
                        Article = article1
                    },
                    new ArticleQuestion()
                    {
                        Article = article2
                    }
                }
            };

            // Act
            webpageCreator.CreateQuestionWebpage(question);

            // Assert
            htmlHelperMock.Verify(h => h.BuildBody(htmlQuestionText, new List<Article>() { article1 }));
            htmlHelperMock.Verify(h => h.BuildStyle(settingsMock.Object));
            htmlHelperMock.Verify(h => h.BuildWebpage(style, body));
        }

        [Fact]
        public void Test_CreateAnswerWebpage()
        {
            // Arrange
            var settingsMock = new Mock<ISettings>();
            settingsMock.SetupGet(m => m.FontSize).Returns("small");

            string htmlAnswerText = "htmlAnswerText";
            string style = "style";
            string body = "body";

            var htmlHelperMock = new Mock<IHtmlHelper>();
            htmlHelperMock.Setup(m => m.BuildStyle(It.IsAny<ISettings>())).Returns(style);
            htmlHelperMock.Setup(m => m.BuildBody(htmlAnswerText, It.IsAny<List<Article>>())).Returns(body);

            var webpageCreator = new WebpageCreator(settingsMock.Object, htmlHelperMock.Object);

            Article article1 = new Article { Id = 1, Text = "article" };
            Article article2 = new Article { Id = 1, Text = "article" };

            Answer answer = new Answer
            {
                Text = htmlAnswerText,
                ArticleAnswers = new List<ArticleAnswer>()
                {
                    new ArticleAnswer()
                    {
                        Article = article1
                    },
                    new ArticleAnswer()
                    {
                        Article = article2
                    }
                }
            };

            // Act
            webpageCreator.CreateAnswerWebpage(answer);

            // Assert
            htmlHelperMock.Verify(h => h.BuildBody(htmlAnswerText, new List<Article>() { article1 }));
            htmlHelperMock.Verify(h => h.BuildStyle(settingsMock.Object));
            htmlHelperMock.Verify(h => h.BuildWebpage(style, body));
        }

        [Fact]
        public void Test_CreateContentWebpage()
        {
            // Arrange
            var settingsMock = new Mock<ISettings>();
            settingsMock.SetupGet(m => m.FontSize).Returns("small");

            string htmlQuestionText = "htmlQuestionText";
            string htmlAnswerText = "htmlAnswerText";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(htmlQuestionText);
            sb.AppendLine(htmlAnswerText);

            string style = "style";
            string body = "body";

            var htmlHelperMock = new Mock<IHtmlHelper>();
            htmlHelperMock.Setup(m => m.FormatQuestionForContentView(It.IsAny<string>())).Returns(htmlQuestionText);
            htmlHelperMock.Setup(m => m.FormatAnswerForContentView(It.IsAny<string>())).Returns(htmlAnswerText);
            htmlHelperMock.Setup(m => m.BuildStyle(It.IsAny<ISettings>())).Returns(style);
            htmlHelperMock.Setup(m => m.BuildBody(It.IsAny<string>(), It.IsAny<List<Article>>())).Returns(body);

            var webpageCreator = new WebpageCreator(settingsMock.Object, htmlHelperMock.Object);

            Article article1 = new Article { Id = 1, Text = "article" };
            Article article2 = new Article { Id = 2, Text = "article" };

            Question question = new Question
            {
                Text = htmlQuestionText,
                ArticleQuestions = new List<ArticleQuestion>()
                {
                    new ArticleQuestion()
                    {
                        Article = article1
                    },
                    new ArticleQuestion()
                    {
                        Article = article2
                    }
                }
            };

            Article article3 = new Article { Id = 3, Text = "article" };
            Article article4 = new Article { Id = 1, Text = "article" };

            Answer answer = new Answer
            {
                Text = htmlAnswerText,
                ArticleAnswers = new List<ArticleAnswer>()
                {
                    new ArticleAnswer()
                    {
                        Article = article3
                    },
                    new ArticleAnswer()
                    {
                        Article = article4
                    }
                }
            };

            question.Answer = answer;

            // Act
            webpageCreator.CreateContentWebpage(new List<Question>() { question });

            // Assert
            htmlHelperMock.Verify(h => h.BuildBody(sb.ToString(), new List<Article>() { article1, article2, article3 }));
            htmlHelperMock.Verify(h => h.BuildStyle(settingsMock.Object));
            htmlHelperMock.Verify(h => h.BuildWebpage(style, body));
        }
    }
}
