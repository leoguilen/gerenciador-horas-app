using ManagerHours.Model;
using System.Threading.Tasks;

namespace ManagerHours.Interfaces
{
    public interface IRowService
    {
        Task<Rows> GetRows();
        Task<Row> GetRow(string date);
        Task<RowInfo> GetRowById(int id);
        Task<RowInfo> GetRowByDate(string date);
    }
}
