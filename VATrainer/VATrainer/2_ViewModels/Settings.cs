using Xamarin.Essentials;

namespace VATrainer.ViewModels
{
    public enum AnimationSpeed
    {
        Fast = 500,
        Medium = 750,
        Slow = 1000
    }

    public class Settings : ISettings
    {
        private const string DisplayInstructionKey = "displayInstructionKey";
        private const string FontSizeKey = "fontSizeKey";
        private const string AnimationDurationKey = "animationDurationKey";

        public string FontSize
        {
            get => Preferences.Get(FontSizeKey, "16px");
            set => Preferences.Set(FontSizeKey, value);
        }

        public bool DisplayInstruction
        {
            get => Preferences.Get(DisplayInstructionKey, true);
            set => Preferences.Set(DisplayInstructionKey, value);
        }

        public int AnimationDuration
        {
            get => Preferences.Get(AnimationDurationKey, (int)AnimationSpeed.Medium);
            set => Preferences.Set(AnimationDurationKey, value);
        }
    }
}
