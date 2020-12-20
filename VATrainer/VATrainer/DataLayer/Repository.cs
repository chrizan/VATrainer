using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using VATrainer.Models;

namespace VATrainer.DataLayer
{
    public class Repository : IRepository
    {
        public async Task<Question> GetQuestionForId(int questionId)
        {
            using var context = new VATrainerContext();
            Question q = await context.Question
                .Where(question => question.Id == questionId)
                .Include(question => question.ArticleQuestions)
                .ThenInclude(articleQuestion => articleQuestion.Article)
                .FirstOrDefaultAsync();
            return q;
        }

        public async Task<Question> GetNextQuestionOfSameTheme(Question currentQuestion)
        {
            using var context = new VATrainerContext();
            Question q = await context.Question
                .Where(question => question.ThemeId == currentQuestion.ThemeId)
                .Where(question => question.Order > currentQuestion.Order)
                .Include(question => question.ArticleQuestions)
                .ThenInclude(articleQuestion => articleQuestion.Article)
                .FirstOrDefaultAsync();
            return q;
        }

        public async Task<Answer> GetAnswerToQuestion(int questionId)
        {
            using var context = new VATrainerContext();
            Answer a = await context.Answer
                .Where(answer => answer.QuestionId == questionId)
                .Include(answer => answer.ArticleAnswers)
                .ThenInclude(articleAnswer => articleAnswer.Article)
                .FirstOrDefaultAsync();
            return a;
        }
    }
}
