using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using VATrainer.Models;
using VATrainer.ViewModels;
using Xunit;

namespace VATrainer.Test.ViewModels
{
    public class SettingPageViewModelTest
    {
        [Fact]
        public void Test_Article()
        {
            // Arrange
            const string fontSize = "16px";
            const string articleText = "Article";
            
            Article article = new Article() { Text = articleText };
            var repository = new Mock<IRepository>();
            repository.Setup(r => r.GetArticleForId(3)).Returns(Task.FromResult(article));

            var settings = new Mock<ISettings>();
            settings.SetupGet(s => s.FontSize).Returns(fontSize);

            var settingPageViewModel = new SettingPageViewModel(repository.Object, settings.Object);

            // Act
            var articleWebViewSource = settingPageViewModel.Article;

            // Assert
            articleWebViewSource.Html.Should().Be($"<html style=background-color:#f5f5f5;><div style=color:#320b86;font-size:{fontSize}>{articleText}</div></html>");
        }
    }
}
