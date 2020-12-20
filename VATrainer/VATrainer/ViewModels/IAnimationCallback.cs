using System;

namespace VATrainer.ViewModels
{
    public interface IAnimationCallback
    {
        event EventHandler OnAnimationFinished;
    }
}
