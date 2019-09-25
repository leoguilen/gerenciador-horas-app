using GHoras.Aplicativo.Model;
using System.Threading.Tasks;

namespace GHoras.Aplicativo.Interfaces
{
    public interface IDateService
    {
        Task<bool> SendDate(DateValue dateValue);
        Task<bool> SendObservation(ObsValue obsValue);
    }
}
