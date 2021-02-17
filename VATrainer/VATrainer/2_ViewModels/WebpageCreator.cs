using System.Collections.Generic;
using System.Linq;
using System.Text;
using VATrainer.Models;

namespace VATrainer.ViewModels
{
    public class WebpageCreator : IWebpageCreator
    {
        private readonly ISettings _settings;
        private readonly IHtmlHelper _htmlHelper;

        public WebpageCreator(ISettings settings, IHtmlHelper htmlHelper)
        {
            _settings = settings;
            _htmlHelper = htmlHelper;
        }

        public string CreateQuestionWebpage(Question question)
        {
            List<Article> articles = question.ArticleQuestions
                .Select(articleQuestion => articleQuestion.Article)
                .Distinct(new ArticleIdComparer())
                .ToList();
            string body = _htmlHelper.BuildBody(question.Text, articles);
            string style = _htmlHelper.BuildStyle(_settings);
            return _htmlHelper.BuildWebpage(style, body);
        }

        public string CreateAnswerWebpage(Answer answer)
        {
            List<Article> articles = answer.ArticleAnswers
                .Select(articleAnswer => articleAnswer.Article)
                .Distinct(new ArticleIdComparer())
                .ToList();
            string body = _htmlHelper.BuildBody(answer.Text, articles);
            string style = _htmlHelper.BuildStyle(_settings);
            return _htmlHelper.BuildWebpage(style, body);
        }

        public string CreateContentWebpage(List<Question> questions)
        {
            List<Article> articles = new List<Article>();
            StringBuilder content = new StringBuilder();
            foreach (Question question in questions)
            {
                content.AppendLine(_htmlHelper.FormatQuestionForContentView(question.Text));
                content.AppendLine(_htmlHelper.FormatAnswerForContentView(question.Answer.Text));
                articles.AddRange(question.ArticleQuestions.Select(articleQuestion => articleQuestion.Article));
                articles.AddRange(question.Answer.ArticleAnswers.Select(articleAnswer => articleAnswer.Article));
            }
            articles = articles.Distinct(new ArticleIdComparer()).ToList();
            string body = _htmlHelper.BuildBody(content.ToString(), articles);
            string style = _htmlHelper.BuildStyle(_settings);
            return _htmlHelper.BuildWebpage(style, body);
        }

        private class ArticleIdComparer : IEqualityComparer<Article>
        {
            public bool Equals(Article x, Article y)
            {
                return x.Id == y.Id;
            }

            public int GetHashCode(Article obj)
            {
                return 0;
            }
        }
    }
}
