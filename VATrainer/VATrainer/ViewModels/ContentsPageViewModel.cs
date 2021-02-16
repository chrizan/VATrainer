using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.Generic;
using VATrainer.Models;
using Xamarin.Forms;

namespace VATrainer.ViewModels
{
    public class ContentsPageViewModel : BindableBase, INavigationAware
    {
        private readonly IRepository _repository;
        private readonly IWebpageCreator _webpageCreator;

        private HtmlWebViewSource _content;

        public ContentsPageViewModel(IRepository repository, IWebpageCreator webpageCreator)
        {
            _repository = repository;
            _webpageCreator = webpageCreator;
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            int themeId = int.Parse(parameters.GetValue<string>("theme"));
            Content = new HtmlWebViewSource
            { 
                Html = GetContent(themeId)
            };
        }

        private string GetContent(int themeId)
        {
            List<Question> questions = _repository.GetAllQuestionsOfTheme(themeId).Result;
            return _webpageCreator.CreateContentWebpage(questions);
        }

        public HtmlWebViewSource Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            //Currently not needed
        }
    }
}
