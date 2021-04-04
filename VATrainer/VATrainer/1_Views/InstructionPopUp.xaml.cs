using DryIoc;
using Rg.Plugins.Popup.Pages;
using VATrainer.ViewModels;
using Xamarin.Forms.Xaml;

namespace VATrainer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InstructionPopUp : PopupPage
    {
        public InstructionPopUp()
        {
            InitializeComponent();
            BindingContext = new InstructionPopUpViewModel(App.AppContainer.Resolve<ISettings>());
        }
    }
}