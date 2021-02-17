using Xamarin.Essentials;

namespace VATrainer.ViewModels
{
    public class Settings : ISettings
    {
        public const string FontSizeSmall = "small";
        public const string FontSizeMedium = "medium";
        public const string FontSizeLarge = "large";

        private const string FontSizeKey = "fontSizeKey";
        private const string DisplayInstructionKey = "displayInstructionKey";

        public string FontSize
        {
            get => Preferences.Get(FontSizeKey, FontSizeMedium);
            set => Preferences.Set(FontSizeKey, value);
        }

        public bool DisplayInstruction
        {
            get => Preferences.Get(DisplayInstructionKey, true);
            set => Preferences.Set(DisplayInstructionKey, value);
        }
    }
}
