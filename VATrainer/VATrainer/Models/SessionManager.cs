namespace VATrainer.Models
{
    public class SessionManager : ISessionManager
    {
        private readonly IRepository repository;

        public SessionManager(IRepository repository)
        {
            this.repository = repository;
            Question = repository.GetQuestionForId(1).Result;
        }

        public Question Question { get; private set; }

        public void LoadNextQuestionAnswer()
        {
            Question question = repository.GetNextQuestionOfSameTheme(Question).Result;
            if (question == null)
            {
                Question = repository.GetQuestionForId(1).Result;
            }
            else
            {
                Question = question;
            }
        }
    }
}
