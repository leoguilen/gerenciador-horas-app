using GHoras.Aplicativo.Model;
using System.Threading.Tasks;

namespace GHoras.Aplicativo.Interfaces
{
    public interface IRowService
    {
        Task<Rows> GetRows();
        Task<Row> GetRow(string date);
        Task<RowInfo> GetRowById(int id);
        Task<RowInfo> GetRowByDate(string date);
    }
}
