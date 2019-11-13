using System;
using Xamarin.Forms.Xaml;
using ManagerHours.Services;
using Rg.Plugins.Popup.Extensions;

namespace ManagerHours.View.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InserirObsPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        private readonly DateTime dtPonto;

        public InserirObsPopup(DateTime dateTimePonto)
        {
            InitializeComponent();

            dtPonto = dateTimePonto;

            lbl_avisoTxt.Text = AvisoHorasExtras(dtPonto);
        }

        private async void CloseButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync(true);
        }

        private string AvisoHorasExtras(DateTime dt)
        {

            TimeSpan finalHorasNormais = new TimeSpan(17,49,00);
            TimeSpan dataHoraAgora = 
                new TimeSpan(dt.Hour, dt.Minute, dt.Second);

            TimeSpan diffExtra = new TimeSpan(dataHoraAgora.Ticks - finalHorasNormais.Ticks);

            string message = string.Empty;

            if (diffExtra.Hours == 0 && diffExtra.Minutes != 0)
                message = $"{diffExtra.Minutes} minutos";
            else if (diffExtra.Hours != 0 && diffExtra.Minutes != 0)
                message = $"{diffExtra.Hours} horas e {diffExtra.Minutes} minutos";
            else
                message = "-";

            return $"O horário de expediente normal já foi ultrapassado " +
                $"e foi acumulado {message} de horário extra, " +
                $"e para salvar esse ponto será necessário inserir uma " +
                $"justificativa pela saída mais tarde!";
        }

        private async void BtnEnviar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new LoaderInserindoNovoPontoPopup(txt_obs.Text, dtPonto,true));
        }

        private void Editor_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (txt_obs.Text != string.Empty)
                BtnEnviar.IsEnabled = true;
            else
                BtnEnviar.IsEnabled = false;
        }
    }
}