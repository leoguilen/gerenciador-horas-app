using ManagerHours._Util;
using ManagerHours.Model;
using ManagerHours.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ManagerHours.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : TabbedPage
    {
        private GetInfo getInfo;
        private GetRow getRow;
        private RemoveDate removeDate;
        private SendDate sendDate;
        private readonly double valorHoraExtras = 10.50;
        public List<Carousel> infosCarouselList { get; set; }

        public Home()
        {
            InitializeComponent();

            getInfo = new GetInfo();
            getRow = new GetRow();
            removeDate = new RemoveDate();
            sendDate = new SendDate();

            CarregarValores();
        }

        private async void Btn_refresh_Clicked(object sender, EventArgs e)
        {
            btn_refresh.IsVisible = false;
            refreshing.IsVisible = true;

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

        private void Btn_novo_Clicked(object sender, EventArgs e)
        {

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

            return $"R$ {Math.Round(calcExtrasValor,2)}";
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

            foreach (Row row in rows.RowsInList().Where(r => r.Saida != null))
            {
                listaDeSaidas.Add(row.Saida.Value);
            }

            double totalHorasSaida = 0;
            int qtdeSaidas = listaDeSaidas.Count;

            foreach (DateTime saida in listaDeSaidas)
            {
                string[] dataSplited = saida.ToLongTimeString().Split(':');
                string dataFormated = $"{dataSplited[0]},{dataSplited[1]}";
                double dataToDouble = double.Parse(dataFormated);
                totalHorasSaida = totalHorasSaida + dataToDouble;
            }

            double calcMediaHoraSaida = totalHorasSaida / qtdeSaidas;
            string[] partesHoraMedia = calcMediaHoraSaida.ToString().Split(',');
            DateTime mediaHoraSaida = DateTime.FromOADate(calcMediaHoraSaida);

            if (mediaHoraSaida.Hour == 0 && mediaHoraSaida.Minute == 0)
                return "-";

            return $"{mediaHoraSaida.ToLongTimeString()}";
        }
    }
}