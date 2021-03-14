namespace VATrainer.ViewModels
{
    public enum NextStep
    {
        One,
        Two
    }

    public delegate void NextFinishedCallback();

    public class NextAnimationParams
    {
        public readonly NextFinishedCallback _callback;

        public NextAnimationParams(NextStep nextStep, NextFinishedCallback callback)
        {
            _callback = callback;
            NextStep = nextStep;
        }

        public NextStep NextStep { get; set; }

        public void TriggerCallback()
        {
            _callback?.Invoke();
        }
    }
}
