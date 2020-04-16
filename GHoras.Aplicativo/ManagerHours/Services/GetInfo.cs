using ManagerHours.Interfaces;
using ManagerHours.Model;
using System.Net.Http;
using System.Threading.Tasks;

namespace ManagerHours.Services
{
    public class GetInfo
    {
        private readonly ISpreadsheetService _spreadsheetService;

        public GetInfo(ISpreadsheetService spreadsheetService)
        {
            _spreadsheetService = spreadsheetService;
        }

        public async Task<SpreadsheetInfo> GetSpreadsheetInfoAsync()
        {
            SpreadsheetInfo spreadsheetInfo;

            try
            {
                spreadsheetInfo = await _spreadsheetService.GetSpreadsheetInfo();
            }
            catch (HttpRequestException)
            {
                spreadsheetInfo = null;
            }

            return spreadsheetInfo;
        }

        public async Task<SpreadsheetUpdateInfo> GetSpreadsheetUpdateInfoAsync()
        {
            SpreadsheetUpdateInfo spreadsheetUpdateInfo;

            try
            {
                spreadsheetUpdateInfo = await _spreadsheetService.GetSpreadsheetUpdateInfo();
            }
            catch (HttpRequestException)
            {
                spreadsheetUpdateInfo = null;
            }

            return spreadsheetUpdateInfo;
        }
    }
}
