using Rg.Plugins.Popup.Extensions;
using System;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace ManagerHours.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmaPontoPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public ConfirmaPontoPopup()
        {
            InitializeComponent();
        }

        private async void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            btn_confirm.IsVisible = false;
            btn_confirm_actived.IsVisible = true;

            await Task.Delay(150);

            btn_confirm.IsVisible = true;
            btn_confirm_actived.IsVisible = false;

            await Navigation.PushPopupAsync(new LoaderInserindoNovoPontoPopup(), true);

            //await DisplayAlert("MENSAGEM", "Confirmado", "OK");

            // TODO: Popup com loader e mensagem de confirmação após a inserção da data
        }

        private async void ImageButton_Clicked_2(object sender, EventArgs e)
        {
            btn_cancel.IsVisible = false;
            btn_cancel_actived.IsVisible = true;

            await Task.Delay(150);

            btn_cancel.IsVisible = true;
            btn_cancel_actived.IsVisible = false;

            await DisplayAlert("MENSAGEM", "Operação Cancelada", "OK");

            await Navigation.PopPopupAsync(true);
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync(true);
        }
    }
}