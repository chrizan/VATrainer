﻿using Prism.Mvvm;
using System;
using VATrainer.Models;
using Xamarin.Forms;

namespace VATrainer.ViewModels
{
    public class SettingPageViewModel : BindableBase
    {
        private const string FontSizeUnit = "px";
        private const double FontSizeMin = 12;
        private const double FontSizeMax = 20;

        private const string PrimaryColorDark = "#320b86";
        private const string BackgroundColorLight = "#f5f5f5";

        private readonly ISettings _settings;
        private readonly Article _article;

        private FlipParams _flipAnimationParams;

        public SettingPageViewModel(IRepository repository, ISettings settings)
        {
            _settings = settings;
            _article = repository.GetArticleForId(3).Result;
        }

        public HtmlWebViewSource Article
        {
            get => new HtmlWebViewSource()
            {
                Html = $"<html style=background-color:{BackgroundColorLight};>" +
                $"<div style=color:{PrimaryColorDark};" +
                $"font-size:{_settings.FontSize}>" +
                $"{_article.Text}</div></html>"
            };
        }

        public double SliderValueMin
        {
            get => FontSizeMin;
        }

        public double SliderValueMax
        {
            get => FontSizeMax;
        }

        public double SliderValue
        {
            get => Convert.ToDouble(_settings.FontSize.Replace(FontSizeUnit, string.Empty));
            set
            {
                _settings.FontSize = Math.Round(value).ToString() + FontSizeUnit;
                RaisePropertyChanged(nameof(SliderValue));
                RaisePropertyChanged(nameof(Article));
            }
        }

        public bool DisplayInstruction
        {
            get => _settings.DisplayInstruction;
            set => _settings.DisplayInstruction = value;
        }

        public FlipParams Flip
        {
            get { return _flipAnimationParams; }
            set { SetProperty(ref _flipAnimationParams, value); }
        }

        public uint AnimationDuration
        {
            get => (uint)_settings.AnimationDuration;
        }

        public string AnimationSpeed
        {
            get => MapToString(_settings.AnimationDuration);
            set
            {
                int animationDuration = MapToInt(value);
                if (animationDuration != _settings.AnimationDuration)
                {
                    _settings.AnimationDuration = animationDuration;
                    RaisePropertyChanged(nameof(AnimationDuration));
                    Flip = new FlipParams(FlipDirection.Left);
                }
            }
        }

        private static string MapToString(int speed)
        {
            if (speed == 500)
            {
                return "Fast";
            }
            else if (speed == 750)
            {
                return "Medium";
            }
            else if (speed == 1000)
            {
                return "Slow";
            }
            else throw new NotImplementedException();
        }

        private static int MapToInt(string speed)
        {
            if (speed == "Fast")
            {
                return 500;
            }
            else if (speed == "Medium")
            {
                return 750;
            }
            else if (speed == "Slow")
            {
                return 1000;
            }
            else throw new NotImplementedException();
        }
    }
}
