using System.Net.Http;
using System.Threading.Tasks;
using GHoras.Aplicativo.Model;
using GHoras.Aplicativo.Interfaces;
using GHoras.Aplicativo.Implementation;

namespace GHoras.Aplicativo.Service
{
    public class SendDate
    {
        private readonly IDateService _dateService;

        public SendDate()
        {
            _dateService = new DateService();
        }

        public async Task<bool> PostDateValuesAsync(DateValue dateValue)
        {
            bool result;

            try
            {
                result = await _dateService.SendDate(dateValue);
            }
            catch (HttpRequestException)
            {
                result = false;
            }

            return result;
        }

        public async Task<bool> PostObsValuesAsync(ObsValue obsValue)
        {
            bool result;

            try
            {
                result = await _dateService.SendObservation(obsValue);
            }
            catch (HttpRequestException)
            {
                result = false;
            }

            return result;
        }
    }
}
