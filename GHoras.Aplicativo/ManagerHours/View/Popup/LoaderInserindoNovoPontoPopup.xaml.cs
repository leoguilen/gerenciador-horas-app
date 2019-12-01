using System;
using ManagerHours.Enum;
using ManagerHours.Model;
using Xamarin.Forms.Xaml;
using ManagerHours.Services;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using ManagerHours._Util;

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
            GetEvento getEvento = new GetEvento();
            string data = dtPonto.ToString("dd/MM");
            
            var dtPontoValue = new DateValue
            {
                Data = data,
                Evento = await Task.FromResult(await getEvento.GetEventForTime(dtPonto)),
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
            GetEvento getEvento = new GetEvento();
            string data = dtPonto.ToString("dd/MM");
            string obs = messageObs;

            var dtPontoValue = new DateValue
            {
                Data = data,
                Evento = getEvento.GetEventForTime(dtPonto).ToString(),
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