﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            Question question = await context.Question
                .Where(question => question.Id == questionId)
                .Include(question => question.ArticleQuestions)
                .ThenInclude(articleQuestion => articleQuestion.Article)
                .Include(question => question.Answer)
                .ThenInclude(answer => answer.ArticleAnswers)
                .ThenInclude(articleAnswer => articleAnswer.Article)
                .FirstOrDefaultAsync();
            return question;
        }

        public async Task<Question> GetNextQuestionOfSameTheme(Question currentQuestion)
        {
            using var context = new VATrainerContext();
            Question question = await context.Question
                .Where(question => question.ThemeId == currentQuestion.ThemeId)
                .Where(question => question.Order > currentQuestion.Order)
                .Include(question => question.ArticleQuestions)
                .ThenInclude(articleQuestion => articleQuestion.Article)
                .Include(question => question.Answer)
                .ThenInclude(answer => answer.ArticleAnswers)
                .ThenInclude(articleAnswers => articleAnswers.Article)
                .FirstOrDefaultAsync();
            return question;
        }

        public async Task<List<Question>> GetAllQuestionsOfTheme(int themeId)
        {
            using var context = new VATrainerContext();
            List<Question> questions = await context.Question
                .Where(question => question.ThemeId == themeId)
                .Include(question => question.ArticleQuestions)
                .ThenInclude(articleQuestion => articleQuestion.Article)
                .Include(question => question.Answer)
                .ThenInclude(answer => answer.ArticleAnswers)
                .ThenInclude(articleAnswers => articleAnswers.Article)
                .OrderBy(question => question.Id)
                .ToListAsync();
            return questions;
        }

        public async Task<Article> GetArticleForId(int articleId)
        {
            using var context = new VATrainerContext();
            Article article = await context.Article
                .Where(article => article.Id == articleId)
                .FirstOrDefaultAsync();
            return article;
        }
    }
}
