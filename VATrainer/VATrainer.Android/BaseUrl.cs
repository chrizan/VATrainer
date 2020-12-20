using VATrainer.Droid;
using VATrainer.ViewModels;
using Xamarin.Forms;

[assembly: Dependency(typeof(BaseUrl))]
namespace VATrainer.Droid
{
    public class BaseUrl : IBaseUrl
    {
        public string Get()
        {
            return "file:///android_asset/";
        }
    }
}