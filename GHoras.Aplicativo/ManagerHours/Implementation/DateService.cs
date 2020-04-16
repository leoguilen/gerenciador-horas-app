using ManagerHours._Util;
using ManagerHours.Interfaces;
using ManagerHours.Model;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ManagerHours.Implementation
{
    public class DateService : IDateService
    {
        private readonly HttpClient _client;
        private readonly string _pathServiceDate = "api/spreadsheet/data/insertTime";
        private readonly string _pathServiceObs = "api/spreadsheet/data/insertObs";
        private readonly string _pathServiceDelDate = "api/spreadsheet/data/deleteCell";

        public DateService()
        {
            _client = ServiceSettings.ServiceStartSettings();
        }

        public async Task<bool> DeleteDate(string id)
        {
            HttpResponseMessage response = null;

            try
            {
                response = await _client.DeleteAsync($"{_pathServiceDelDate}/{id}");

                if (response.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch
            {
                throw new HttpRequestException($"Erro na requisição ao serviço. Requisição retornou com o status '{response.StatusCode}'");
            }
            finally
            {
                response.Dispose();
            }
        }

        public async Task<bool> SendDate(DateValue dateValue)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            string dateValueJson = JsonConvert.SerializeObject(dateValue);

            using (HttpContent body = new StringContent(dateValueJson, Encoding.UTF8, "application/json"))
            {
                try
                {
                    response = await _client.PostAsync($"{_client.BaseAddress + _pathServiceDate}", body);
                    response.EnsureSuccessStatusCode();
                }
                catch
                {
                    throw new HttpRequestException($"Erro na requisição ao serviço. Requisição retornou com o status '{response.StatusCode}'");
                }
                finally
                {
                    response.Dispose();
                    body.Dispose();
                }
            }

            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> SendObservation(ObsValue obsValue)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            string obsValueJson = JsonConvert.SerializeObject(obsValue);

            using (HttpContent body = new StringContent(obsValueJson, Encoding.UTF8, "application/json"))
            {
                try
                {
                    response = await _client.PostAsync($"{_client.BaseAddress + _pathServiceObs}", body);
                    response.EnsureSuccessStatusCode();
                }
                catch
                {
                    throw new HttpRequestException($"Erro na requisição ao serviço. Requisição retornou com o status '{response.StatusCode}'");
                }
                finally
                {
                    response.Dispose();
                    body.Dispose();
                }
            }

            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
