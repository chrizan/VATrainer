using System.Windows.Input;
using Xamarin.Forms;

namespace VATrainer.Views
{
    public enum PannedDirection
    {
        Left,
        Right
    }
    public class CustomWebView : WebView
    {
        public ICommand PannedCommand
        {
            set { SetValue(PannedCommandProperty, value); }
            get { return (ICommand)GetValue(PannedCommandProperty); }
        }
        public static readonly BindableProperty PannedCommandProperty = BindableProperty.Create(nameof(PannedCommand), typeof(ICommand), typeof(CustomWebView));
    }
}
