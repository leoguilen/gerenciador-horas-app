using GHoras.Aplicativo.Model;
using System.Threading.Tasks;

namespace GHoras.Aplicativo.Interfaces
{
    public interface ISpreadsheetService
    {
        Task<SpreadsheetInfo> GetSpreadsheetInfo();
        Task<SpreadsheetUpdateInfo> GetSpreadsheetUpdateInfo();
    }
}
