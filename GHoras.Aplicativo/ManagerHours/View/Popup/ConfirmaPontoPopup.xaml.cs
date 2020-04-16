using System;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using ManagerHours.View.Popup;
using Rg.Plugins.Popup.Extensions;

namespace ManagerHours.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmaPontoPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        private readonly DateTime dateTimeCurrent = DateTime.Now;

        public ConfirmaPontoPopup()
        {
            InitializeComponent();

            lbl_confirmText.Text = $"Confirmar o horário {dateTimeCurrent} como um novo evento de ponto?";
        }

        private async void Btn_confirm_Clicked(object sender, EventArgs e)
        {
            btn_confirm.IsVisible = false;
            btn_confirm_actived.IsVisible = true;

            await Task.Delay(150);

            btn_confirm.IsVisible = true;
            btn_confirm_actived.IsVisible = false;

            if(!(dateTimeCurrent.Hour >= 8 && dateTimeCurrent.Hour >= 18)) {
                await Navigation.PushPopupAsync(new InserirObsPopup(dateTimeCurrent));
            } else {
                await Navigation.PushPopupAsync(new LoaderInserindoNovoPontoPopup(string.Empty,dateTimeCurrent,false));
            }
        }

        private async void CloseButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync(true);
        }
        private async void Btn_cancel_Clicked(object sender, EventArgs e)
        { 
            btn_cancel.IsVisible = false;
            btn_cancel_actived.IsVisible = true;

            await Task.Delay(150);

            btn_cancel.IsVisible = true;
            btn_cancel_actived.IsVisible = false;

            await Navigation.PopPopupAsync(true);
        }
    }
}