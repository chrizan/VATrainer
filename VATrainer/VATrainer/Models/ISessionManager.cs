namespace VATrainer.Models
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface ISessionManager
    {
        Question Question { get; }
        void LoadNextQuestionAnswer();
    }
}
