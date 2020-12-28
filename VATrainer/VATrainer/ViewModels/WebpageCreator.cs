using System.Collections.Generic;
using System.Linq;
using System.Text;
using VATrainer.Models;

namespace VATrainer.ViewModels
{
    public class WebpageCreator : IWebpageCreator
    {
        public string CreateQuestionWebpage(Question question)
        {
            List<Article> articles = question.ArticleQuestions.Select(articleQuestion => articleQuestion.Article).ToList();
            string javaScriptArticles = HtmlUtil.BuildJavaScriptArticles(articles);
            string javaScript = HtmlUtil.BuildJavaScript(javaScriptArticles);
            string body = HtmlUtil.BuildBody(question.Text, javaScript);
            string style = HtmlUtil.BuildStyle();
            string html = HtmlUtil.BuildWebpage(style, body);
            return html;
        }

        public string CreateAnswerWebpage(Answer answer)
        {
            List<Article> articles = answer.ArticleAnswers.Select(articleAnswer => articleAnswer.Article).ToList();
            string javaScriptArticles = HtmlUtil.BuildJavaScriptArticles(articles);
            string javaScript = HtmlUtil.BuildJavaScript(javaScriptArticles);
            string body = HtmlUtil.BuildBody(answer.Text, javaScript);
            string style = HtmlUtil.BuildStyle();
            string html = HtmlUtil.BuildWebpage(style, body);
            return html;
        }

        public string CreateContentWebpage(List<Question> questions)
        {
            List<Article> allArticles = new List<Article>();
            StringBuilder sb = new StringBuilder();
            foreach (Question question in questions)
            {
                sb.AppendLine(question.Text);
                sb.AppendLine();
                sb.AppendLine(question.Answer.Text);
                sb.AppendLine();
                allArticles.AddRange(question.ArticleQuestions.Select(articleQuestion => articleQuestion.Article).ToList());
                allArticles.AddRange(question.Answer.ArticleAnswers.Select(articleAnswer => articleAnswer.Article).ToList());
            }
            string javaScriptArticles = HtmlUtil.BuildJavaScriptArticles(allArticles);
            string javaScript = HtmlUtil.BuildJavaScript(javaScriptArticles);
            string body = HtmlUtil.BuildBody(sb.ToString(), javaScript);
            string style = HtmlUtil.BuildStyle();
            string html = HtmlUtil.BuildWebpage(style, body);
            return html;
        }
    }
}
