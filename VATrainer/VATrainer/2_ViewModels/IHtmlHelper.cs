using System.Collections.Generic;
using VATrainer.Models;

namespace VATrainer.ViewModels
{
    public interface IHtmlHelper
    {
        string BuildBody(string htmlText, List<Article> articles);
        string BuildStyle(ISettings settings);
        string BuildWebpage(string style, string body);
        string FormatAnswerForContentView(string answer);
        string FormatQuestionForContentView(string question);
    }
}