using Polly;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using ManagerHours.Model;
using ManagerHours._Util;
using System.Threading.Tasks;
using ManagerHours.Interfaces;

namespace ManagerHours.Implementation
{
    public class SpreadsheetService : ISpreadsheetService
    {
        private readonly HttpClient _client;
        private readonly string _pathServiceInfo = "api/spreadsheet/info";
        private readonly string _pathServiceUpdateInfo = "api/spreadsheet/info/modifyInfo";

        public SpreadsheetService()
        {
            _client = ServiceSettings.ServiceStartSettings();
        }

        public async Task<SpreadsheetInfo> GetSpreadsheetInfo()
        {
            SpreadsheetInfo spreadsheetInfo = null;
            HttpResponseMessage response = null;

            try
            {
                var result = await Policy.Handle<HttpRequestException>()
                  .OrResult<HttpResponseMessage>(r => (int)r.StatusCode != 200)
                  .WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(10))
                  .ExecuteAsync(async () =>
                  {
                      response = await _client.GetAsync($"{_client.BaseAddress + _pathServiceInfo}");
                      Debug.WriteLine($"{ DateTime.Now } - Enviando requisição ao servidor para pegar informações da última atualização da planilha");
                      return response;
                  });

                if (response.IsSuccessStatusCode)
                {
                    var result_response = await response.Content.ReadAsStringAsync();
                    spreadsheetInfo = JsonConvert.DeserializeObject<SpreadsheetInfo>(result_response);
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

            return spreadsheetInfo;
        }

        public async Task<SpreadsheetUpdateInfo> GetSpreadsheetUpdateInfo()
        {
            SpreadsheetUpdateInfo spreadsheetUpdateInfo = null;
            HttpResponseMessage response = null;

            try
            {
                var result = await Policy.Handle<HttpRequestException>()
                  .OrResult<HttpResponseMessage>(r => (int)r.StatusCode != 200)
                  .WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(10))
                  .ExecuteAsync(async () =>
                  {
                      response = await _client.GetAsync($"{_client.BaseAddress + _pathServiceUpdateInfo}");
                      Debug.WriteLine($"{ DateTime.Now } - Enviando requisição ao servidor para pegar informações da última atualização da planilha");
                      return response;
                  });

                if (response.IsSuccessStatusCode)
                {
                    var result_response = await response.Content.ReadAsStringAsync();
                    spreadsheetUpdateInfo = JsonConvert.DeserializeObject<SpreadsheetUpdateInfo>(result_response);
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

            return spreadsheetUpdateInfo;
        }
    }
}
