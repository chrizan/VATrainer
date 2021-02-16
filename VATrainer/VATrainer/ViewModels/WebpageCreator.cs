using System.Collections.Generic;
using System.Linq;
using System.Text;
using VATrainer.Models;

namespace VATrainer.ViewModels
{
    //TODO: consider " and ' in text
    public class WebpageCreator : IWebpageCreator
    {
        private readonly ISettings _settings;

        public WebpageCreator(ISettings settings)
        {
            _settings = settings;
        }

        public string CreateQuestionWebpage(Question question)
        {
            List<Article> articles = question.ArticleQuestions
                .Select(articleQuestion => articleQuestion.Article)
                .Distinct(new ArticleIdComparer())
                .ToList();
            string body = HtmlUtil.BuildBody(question.Text, articles);
            string style = HtmlUtil.BuildStyle(_settings);
            return HtmlUtil.BuildWebpage(style, body);
        }

        public string CreateAnswerWebpage(Answer answer)
        {
            List<Article> articles = answer.ArticleAnswers
                .Select(articleAnswer => articleAnswer.Article)
                .Distinct(new ArticleIdComparer())
                .ToList();
            string body = HtmlUtil.BuildBody(answer.Text, articles);
            string style = HtmlUtil.BuildStyle(_settings);
            return HtmlUtil.BuildWebpage(style, body);
        }

        public string CreateContentWebpage(List<Question> questions)
        {
            List<Article> articles = new List<Article>();
            StringBuilder sb = new StringBuilder();
            foreach (Question question in questions)
            {
                sb.AppendLine(question.Text);
                sb.AppendLine();
                sb.AppendLine(question.Answer.Text);
                sb.AppendLine();
                articles.AddRange(question.ArticleQuestions.Select(articleQuestion => articleQuestion.Article).ToList());
                articles.AddRange(question.Answer.ArticleAnswers.Select(articleAnswer => articleAnswer.Article).ToList());
            }
            articles = articles.Distinct(new ArticleIdComparer()).ToList();
            string body = HtmlUtil.BuildBody(sb.ToString(), articles);
            string style = HtmlUtil.BuildStyle(_settings);
            return HtmlUtil.BuildWebpage(style, body);
        }

        private class ArticleIdComparer : IEqualityComparer<Article>
        {
            public bool Equals(Article x, Article y)
            {
                return x.Id == y.Id;
            }

            public int GetHashCode(Article obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}
