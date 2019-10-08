using ManagerHours.Implementation;
using ManagerHours.Interfaces;
using System.Threading.Tasks;
using System.Net.Http;

namespace ManagerHours.Services
{
    public class RemoveDate
    {
        private readonly IDateService _dateService;

        public RemoveDate()
        {
            _dateService = new DateService();
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
