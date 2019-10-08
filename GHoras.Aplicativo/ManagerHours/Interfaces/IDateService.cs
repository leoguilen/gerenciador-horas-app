using ManagerHours.Model;
using System.Threading.Tasks;

namespace ManagerHours.Interfaces
{
    public interface IDateService
    {
        Task<bool> SendDate(DateValue dateValue);
        Task<bool> SendObservation(ObsValue obsValue);
        Task<bool> DeleteDate(string id);
    }
}
