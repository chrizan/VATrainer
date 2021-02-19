using System.Collections.Generic;
using System.Threading.Tasks;

namespace VATrainer.Models
{
    /// <summary>
    /// Encapsulates access to the data layer
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Returns the question with the given id
        /// </summary>
        /// <param name="questionId">The primary key of the question</param>
        /// <returns></returns>
        Task<Question> GetQuestionForId(int questionId);

        /// <summary>
        /// Returns the next question within the same theme
        /// </summary>
        /// <param name="currentQuestion"></param>
        /// <returns></returns>
        Task<Question> GetNextQuestionOfSameTheme(Question currentQuestion);

        /// <summary>
        /// Returns all questions within this theme
        /// </summary>
        /// <param name="themeId"></param>
        /// <returns></returns>
        Task<List<Question>> GetAllQuestionsOfTheme(int themeId);

        /// <summary>
        /// Returns the article with the given id
        /// </summary>
        /// <param name="articleId">The primary key of the article</param>
        /// <returns></returns>
        Task<Article> GetArticleForId(int articleId);
    }
}