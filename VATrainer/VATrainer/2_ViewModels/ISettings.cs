namespace VATrainer.ViewModels
{
    public interface ISettings
    {
        public string FontSize { get; set; }
        
        public bool DisplayInstruction { get; set; }

        public int AnimationDuration { get; set; }
    }
}
