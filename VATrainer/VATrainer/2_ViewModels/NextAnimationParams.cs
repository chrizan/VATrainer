namespace VATrainer.ViewModels
{
    public enum NextStep
    {
        Out,
        In
    }

    public enum Confidence
    {
        Confident,
        Unconfident,
        None
    }

    public delegate void NextFinishedCallback();

    public class NextAnimationParams
    {
        public readonly NextFinishedCallback _callback;

        public NextAnimationParams(NextStep nextStep, Confidence confidence, NextFinishedCallback callback)
        {
            NextStep = nextStep;
            Confidence = confidence;
            _callback = callback;
        }

        public NextStep NextStep { get; set; }

        public Confidence Confidence { get; set; }

        public void TriggerCallback()
        {
            _callback?.Invoke();
        }
    }
}
