using System.Collections.Generic;
using VATrainer.Models;

namespace VATrainer.ViewModels
{
    public interface IWebpageCreator
    {
        string CreateQuestionWebpage(Question question);
        string CreateAnswerWebpage(Answer answer);
        string CreateContentWebpage(List<Question> questions);
    }
}
