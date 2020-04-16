using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ManagerHours._Util
{
    public class ServiceSettings
    {
        private const string EnderecoBase = "https://gerenciador-horas-service.herokuapp.com/";

        private ServiceSettings() { }

        public static HttpClient ServiceStartSettings()
        {
            HttpClient client = new HttpClient();

            client.Timeout = new TimeSpan(0, 0, 5);
            client.BaseAddress = new Uri(EnderecoBase);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        public static async Task<HttpStatusCode> ServiceState()
        {
            HttpResponseMessage result = new HttpResponseMessage();
            HttpClient client = ServiceStartSettings();
            const string pathCheckService = "api/checkService";

            try
            {
                result = await client.GetAsync(client.BaseAddress + pathCheckService);
                return result.StatusCode;
            }
            catch
            {
                return result.StatusCode;
            }
            finally
            {
                client.Dispose();
                result.Dispose();
            }
        }

    }
}
