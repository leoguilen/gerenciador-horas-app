using System;
using ManagerHours.Enum;
using ManagerHours.Model;
using Xamarin.Forms.Xaml;
using ManagerHours.Services;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;

namespace ManagerHours.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoaderInserindoNovoPontoPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        private readonly SendDate sendDate;

        public LoaderInserindoNovoPontoPopup(string messageObs, DateTime dtPonto, bool extra, bool onlyLoading = false)
        {
            InitializeComponent();

            sendDate = new SendDate();

            if(onlyLoading == false)
            {
                if (extra)
                    InserirComObs(messageObs, dtPonto);
                else
                    InserirValores(dtPonto);
            }
        }

        private async void InserirValores(DateTime dtPonto)
        {
            string data = dtPonto.ToString("dd/MM");
            //Evento env = new Evento().GetEventForTime(dtPonto);

            var dtPontoValue = new DateValue
            {
                Data = data,
                Evento = Evento.saida.ToString(),
                Hora = dtPonto.ToShortTimeString(),
                Autor = "Leonardo Guilen",
            };

            try
            {
                var sendDataValue = Task.FromResult(await sendDate.PostDateValuesAsync(dtPontoValue)).GetAwaiter();
            } catch (Exception ex) {
                confirmLoader.IsVisible = false;
                sendError.IsVisible = true;

                await DisplayAlert("ERRO", $"Encontramos um problema ao salvar o seu ponto. Mais detalhes {ex.Message}", "OK");  
            }


            confirmLoader.IsVisible = false;
            confirmedImg.IsVisible = true;

            await Task.Delay(1500);

            await DisplayAlert("SUCESSO", "Seu novo ponto foi salvo com sucesso!", "OK");

            await Navigation.PopAllPopupAsync();
        }

        private async void InserirComObs(string messageObs, DateTime dtPonto)
        {
            string data = dtPonto.ToString("dd/MM");
            string obs = messageObs;
            //Evento env = new Evento().GetEventForTime(dtPonto);

            var dtPontoValue = new DateValue
            {
                Data = data,
                Evento = Evento.entrada.ToString(),
                Hora = dtPonto.ToShortTimeString(),
                Autor = "Leonardo Guilen",
            };

            var obsValue = new ObsValue
            {
                Data = data,
                Observacao = obs
            };

            try
            {
                var sendDataValue = Task.FromResult(await sendDate.PostDateValuesAsync(dtPontoValue)).GetAwaiter();

                sendDataValue.OnCompleted(async () => 
                    Task.FromResult(await sendDate.PostObsValuesAsync(obsValue)).GetAwaiter());
            } catch (Exception ex) {
                confirmLoader.IsVisible = false;
                sendError.IsVisible = true;

                await DisplayAlert("ERRO", $"Encontramos um problema ao salvar o seu ponto. Mais detalhes {ex.Message}", "OK");
            }

            confirmLoader.IsVisible = false;
            confirmedImg.IsVisible = true;

            await Task.Delay(1500);

            await DisplayAlert("SUCESSO", "Seu novo ponto foi salvo com sucesso!","OK");

            await Navigation.PopAllPopupAsync();
        }
    }
}