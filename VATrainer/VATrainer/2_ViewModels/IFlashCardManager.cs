using VATrainer.Models;

namespace VATrainer.ViewModels
{
    public interface IFlashCardManager
    {
        public void Init(int theme);

        public int CardsOnUnconfidentStack { get; }

        public int CardsOnSemiConfidentStack { get; }

        public int CardsOnConfidentStack { get; }

        public Question NextQuestion { get; }

        public void ExecuteUnconfident();

        public void ExecuteConfident();
        
        public void SaveState();
    }
}