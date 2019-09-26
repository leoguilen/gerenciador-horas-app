using GHoras.Aplicativo.Implementation;
using GHoras.Aplicativo.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace GHoras.Aplicativo.Service
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
