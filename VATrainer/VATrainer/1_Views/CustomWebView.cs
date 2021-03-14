using System.Windows.Input;
using Xamarin.Forms;

namespace VATrainer.Views
{
    public enum SwipedDirection
    {
        Left,
        Right
    }

    public class CustomWebView : WebView
    {
        public ICommand SwipedCommand
        {
            set { SetValue(SwipedCommandProperty, value); }
            get { return (ICommand)GetValue(SwipedCommandProperty); }
        }
        public static readonly BindableProperty SwipedCommandProperty = BindableProperty.Create(nameof(SwipedCommand), typeof(ICommand), typeof(CustomWebView));
    }
}
