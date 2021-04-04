using Rg.Plugins.Popup.Pages;
using VATrainer.ViewModels;
using Xamarin.Forms.Xaml;

namespace VATrainer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetPopUp : PopupPage
    {
        public ResetPopUp()
        {
            InitializeComponent();
            BindingContext = new ResetPopUpViewModel();
        }
    }
}