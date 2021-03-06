using FluentAssertions;
using Moq;
using System.Collections.Generic;
using VATrainer.Models;
using VATrainer.ViewModels;
using Xunit;

namespace VATrainer.Test.ViewModels
{
    public class HtmlHelperTest
    {
        private readonly IHtmlHelper _htmlHelper = new HtmlHelper();

        [Fact]
        public void Test_BuildWebpage()
        {
            // Arrange
            string style = "style";
            string body = "body";

            // Act
            string webPage = _htmlHelper.BuildWebpage(style, body);

            // Assert 
            webPage.Should().Contain(style);
            webPage.Should().Contain(body);
        }

        [Fact]
        public void Test_BuildStyle()
        {
            // Arrange
            var settingsMock = new Mock<ISettings>();
            settingsMock.SetupGet(m => m.FontSize).Returns("small");

            // Act
            string style = _htmlHelper.BuildStyle(settingsMock.Object);

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
            string body = _htmlHelper.BuildBody(html, articles);

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
            string formattedQuestion = _htmlHelper.FormatQuestionForContentView(question);

            // Assert 
            formattedQuestion.Should().Be(@"<div style=""font-weight:bold;"">" + question + "</div>");
        }

        [Fact]
        public void Test_FormatAnswerForContentView()
        {
            // Arrange
            string answer = "answer";

            // Act
            string formattedAnswer = _htmlHelper.FormatAnswerForContentView(answer);

            // Assert 
            formattedAnswer.Should().Be(@"<div style=""padding-bottom: 5px"";>" + answer + "</div>");
        }
    }
}
