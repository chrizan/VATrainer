﻿using System.Windows.Input;
using Xamarin.Forms;

namespace VATrainer.Views
{
    public class FrameButton : Frame
    {
        public FrameButton()
        {
            Initialize();
        }

        private void Initialize()
        {
            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = TransitionCommand
            });
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(FrameButton),
            null);

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameter), 
            typeof(object), 
            typeof(FrameButton), 
            null);

        private ICommand TransitionCommand
        {
            get
            {
                return new Command(async () =>
                {
                    BackgroundColor = (Color)Prism.PrismApplicationBase.Current.Resources["BackgroundColorLight"];
                    await this.ScaleTo(0.8, 50, Easing.Linear);
                    await this.ScaleTo(1, 50, Easing.Linear);
                    BackgroundColor = Color.White;
                    Command?.Execute(CommandParameter);
                });
            }
        }
    }
}
