using System.Collections.Generic;
using System.Linq;
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
            throw new System.NotImplementedException();
        }
    }
}
