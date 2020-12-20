using VATrainer.Models;

namespace VATrainer.ViewModels
{
    public interface IWebpageCreator
    {
        string CreateWebpageForQuestion(Question question);
        string CreateWebpageForAnswer(Answer answer);
    }
}
