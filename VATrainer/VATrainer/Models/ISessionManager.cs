namespace VATrainer.Models
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface ISessionManager
    {
        Answer Answer { get; }
        Question Question { get; }
        void LoadNextQuestionAnswer();
    }
}
