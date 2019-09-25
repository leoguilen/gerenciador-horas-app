using System.Threading.Tasks;
using GHoras.Aplicativo.Model;
using GHoras.Aplicativo.Interfaces;
using GHoras.Aplicativo.Implementation;
using System.Net.Http;

namespace GHoras.Aplicativo.Service
{
    public class GetInfo
    {
        private readonly ISpreadsheetService _spreadsheetService;

        public GetInfo()
        {
            _spreadsheetService = new SpreadsheetService();
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
