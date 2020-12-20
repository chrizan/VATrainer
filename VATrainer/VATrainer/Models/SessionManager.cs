namespace VATrainer.Models
{
    public class SessionManager : ISessionManager
    {
        private readonly IRepository repository;

        public SessionManager(IRepository repository)
        {
            this.repository = repository;
            Question = repository.GetQuestionForId(1).Result;
            Answer = repository.GetAnswerToQuestion(1).Result;
        }

        public Question Question { get; private set; }

        public Answer Answer { get; private set; }

        public void LoadNextQuestionAnswer()
        {
            Question question = repository.GetNextQuestionOfSameTheme(Question).Result;
            if (question == null)
            {
                Question = repository.GetQuestionForId(1).Result;
                Answer = repository.GetAnswerToQuestion(1).Result;
            }
            else
            {
                Question = question;
                Answer = repository.GetAnswerToQuestion(question.Id).Result;
            }
        }
    }
}
