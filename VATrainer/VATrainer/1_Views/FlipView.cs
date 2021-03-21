using System;
using System.Threading.Tasks;
using VATrainer.ViewModels;
using Xamarin.Forms;

namespace VATrainer.Views
{
    public class FlipView : ContentView
    {
        private const uint FlipTime = 800;
        private const double ScaleFactor = 0.5;

        private readonly RelativeLayout _contentHolder;
        private readonly double _screenWidth;

        public FlipView()
        {
            _contentHolder = new RelativeLayout();
            _screenWidth = Application.Current.MainPage.Width;
            Content = _contentHolder;
        }

        public View FrontView
        {
            get { return (View)this.GetValue(FrontViewProperty); }
            set { this.SetValue(FrontViewProperty, value); }
        }

        public static readonly BindableProperty FrontViewProperty =
            BindableProperty.Create(
                nameof(FrontView),
                typeof(View),
                typeof(FlipView),
                null,
                BindingMode.Default,
                null,
                FrontViewPropertyChanged);

        private static void FrontViewPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // Set FrontView visble at start
            if (newValue != null)
            {
                ((FlipView)bindable)
                    ._contentHolder
                    .Children
                    .Add(((FlipView)bindable).FrontView,
                        Constraint.Constant(0),
                        Constraint.Constant(0),
                        Constraint.RelativeToParent((parent) => parent.Width),
                        Constraint.RelativeToParent((parent) => parent.Height)
                    );
                ((FlipView)bindable).FrontView.IsVisible = true;
            }
        }

        public View BackView
        {
            get { return (View)this.GetValue(BackViewProperty); }
            set { this.SetValue(BackViewProperty, value); }
        }

        public static readonly BindableProperty BackViewProperty =
            BindableProperty.Create(
                nameof(BackView),
                typeof(View),
                typeof(FlipView),
                null,
                BindingMode.Default,
                null,
                BackViewPropertyChanged);

        private static void BackViewPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            //Set BackView invisible at start
            if (newvalue != null)
            {
                ((FlipView)bindable)
                    ._contentHolder
                    .Children
                    .Add(((FlipView)bindable).BackView,
                        Constraint.Constant(0),
                        Constraint.Constant(0),
                        Constraint.RelativeToParent((parent) => parent.Width),
                        Constraint.RelativeToParent((parent) => parent.Height)
                     );

                ((FlipView)bindable).BackView.IsVisible = false;
            }
        }

        public NextAnimationParams Next
        {
            get { return (NextAnimationParams)GetValue(NextProperty); }
            set { SetValue(NextProperty, value); }
        }

        public static readonly BindableProperty NextProperty =
            BindableProperty.Create(
                nameof(Next),
                typeof(NextAnimationParams),
                typeof(FlipView),
                null,
                BindingMode.Default,
                null,
                OnNextPropertyChanged);

        private static void OnNextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((FlipView)bindable).ExecuteNext((NextAnimationParams)newValue);
        }

        private async void ExecuteNext(NextAnimationParams nextAnimationParams)
        {
            //TODO: ExecuteNext is called after navigating back (<-) with parameter null
            if (nextAnimationParams != null)
            {
                if(nextAnimationParams.Card == Card.MoveOut)
                {
                    await ExecuteMoveOut(nextAnimationParams);
                    nextAnimationParams.TriggerCallback();
                }
                else if (nextAnimationParams.Card == Card.SetContent)
                {
                    await ExecuteSetContent();
                    nextAnimationParams.TriggerCallback();
                }
                else if (nextAnimationParams.Card == Card.MoveIn)
                {
                    await ExecuteMoveIn();
                    nextAnimationParams.TriggerCallback();
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        private async Task ExecuteMoveOut(NextAnimationParams nextAnimationParams)
        {
            await this.ScaleTo(ScaleFactor, FlipTime / 2, Easing.Linear);
            if (Confidence.Confident == nextAnimationParams.Confidence)
            {
                await this.TranslateTo(_screenWidth, 0, FlipTime / 2, Easing.Linear);
            }
            else
            {
                await this.TranslateTo(-_screenWidth, 0, FlipTime / 2, Easing.Linear);
            }
        }

        private async Task ExecuteSetContent()
        {
            FrontView.IsVisible = true;
            BackView.IsVisible = false;
            await this.TranslateTo(0, 0, 0, Easing.Linear);
        }

        private async Task ExecuteMoveIn()
        {
            await this.ScaleTo(1, 1, Easing.Linear);
        }

        public FlipParams Flip
        {
            get { return (FlipParams)GetValue(FlipProperty); }
            set { SetValue(FlipProperty, value); }
        }

        public static readonly BindableProperty FlipProperty =
            BindableProperty.Create(
                nameof(Flip),
                typeof(FlipParams),
                typeof(FlipView),
                null,
                BindingMode.Default,
                null,
                OnFlipPropertyChanged);

        private static void OnFlipPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((FlipView)bindable).ExecuteFlip((FlipParams)newValue);
        }

        private async void ExecuteFlip(FlipParams flipAnimationParams)
        {
            //TODO: ExecuteFlip is called after navigating back (<-) with parameter null
            if (flipAnimationParams != null)
            {
                if (FlipDirection.Right == flipAnimationParams.Direction)
                {
                    await FlipRight();
                }
                else if (FlipDirection.Left == flipAnimationParams.Direction)
                {
                    await FlipLeft();
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        private async Task FlipRight()
        {
            RotationY = 0;
            await this.ScaleTo(ScaleFactor, 2 * FlipTime / 16, Easing.Linear);
            await this.RotateYTo(90, 6 * FlipTime / 16, Easing.Linear);

            FrontView.IsVisible = !FrontView.IsVisible;
            BackView.IsVisible = !BackView.IsVisible;

            RotationY = 270;
            await this.RotateYTo(360, 6 * FlipTime / 16, Easing.Linear);
            await this.ScaleTo(1, 2 * FlipTime / 16, Easing.Linear);
        }

        private async Task FlipLeft()
        {
            RotationY = 360;
            await this.ScaleTo(ScaleFactor, 2 * FlipTime / 16, Easing.Linear);
            await this.RotateYTo(270, 6 * FlipTime / 16, Easing.Linear);

            FrontView.IsVisible = !FrontView.IsVisible;
            BackView.IsVisible = !BackView.IsVisible;

            RotationY = 90;
            await this.RotateYTo(0, 6 * FlipTime / 16, Easing.Linear);
            await this.ScaleTo(1, 2 * FlipTime / 16, Easing.Linear);
        }
    }
}
