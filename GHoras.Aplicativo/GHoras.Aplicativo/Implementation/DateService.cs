﻿using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using GHoras.Aplicativo._Util;
using GHoras.Aplicativo.Model;
using GHoras.Aplicativo.Interfaces;

namespace GHoras.Aplicativo.Implementation
{
    public class DateService : IDateService
    {
        private readonly HttpClient _client;
        private readonly string _pathServiceDate = "api/spreadsheet/data/insertTime";
        private readonly string _pathServiceObs = "api/spreadsheet/data/insertObs";

        public DateService()
        {
            _client = ServiceSettings.ServiceStartSettings();
        }

        public async Task<bool> SendDate(DateValue dateValue)
        {
            HttpResponseMessage response = null;
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

            return response.StatusCode == HttpStatusCode.OK ? true : false;
        }

        public async Task<bool> SendObservation(ObsValue obsValue)
        {
            HttpResponseMessage response = null;
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

            return response.StatusCode == HttpStatusCode.OK ? true : false;
        }
    }
}
