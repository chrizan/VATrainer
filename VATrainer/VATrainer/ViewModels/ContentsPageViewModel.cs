using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using VATrainer.Models;
using Xamarin.Forms;

namespace VATrainer.ViewModels
{
    public class ContentsPageViewModel : BindableBase, INavigationAware
    {
        private readonly ISessionManager _sessionManager;
        private readonly IWebpageCreator _webpageCreator;

        private HtmlWebViewSource _content;

        public ContentsPageViewModel(ISessionManager sessionManager, IWebpageCreator webpageCreator)
        {
            _sessionManager = sessionManager;
            _webpageCreator = webpageCreator;
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Content = new HtmlWebViewSource
            {
                Html = GetContent(parameters.GetValue<string>("theme"))
            };
        }

        private string GetContent(string themeId)
        {
            return _sessionManager.Question.Text;
        }

        public HtmlWebViewSource Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
