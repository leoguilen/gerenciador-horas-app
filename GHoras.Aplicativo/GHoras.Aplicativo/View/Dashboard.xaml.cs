using System;
using System.Net;
using Xamarin.Forms;
using System.Net.Http;
using Xamarin.Forms.Xaml;
using GHoras.Aplicativo.Enum;
using GHoras.Aplicativo.Model;
using GHoras.Aplicativo._Util;
using GHoras.Aplicativo.Service;

namespace GHoras.Aplicativo.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : ContentPage
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private async void Btn_teste_Clicked(object sender, EventArgs e)
        {
            var statusCode = await ServiceSettings.ServiceState();

            if (statusCode == HttpStatusCode.OK)
                await DisplayAlert("Alerta", $"Serviço está rodando. Status: {HttpStatusCode.OK}", "OK");
            else
                await DisplayAlert("Alerta", $"Problemas com a conexão ao serviço. Status: {statusCode}","OK");
        }

        private async void Btn_teste2_Clicked(object sender, EventArgs e)
        {
            GetRow getRow = new GetRow();

            try
            {
                var row = await getRow.GetRowAsync("01/09");

                if (row == null)
                    throw new HttpRequestException();

                await DisplayAlert("Alerta", $"Valores linha: Data: {row.Data} | Entrada: {row.Entrada}","OK");
            }
            catch (HttpRequestException ex)
            {
                await DisplayAlert("Alerta", $"Detalhes erro: {ex.Message}", "OK");  
            }
        }

        private async void Btn_teste3_Clicked(object sender, EventArgs e)
        {
            GetRow getRow = new GetRow();

            try
            {
                var rows = await getRow.GetRowsAsync();

                if(rows == null)
                    throw new HttpRequestException();

                await DisplayAlert("Alerta", $"Valores linhas: Data 01: {rows.Data_01.Data} | Data 02: {rows.Data_02.Data}", "OK");
            }
            catch (HttpRequestException ex)
            {
                await DisplayAlert("Alerta", $"Detalhes erro: {ex.Message}", "OK");
            }
        }

        private async void Btn_teste4_Clicked(object sender, EventArgs e)
        {
            GetRow getRow = new GetRow();

            try
            {
                var rowInfo = await getRow.GetRowByIdAsync(25);

                if(rowInfo == null)
                    throw new HttpRequestException();

                await DisplayAlert("Alerta", $"Valores linhas: Index Coluna: {rowInfo.IndexCol} " +
                    $"| Index Linha: {rowInfo.IndexRow} | Valor: {rowInfo.Value} | ID: {rowInfo.BatchID}" , "OK");
            }
            catch (HttpRequestException ex)
            {
                await DisplayAlert("Alerta", $"Detalhes erro: {ex.Message}", "OK");
            }
        }

        private async void Btn_teste5_Clicked(object sender, EventArgs e)
        {
            GetRow getRow = new GetRow();

            try
            {
                var rowInfo = await getRow.GetRowByDateAsync("10/09");

                if (rowInfo == null)
                    throw new HttpRequestException();

                await DisplayAlert("Alerta", $"Valores linhas: Index Coluna: {rowInfo.IndexCol} " +
                    $"| Index Linha: {rowInfo.IndexRow} | Valor: {rowInfo.Value} | ID: {rowInfo.BatchID}", "OK");
            }
            catch (HttpRequestException ex)
            {
                await DisplayAlert("Alerta", $"Detalhes erro: {ex.Message}", "OK");
            }
        }

        private async void Btn_teste6_Clicked(object sender, EventArgs e)
        {
            GetInfo getInfo = new GetInfo();

            try
            {
                var planInfo = await getInfo.GetSpreadsheetInfoAsync();

                if(planInfo == null)
                    throw new HttpRequestException();

                await DisplayAlert("Alerta", $"Info Planilha: Titulo: {planInfo.Titulo} " +
                    $"| Qtde Linhas: {planInfo.QuantidadeDeLinhas} | Qtde Colunas: {planInfo.QuantidadeDeColunas}", "OK");
            }
            catch (HttpRequestException ex)
            {
                await DisplayAlert("Alerta", $"Detalhes erro: {ex.Message}", "OK");
            }
        }

        private async void Btn_teste7_Clicked(object sender, EventArgs e)
        {
            GetInfo getInfo = new GetInfo();

            try
            {
                var planUpdateInfo = await getInfo.GetSpreadsheetUpdateInfoAsync();

                if (planUpdateInfo == null)
                    throw new HttpRequestException();

                await DisplayAlert("Alerta", $"Ultima Atualização: {planUpdateInfo.UltimaAtualizacao} " +
                    $"| Nome do Autor: {planUpdateInfo.NomeAutor} | Email do Autor: {planUpdateInfo.EmailAutor}", "OK");
            }
            catch (HttpRequestException ex)
            {
                await DisplayAlert("Alerta", $"Detalhes erro: {ex.Message}", "OK");
            }
        }

        private async void Btn_teste8_Clicked(object sender, EventArgs e)
        {
            SendDate sendDate = new SendDate();
            var dateValues = new DateValue()
            {
                Data = "01/09",
                Evento = Evento.entrada.ToString(),
                Hora = "08:00:00",
                Autor = "Leonardo Guilen"
            };

            try
            {
                if(await sendDate.PostDateValuesAsync(dateValues) == true)
                    await DisplayAlert("Alerta", "Novo valor inserido com sucesso!", "OK");
                else
                    await DisplayAlert("Alerta", "Falha ao inserir um novo valor", "OK");
            } catch (Exception ex)
            {
                await DisplayAlert("Alerta", $"Erro: {ex.Message} ", "OK");
            }
        }

        private async void Btn_teste9_Clicked(object sender, EventArgs e)
        {
            SendDate sendDate = new SendDate();
            var obsValues = new ObsValue()
            {
                Data = "01/09",
                Observacao = "Produção do sistema da Dow Corning"
            };

            try
            {
                if (await sendDate.PostObsValuesAsync(obsValues) == true)
                    await DisplayAlert("Alerta", "Novo valor inserido com sucesso!", "OK");
                else
                    await DisplayAlert("Alerta", "Falha ao inserir um novo valor", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", $"Erro: {ex.Message} ", "OK");
            }
        }

        private async void Btn_teste10_Clicked(object sender, EventArgs e)
        {
            RemoveDate deleteDate = new RemoveDate();
            string batchID = "R5C3";

            try
            {
                if(await deleteDate.DeleteDateAsync(batchID) == true)
                    await DisplayAlert("Alerta", "Valor deletado com sucesso!", "OK");
                else
                    await DisplayAlert("Alerta", "Falha ao deletar um valor", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", $"Erro: {ex.Message} ", "OK");
            }
        }
    }
}