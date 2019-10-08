using ManagerHours.Model;
using System.Threading.Tasks;

namespace ManagerHours.Interfaces
{
    public interface ISpreadsheetService
    {
        Task<SpreadsheetInfo> GetSpreadsheetInfo();
        Task<SpreadsheetUpdateInfo> GetSpreadsheetUpdateInfo();
    }
}
