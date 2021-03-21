namespace VATrainer.ViewModels
{
    public enum Card
    {
        MoveOut,
        SetContent,
        MoveIn
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

        public NextAnimationParams(Card card, Confidence confidence, NextFinishedCallback callback)
        {
            Card = card;
            Confidence = confidence;
            _callback = callback;
        }

        public Card Card { get; set; }

        public Confidence Confidence { get; set; }

        public void TriggerCallback()
        {
            _callback?.Invoke();
        }
    }
}
