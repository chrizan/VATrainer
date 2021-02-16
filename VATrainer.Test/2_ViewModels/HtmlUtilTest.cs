using FluentAssertions;
using Moq;
using System.Collections.Generic;
using VATrainer.Models;
using VATrainer.ViewModels;
using Xunit;

namespace VATrainer.Test.ViewModels
{
    public class HtmlUtilTest
    {
        [Fact]
        public void Test_BuildWebpage()
        {
            // Arrange
            string style = "style";
            string body = "body";

            // Act
            string webPage = HtmlUtil.BuildWebpage(style, body);

            // Assert 
            webPage.Should().Contain(style);
            webPage.Should().Contain(body);
        }

        [Fact]
        public void Test_BuildStyle()
        {
            // Arrange
            var repoMock = new Mock<IRepository>();
            ISettings settings = new Settings(repoMock.Object) { FontSize = "small" };

            // Act
            string style = HtmlUtil.BuildStyle(settings);

            // Assert 
            style.Should().Contain("font-size: small;");
        }

        [Fact]
        public void Test_BuildBody()
        {
            // Arrange
            string html = "htmlText";
            string article1 = "article_1";
            string article2 = "article_2";
            var articles = new List<Article>() 
            {
                new Article { Text = article1 }, 
                new Article { Text = article2 } 
            };

            // Act
            string body = HtmlUtil.BuildBody(html, articles);

            // Assert 
            body.Should().Contain(html);
            body.Should().Contain(article1);
            body.Should().Contain(article2);
        }

        [Fact]
        public void Test_FormatQuestionForContentView()
        {
            // Arrange
            string question = "question";

            // Act
            string formattedQuestion = HtmlUtil.FormatQuestionForContentView(question);

            // Assert 
            formattedQuestion.Should().Be("<b>" + question + "</b>");
        }

        [Fact]
        public void Test_FormatAnswerForContentView()
        {
            // Arrange
            string answer = "answer";

            // Act
            string formattedAnswer = HtmlUtil.FormatAnswerForContentView(answer);

            // Assert 
            formattedAnswer.Should().Be(answer + "<br>");
        }
    }
}
