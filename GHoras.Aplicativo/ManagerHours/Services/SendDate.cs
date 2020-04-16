using ManagerHours.Interfaces;
using ManagerHours.Model;
using System.Net.Http;
using System.Threading.Tasks;

namespace ManagerHours.Services
{
    public class SendDate
    {
        private readonly IDateService _dateService;

        public SendDate(IDateService dateService)
        {
            _dateService = dateService;
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
