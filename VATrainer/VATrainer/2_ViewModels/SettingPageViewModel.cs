using Prism.Mvvm;
using System;
using VATrainer.Models;
using Xamarin.Forms;

namespace VATrainer.ViewModels
{
    public class SettingPageViewModel : BindableBase
    {
        private const string FontSizeUnit = "px";
        private const string FontSizeMin = "12";
        private const string FontSizeMax = "20";

        private readonly ISettings _settings;
        private readonly Article _article;

        public SettingPageViewModel(IRepository repository, ISettings settings)
        {
            _settings = settings;
            _article = repository.GetArticleForId(3).Result;
        } 

        public HtmlWebViewSource Article
        {
            get => new HtmlWebViewSource() { Html = $"<div style=font-size:{_settings.FontSize}>{_article.Text}</div>" };
        }

        public double SliderValueMin
        {
            get => Convert.ToDouble(FontSizeMin);
        }

        public double SliderValueMax
        {
            get => Convert.ToDouble(FontSizeMax);
        }

        public double SliderValue { 
            get => Convert.ToDouble(_settings.FontSize.Replace(FontSizeUnit, string.Empty));
            set
            {
                _settings.FontSize = ((int)value).ToString() + FontSizeUnit;
                RaisePropertyChanged(nameof(Article));
            }
        }
    }
}
