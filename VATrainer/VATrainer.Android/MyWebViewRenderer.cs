using Android.Content;
using Android.Views;
using VATrainer.Droid;
using VATrainer.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

// Corresponding custom renderer
[assembly: ExportRenderer(typeof(CustomWebView), typeof(MyWebViewRenderer))]
namespace VATrainer.Droid
{
    public class MyWebViewRenderer : WebViewRenderer
    {
        public MyWebViewRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            Control.SetOnTouchListener(new MyOnTouchListener((CustomWebView)Element));
        }

    }

    public class MyOnTouchListener : Java.Lang.Object, Android.Views.View.IOnTouchListener
    {
        float oldX;

        float newX;

        readonly CustomWebView myWebView;
        public MyOnTouchListener(CustomWebView webView)
        {
            myWebView = webView;
        }
        public bool OnTouch(Android.Views.View v, MotionEvent e)
        {
            if (e.Action == MotionEventActions.Down)
            {
                oldX = e.GetX(0);
            }
            if (e.Action == MotionEventActions.Up)
            {
                newX = e.GetX();

                if (newX - oldX > 100)
                {
                    myWebView.PannedCommand.Execute(PannedDirection.Right);
                }
                else if (newX - oldX < -100)
                {
                    myWebView.PannedCommand.Execute(PannedDirection.Left);
                }
            }

            return false;
        }
    }
}