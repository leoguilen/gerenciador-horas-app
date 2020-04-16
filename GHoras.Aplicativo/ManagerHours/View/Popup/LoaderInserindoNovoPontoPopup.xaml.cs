using ManagerHours.Dependencies;
using ManagerHours.Enum;
using ManagerHours.Model;
using ManagerHours.Services;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace ManagerHours.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoaderInserindoNovoPontoPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        private readonly SendDate sendDate;

        public LoaderInserindoNovoPontoPopup(string messageObs, DateTime dtPonto, bool extra, bool onlyLoading = false)
        {
            InitializeComponent();

            sendDate = new SendDate(DateServiceDependencies.Inject());

            if (!onlyLoading)
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

            var dtPontoValue = new DateValue
            {
                Data = data,
                Evento = dtPonto.GetEventForTime().Result,
                Hora = dtPonto.ToShortTimeString(),
                Autor = "Leonardo Guilen",
            };

            try
            {
                var sendDataValue = Task.FromResult(await sendDate
                    .PostDateValuesAsync(dtPontoValue)).ConfigureAwait(false).GetAwaiter();
            }
            catch (Exception ex)
            {
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

            var dtPontoValue = new DateValue
            {
                Data = data,
                Evento = dtPonto.GetEventForTime().Result,
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
                Parallel.Invoke(new ParallelOptions
                {
                    CancellationToken = CancellationToken.None,
                    MaxDegreeOfParallelism = 2,
                    TaskScheduler = null
                },
                async () =>
                {
                    Task.FromResult(await sendDate.PostDateValuesAsync(dtPontoValue))
                        .ConfigureAwait(false).GetAwaiter();
                },
                async () =>
                {
                    Task.FromResult(await sendDate.PostObsValuesAsync(obsValue))
                        .ConfigureAwait(false).GetAwaiter();
                });

            }
            catch (Exception ex)
            {
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
    }
}