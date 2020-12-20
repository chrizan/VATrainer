using Foundation;
using VATrainer.iOS;
using VATrainer.ViewModels;
using Xamarin.Forms;

[assembly: Dependency(typeof(BaseUrl))]
namespace VATrainer.iOS
{
    public class BaseUrl : IBaseUrl
    {
        public string Get()
        {
            return NSBundle.MainBundle.BundlePath;
        }
    }
}