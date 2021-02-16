using System;

namespace VATrainer.ViewModels
{
    public enum FlipDirection
    {
        Left,
        Right
    }

    public enum FlipStep
    {
        FirstQuarter,
        SecondQuarter,
        FirstAndSecondQuarter
    }

    public class FlipAnimationEventArgs : EventArgs
    {
        public bool IsFrontviewVisible { get; set; }
    }

    public class FlipAnimationParams : IAnimationCallback
    {
        public event EventHandler OnAnimationFinished;
        
        public FlipAnimationParams(FlipDirection direction, FlipStep step, EventHandler OnAnimationFinished) 
        { 
            Direction = direction; 
            Step = step;
            this.OnAnimationFinished = OnAnimationFinished;
        }
        
        public FlipDirection Direction { get; set; }
        
        public FlipStep Step { get; set; }
        
        public void TriggerCallback(bool isFrontviewVisible)
        {
            OnAnimationFinished?.Invoke(this, new FlipAnimationEventArgs { IsFrontviewVisible = isFrontviewVisible});
        }
    }
}
