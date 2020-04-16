using ManagerHours.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace ManagerHours.Services
{
    public class RemoveDate
    {
        private readonly IDateService _dateService;

        public RemoveDate(IDateService dateService)
        {
            _dateService = dateService;
        }

        public async Task<bool> DeleteDateAsync(string id)
        {
            bool result;

            try
            {
                result = await _dateService.DeleteDate(id);
            }
            catch (HttpRequestException)
            {
                result = false;
            }

            return result;
        }
    }
}
