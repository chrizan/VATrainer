using Xamarin.Essentials;

namespace VATrainer.ViewModels
{
    public class Settings : ISettings
    {
        private const string FontSizeDefault = "16px";
        private const string FontSizeKey = "fontSizeKey";
        private const string DisplayInstructionKey = "displayInstructionKey";

        public string FontSize
        {
            get => Preferences.Get(FontSizeKey, FontSizeDefault);
            set => Preferences.Set(FontSizeKey, value);
        }

        public bool DisplayInstruction
        {
            get => Preferences.Get(DisplayInstructionKey, true);
            set => Preferences.Set(DisplayInstructionKey, value);
        }
    }
}
