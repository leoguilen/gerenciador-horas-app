using Xamarin.Forms.Xaml;

namespace ManagerHours.View.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AguardarRequisicoesPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public AguardarRequisicoesPopup(string messageErro, int retries)
        {
            InitializeComponent();

            lbl_tentativas.Text = 
                $"Um problema ocorreu ao executar a requisição." +
                $"(Tentativa {retries}). Mais detalhes: {messageErro}";
        }
    }
}