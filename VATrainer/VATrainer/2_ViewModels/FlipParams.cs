namespace VATrainer.ViewModels
{
    public enum FlipDirection
    {
        Left,
        Right
    }

    public class FlipParams
    {
        public FlipParams(FlipDirection direction) 
        { 
            Direction = direction; 
        }
        
        public FlipDirection Direction { get; set; }
    }
}
