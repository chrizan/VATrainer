using Android.Content;
using Android.Views;
using VATrainer.Droid;
using VATrainer.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace VATrainer.Droid
{
    public class CustomWebViewRenderer : WebViewRenderer
    {
        public CustomWebViewRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);
            Control.SetOnTouchListener(new OnTouchListener((CustomWebView)Element));
        }
    }

    public class OnTouchListener : Java.Lang.Object, Android.Views.View.IOnTouchListener
    {
        private readonly CustomWebView _customWebView;

        private float _oldX;
        private float _newX;

        public OnTouchListener(CustomWebView customWebView)
        {
            _customWebView = customWebView;
        }

        public bool OnTouch(Android.Views.View v, MotionEvent e)
        {
            if (e.Action == MotionEventActions.Down)
            {
                OnTouchDown(e);
            }
            if (e.Action == MotionEventActions.Up)
            {
                OnTouchUp(e);
            }
            return false;
        }

        private void OnTouchDown(MotionEvent e)
        {
            _oldX = e.GetX(0);
        }

        private void OnTouchUp(MotionEvent e)
        {
            _newX = e.GetX();

            if (_newX - _oldX > 100)
            {
                ExecuteSwipeRight();
            }
            else if (_newX - _oldX < -100)
            {
                ExecuteSwipeLeft();
            }
            else
            {
                // Ignore
            }
        }

        private void ExecuteSwipeRight()
        {
            _customWebView.SwipedCommand.Execute(SwipedDirection.Right);
        }

        private void ExecuteSwipeLeft()
        {
            _customWebView.SwipedCommand.Execute(SwipedDirection.Left);
        }
    }
}