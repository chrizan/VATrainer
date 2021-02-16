using VATrainer.Models;

namespace VATrainer.ViewModels
{
    public class Settings : ISettings
    {
        private readonly IRepository _repository;

        public Settings(IRepository repository)
        {
            _repository = repository;
            Init();
        }

        private void Init()
        {
            FontSize = "medium";
        }

        public string FontSize { get; set; }
    }
}
