using Polly;
using Newtonsoft.Json;
using System.Net.Http;
using ManagerHours._Util;
using System.Threading.Tasks;
using ManagerHours.Model;
using ManagerHours.Interfaces;
using System.Diagnostics;
using System;

namespace ManagerHours.Implementation
{
    public class RowService : IRowService
    {
        private readonly HttpClient _client;
        private readonly string _pathServiceRow = "api/spreadsheet/data/cell";
        private readonly string _pathServiceRows = "api/spreadsheet/data/cells";
        private readonly string _pathServiceRowInfoById = "api/spreadsheet/data/infoCell";
        private readonly string _pathServiceRowInfoByDate = "api/spreadsheet/data/indexCell";

        public RowService()
        {
            _client = ServiceSettings.ServiceStartSettings();
        }

        public async Task<Row> GetRow(string date)
        {
            Row rowValue = null;
            HttpResponseMessage response = null;

            try
            {
                var result = await Policy.Handle<HttpRequestException>()
                    .OrResult<HttpResponseMessage>(r => (int)r.StatusCode != 200)
                    .RetryAsync(3)
                    .ExecuteAsync(async () => 
                    {
                        response = await _client.GetAsync($"{_client.BaseAddress + _pathServiceRow}/{date}");
                        Debug.WriteLine($"{ DateTime.Now} - Enviando requisição ao servidor");
                        return response;
                    });

                if (result.IsSuccessStatusCode)
                {
                    var result_response = await result.Content.ReadAsStringAsync();
                    rowValue = JsonConvert.DeserializeObject<Row>(result_response);
                }
                else
                {
                    throw new HttpRequestException($"Erro na requisição ao serviço. Requisição retornou com o status '{result.StatusCode}'");
                }

                result.Dispose();
            }
            catch
            {
                throw new HttpRequestException($"Erro na requisição ao serviço. Requisição retornou com o status '{response.StatusCode}'");
            }
            finally
            {
                response.Dispose();
                
            }

            return rowValue;
        }

        public async Task<Rows> GetRows()
        {
            Rows rowsValues;
            HttpResponseMessage response = null;

            try
            {
                var result = await Policy.Handle<HttpRequestException>()
                   .OrResult<HttpResponseMessage>(r => (int)r.StatusCode != 200)
                   .WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(10))
                   .ExecuteAsync(async () =>
                   {
                       response = await _client.GetAsync(_client.BaseAddress + _pathServiceRows);
                       Debug.WriteLine($"{ DateTime.Now} - Enviando requisição ao servidor");
                       return response;
                   });

                if (result.IsSuccessStatusCode)
                {
                    var result_response = await result.Content.ReadAsStringAsync();
                    rowsValues = JsonConvert.DeserializeObject<Rows>(result_response);
                }
                else
                {
                    throw new HttpRequestException($"Erro na requisição ao serviço. Requisição retornou com o status '{result.StatusCode}'");
                }

                result.Dispose();
            }
            catch
            {
                throw new HttpRequestException($"Erro na requisição ao serviço. Requisição retornou com o status '{response.StatusCode}'");
            }
            finally
            {
                response.Dispose();
            }

            return rowsValues;
        }
        public async Task<RowInfo> GetRowByDate(string date)
        {
            RowInfo rowInfo;
            HttpResponseMessage response = null;

            try
            {
                response = await _client.GetAsync(_client.BaseAddress + _pathServiceRowInfoByDate + $"/{date}");

                if (response.IsSuccessStatusCode)
                {
                    var result_response = await response.Content.ReadAsStringAsync();
                    rowInfo = JsonConvert.DeserializeObject<RowInfo>(result_response);
                }
                else
                {
                    throw new HttpRequestException($"Erro na requisição ao serviço. Requisição retornou com o status '{response.StatusCode}'");
                }
            }
            catch
            {
                throw new HttpRequestException($"Erro na requisição ao serviço. Requisição retornou com o status '{response.StatusCode}'");
            }
            finally
            {
                response.Dispose();
            }

            return rowInfo;
        }

        public async Task<RowInfo> GetRowById(int id)
        {
            RowInfo rowInfo;
            HttpResponseMessage response = null;

            try
            {
                response = await _client.GetAsync(_client.BaseAddress + _pathServiceRowInfoById + $"/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var result_response = await response.Content.ReadAsStringAsync();
                    rowInfo = JsonConvert.DeserializeObject<RowInfo>(result_response);
                }
                else
                {
                    throw new HttpRequestException($"Erro na requisição ao serviço. Requisição retornou com o status '{response.StatusCode}'");
                }
            }
            catch
            {
                throw new HttpRequestException($"Erro na requisição ao serviço. Requisição retornou com o status '{response.StatusCode}'");
            }
            finally
            {
                response.Dispose();
            }

            return rowInfo;
        }

    }
}
