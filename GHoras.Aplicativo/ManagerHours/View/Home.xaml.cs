using System;
using System.Linq;
using Xamarin.Forms;
using ManagerHours.Dtos;
using ManagerHours.Model;
using Xamarin.Forms.Xaml;
using ManagerHours._Util;
using ManagerHours.Services;
using System.Threading.Tasks;
using ManagerHours.View.Popup;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using System.Collections.ObjectModel;

namespace ManagerHours.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : TabbedPage
    {
        private GetInfo getInfo;
        private GetRow getRow;
        private readonly double valorHoraExtras = 10.50;
        public List<Carousel> infosCarouselList { get; set; }
        private ObservableCollection<HistoricoPonto> histPonto { get; set; }
        private HistoricoPonto pontos;
        public Home()
        {
            InitializeComponent();

            getInfo = new GetInfo();
            getRow = new GetRow();

            CarregarValores();
        }

        private async void Btn_refresh_Clicked(object sender, EventArgs e)
        {
            btn_refresh.IsVisible = false;
            refreshing.IsVisible = true;
            Infos.BindingContext = null;

            var rowsValues = Task.FromResult(await getRow.GetRowsAsync()).GetAwaiter();

            if (rowsValues.IsCompleted)
            {
                lbl_hrsExtra.Text = rowsValues.GetResult().
                    TotalHorasExtras.ToShortTimeString();

                var dtUltimaAtualizacao = Task.FromResult(await UltimaAtualizacao()).Result;
                lbl_ultimaAtualizacao.Text = $"Última atualização em " +
                    $"{dtUltimaAtualizacao.ToString()}";

                MontarInfosCarrosel(rowsValues.GetResult());

                btn_refresh.IsVisible = true;
                refreshing.IsVisible = false;
            }
        }

        private async void Btn_novo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new ConfirmaPontoPopup());
        }

        private async void CarregarValores()
        {
            try
            {
                Rows rowsValues = Task.FromResult(await getRow.GetRowsAsync()).Result;
                lbl_hrsExtra.Text = rowsValues.TotalHorasExtras.ToShortTimeString();

                var dtUltimaAtualizacao = Task.FromResult(await UltimaAtualizacao()).Result;
                lbl_ultimaAtualizacao.Text = $"Última atualização em " +
                    $"{dtUltimaAtualizacao.ToString()}";

                MontarInfosCarrosel(rowsValues);
            }
            catch (Exception e)
            {
                await DisplayAlert("Erro", $"{e.Message}", "OK");
            }
        }

        private async Task<DateTime> UltimaAtualizacao()
        {
            SpreadsheetUpdateInfo infoUpdate;
            DateTime? lastUpdateDt;

            try
            {
                infoUpdate = await getInfo.GetSpreadsheetUpdateInfoAsync();
                lastUpdateDt = infoUpdate.UltimaAtualizacao.ToLocalTime();
            }
            catch (Exception e)
            {
                await DisplayAlert("Erro", $"Erro - {e.Message}", "OK");
                lastUpdateDt = null;
            }

            return lastUpdateDt.Value;
        }

        private void MontarInfosCarrosel(Rows rows)
        {
            infosCarouselList = new List<Carousel>
            {
                new Carousel()
                {
                    Title = "Data do Último Ponto:",
                    Value = UltimoPonto(rows)
                },
                new Carousel()
                {
                    Title = "Valor Total das Horas Extras: ",
                    Value = CalcularValorHorasExtras(DateTime.Parse(lbl_hrsExtra.Text))
                },
                new Carousel()
                {
                    Title = "Saída mais tarde: ",
                    Value = SaidaMaisTarde(rows)
                },
                new Carousel()
                {
                    Title = "Média do Horário de Saída: ",
                    Value = MediaHoraDeSaida(rows)
                }
            };

            Infos.BindingContext = this;
        }

        private string UltimoPonto(Rows rows)
        {
            var linhaUltimoPonto = rows.RowsInList()
                                   .Where(r => r.Entrada != null);

            if (linhaUltimoPonto.Count() == 0)
                return "-";

            var ultimoPonto = linhaUltimoPonto.Last().LastEvent();

            return ultimoPonto.ToString();
        }

        private string CalcularValorHorasExtras(DateTime horasExtras)
        {
            string[] dataSplited = horasExtras.ToLongTimeString().Split(':');
            string dataFormated = $"{dataSplited[0]},{dataSplited[1]}";
            double dataToDouble = double.Parse(dataFormated);
            double calcExtrasValor = valorHoraExtras * dataToDouble;

            if (calcExtrasValor == 0)
                return "-";

            return $"R$ {Math.Round(calcExtrasValor, 2)}";
        }

        private string SaidaMaisTarde(Rows rows)
        {
            List<DateTime> listaDeSaidas = new List<DateTime>();

            foreach (Row row in rows.RowsInList().Where(r => r.Saida != null))
            {
                listaDeSaidas.Add(row.Saida.Value);
            }

            if (listaDeSaidas.Count == 0)
                return "-";

            return $"{listaDeSaidas.Max()}";
        }

        private string MediaHoraDeSaida(Rows rows)
        {
            List<DateTime> listaDeSaidas = new List<DateTime>();

            var result = rows.RowsInList().Where(r => r.Saida != null);

            if (result.Count() > 0)
            {
                foreach (Row row in result)
                {
                    listaDeSaidas.Add(row.Saida.Value);
                }
            }
            else
            {
                return "-";
            }

            int qtdeSaidas = listaDeSaidas.Count;
            TimeSpan mediaHoraCalc;

            foreach (DateTime saida in listaDeSaidas)
            {
                TimeSpan t1 = new TimeSpan(saida.Ticks);
                mediaHoraCalc += t1;
            }

            long mediaCalculada = mediaHoraCalc.Ticks / qtdeSaidas;

            TimeSpan horaMedia = new TimeSpan(mediaCalculada);

            return $"{horaMedia.Hours}:{horaMedia.Minutes}:{horaMedia.Seconds}";
        }

        private async void Tab_hist_Appearing(object sender, EventArgs e)
        {
            histPonto = new ObservableCollection<HistoricoPonto>();

            await Navigation.PushPopupAsync(new LoaderInserindoNovoPontoPopup("",DateTime.Now,false,true),true);

            var result = Task.FromResult(await getRow.GetRowsAsync());

            if (result.IsCompleted)
                await Navigation.PopPopupAsync();

            List<Row> rows = result.Result.RowsInList();

            pontos = new HistoricoPonto() { Mes = $"Mês: {result.Result.Mes}" };

            foreach (var ponto in rows)
            {
                bool indicAzul, indicVerde;
                string status, corStatus, saida;
                string data = ponto.Data;

                if (ponto.Entrada.HasValue && ponto.SaidaAlmoco.HasValue 
                    && ponto.EntradaAlmoco.HasValue && ponto.Saida.HasValue)
                {
                    saida = ponto.Saida.Value.ToLongTimeString();

                    string[] splitSaida = saida.Split(':');

                    int hora = int.Parse(splitSaida[0]);

                    if (hora >= 18)
                    {
                        indicAzul = false;
                        indicVerde = true;
                        status = "Com Horas Extras";
                        corStatus = "#0F9D58";
                    }
                    else
                    {
                        indicAzul = true;
                        indicVerde = false;
                        status = "Horas Normais";
                        corStatus = "#0366D6";
                    }
                } else {
                    saida = "-";
                    indicAzul = false;
                    indicVerde = false;
                    status = "Sem Valor";
                    corStatus = "#9FA1A4";
                }

                pontos.Add(new PontoDto { Data = data, Saida = saida, IndicAzul = indicAzul, IndicVerde = indicVerde, Status = status, CorStatus = corStatus });
            }

            histPonto.Add(pontos);
            listHistorico.ItemsSource = histPonto;
        }

        private async void ListHistorico_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var dataItem = e.Item as PontoDto;

            await Navigation.PushPopupAsync(new MostraDetalhesPontoPopup(dataItem.Data));
        }

        private void CkbData_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value == true) {
                filtro_dt.IsEnabled = true;

            } else {
                filtro_dt.IsEnabled = false;
                listHistorico.BeginRefresh();
                listHistorico.ItemsSource = histPonto;
                listHistorico.EndRefresh();
            }
        }

        private void Filtro_dt_DateSelected(object sender, DateChangedEventArgs e)
        {
            string[] splitDtSelecionada = e.NewDate.ToShortDateString().Split('/');
            string dtSelecionada = $"{splitDtSelecionada[0]}/{splitDtSelecionada[1]}";
            
            listHistorico.BeginRefresh();

            listHistorico.ItemsSource = histPonto.Select(p => p.Where(pt => pt.Data.Contains(dtSelecionada)));

            listHistorico.EndRefresh();
        }

        private void CkbHrsExtras_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            listHistorico.BeginRefresh();

            if (e.Value == true) {
                if (ckbOrdSaidaTarde.IsChecked)
                    listHistorico.ItemsSource = histPonto.Select(p => p.Where(pt => pt.IndicVerde == true).OrderByDescending(pt => pt.Saida));
                else
                    listHistorico.ItemsSource = histPonto.Select(p => p.Where(pt => pt.IndicVerde == true));
            } else {
                if (ckbOrdSaidaTarde.IsChecked)
                    listHistorico.ItemsSource = histPonto.Select(p => p.OrderByDescending(pt => pt.Saida));
                else
                    listHistorico.ItemsSource = histPonto;
            }

            listHistorico.EndRefresh();
        }

        private void CkbOrdSaidaTarde_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            listHistorico.BeginRefresh();

            if (e.Value == true) {
                if(ckbHrsExtras.IsChecked)
                    listHistorico.ItemsSource = histPonto.Select(p => p.OrderByDescending(pt => pt.Saida).Where(pt => pt.IndicVerde == true));
                else 
                    listHistorico.ItemsSource = histPonto.Select(p => p.OrderByDescending(pt => pt.Saida));
            } else {
                if(ckbHrsExtras.IsChecked)
                    listHistorico.ItemsSource = histPonto.Select(p => p.Where(pt => pt.IndicVerde == true));
                else
                    listHistorico.ItemsSource = histPonto;
            }

            listHistorico.EndRefresh();
        }
    }
}