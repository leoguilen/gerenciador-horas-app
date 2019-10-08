using Newtonsoft.Json;
using System.Net.Http;
using ManagerHours._Util;
using System.Threading.Tasks;
using ManagerHours.Model;
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
                response = await _client.GetAsync($"{_client.BaseAddress + _pathServiceInfo}");

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
                response = await _client.GetAsync($"{_client.BaseAddress + _pathServiceUpdateInfo}");

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
