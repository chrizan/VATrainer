using System;
using System.Threading.Tasks;
using VATrainer.ViewModels;
using Xamarin.Forms;

namespace VATrainer.Views
{
    public class FlipView : ContentView
    {
        private const uint FlipTime = 800;
        private readonly RelativeLayout contentHolder;
        private readonly double scaleFactor = 0.5;

        public FlipView()
        {
            contentHolder = new RelativeLayout();
            Content = contentHolder;
        }

        /// <summary>
        /// Gets or Sets the front view
        /// </summary>
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
                    .contentHolder
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

        /// <summary>
        /// Gets or Sets the back view
        /// </summary>
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
                    .contentHolder
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

        public FlipAnimationParams Flip
        {
            get { return (FlipAnimationParams)GetValue(FlipProperty); }
            set { SetValue(FlipProperty, value); }
        }

        public static readonly BindableProperty FlipProperty =
            BindableProperty.Create(
                nameof(Flip),
                typeof(FlipAnimationParams),
                typeof(FlipView),
                null,
                BindingMode.Default,
                null,
                OnFlipPropertyChanged);

        private static void OnFlipPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((FlipView)bindable).ExecuteFlip((FlipAnimationParams)newValue);
        }

        private async void ExecuteFlip(FlipAnimationParams flipAnimationParams)
        {
            //TODO : ExecuteFlip is called after navigating back (<-) with parameter null
            if (null == flipAnimationParams)
            {
                return;
            }
            if (FlipStep.FirstAndSecondQuarter == flipAnimationParams.Step)
            {
                await ExecuteFirstAndSecondQuarterFlip(flipAnimationParams);
                TriggerCallBack(flipAnimationParams);
            }
            else if (FlipStep.FirstQuarter == flipAnimationParams.Step)
            {
                await ExecuteFirstQuarterFlip(flipAnimationParams);
                TriggerCallBack(flipAnimationParams);
            }
            else if (FlipStep.SecondQuarter == flipAnimationParams.Step)
            {
                await ExecuteSecondQuarterFlip(flipAnimationParams);
                TriggerCallBack(flipAnimationParams);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void TriggerCallBack(FlipAnimationParams flipAnimationParams)
        {
            flipAnimationParams.TriggerCallback(FrontView.IsVisible);
        }

        private async Task ExecuteFirstAndSecondQuarterFlip(FlipAnimationParams flipAnimationParams)
        {
            if (FlipDirection.Right == flipAnimationParams.Direction)
            {
                //var parentAnimation = new Animation();
                //var scaleDownAnimation = new Animation(v => this.Scale = v, 1, 0.5, Easing.Linear);
                //var rotationAnimation = new Animation(v => this.RotationY = v, 0, 90, Easing.Linear);
                //parentAnimation.Add(0, 1.0, rotationAnimation);
                //parentAnimation.Add(0, 1.0, scaleDownAnimation);
                //parentAnimation.Commit(this, "animation", 16, FlipTime / 2);

                //await Task.Delay((int)FlipTime / 2);

                RotationY = 0;
                await this.ScaleTo(scaleFactor, 2 * FlipTime / 16, Easing.Linear);
                await this.RotateYTo(90, 6 * FlipTime / 16, Easing.Linear);

                FrontView.IsVisible = !FrontView.IsVisible;
                BackView.IsVisible = !BackView.IsVisible;

                RotationY = 270;
                await this.RotateYTo(360, 6 * FlipTime / 16, Easing.Linear);
                await this.ScaleTo(1, 2 * FlipTime / 16, Easing.Linear);

                //var parentAnimation1 = new Animation();
                //var scaleUpAnimation = new Animation(v => this.Scale = v, 0.5, 1, Easing.Linear);
                //var rotationAnimation1 = new Animation(v => this.RotationY = v, 270, 360, Easing.Linear);
                //parentAnimation1.Add(0, 1.0, scaleUpAnimation);
                //parentAnimation1.Add(0, 1.0, rotationAnimation1);
                //parentAnimation1.Commit(this, "animation1", 16, FlipTime / 2);
            }
            else if (FlipDirection.Left == flipAnimationParams.Direction)
            {
                RotationY = 360;
                await this.ScaleTo(scaleFactor, 2 * FlipTime / 16, Easing.Linear);
                await this.RotateYTo(270, 6 * FlipTime / 16, Easing.Linear);

                FrontView.IsVisible = !FrontView.IsVisible;
                BackView.IsVisible = !BackView.IsVisible;

                RotationY = 90;
                await this.RotateYTo(0, 6 * FlipTime / 16, Easing.Linear);
                await this.ScaleTo(1, 2 * FlipTime / 16, Easing.Linear);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private async Task ExecuteFirstQuarterFlip(FlipAnimationParams flipAnimationParams)
        {
            if (FlipDirection.Right == flipAnimationParams.Direction)
            {
                RotationY = 0;
                await this.ScaleTo(scaleFactor, 2 * FlipTime / 16, Easing.Linear);
                await this.RotateYTo(90, 6 * FlipTime / 16, Easing.Linear);
                FrontView.IsVisible = !FrontView.IsVisible;
                BackView.IsVisible = !BackView.IsVisible;
            }
            else if (FlipDirection.Left == flipAnimationParams.Direction)
            {
                RotationY = 360;
                await this.ScaleTo(scaleFactor, 2 * FlipTime / 16, Easing.Linear);
                await this.RotateYTo(270, 6 * FlipTime / 16, Easing.Linear);
                FrontView.IsVisible = !FrontView.IsVisible;
                BackView.IsVisible = !BackView.IsVisible;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private async Task ExecuteSecondQuarterFlip(FlipAnimationParams flipAnimationParams)
        {
            if (FlipDirection.Right == flipAnimationParams.Direction)
            {
                RotationY = 270;
                await this.RotateYTo(360, 6 * FlipTime / 16, Easing.Linear);
                await this.ScaleTo(1, 2 * FlipTime / 16, Easing.Linear);
            }
            else if (FlipDirection.Left == flipAnimationParams.Direction)
            {
                RotationY = 90;
                await this.RotateYTo(0, 6 * FlipTime / 16, Easing.Linear);
                await this.ScaleTo(1, 2 * FlipTime / 16, Easing.Linear);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
