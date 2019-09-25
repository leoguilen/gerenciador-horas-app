using GHoras.Aplicativo.Implementation;
using GHoras.Aplicativo.Interfaces;
using GHoras.Aplicativo.Model;
using System.Threading.Tasks;
using System.Net.Http;

namespace GHoras.Aplicativo.Service
{
    public class GetRow
    {
        private readonly IRowService _rowService;

        public GetRow()
        {
            _rowService = new RowService();
        }

        public async Task<Row> GetRowAsync(string date)
        {
            Row row;

            try
            {
                if (date.Contains("/"))
                    date = date.Remove(2, 1);

                row = await _rowService.GetRow(date);
            }
            catch (HttpRequestException)
            {
                row = null;
            }

            return row;
        }

        public async Task<Rows> GetRowsAsync()
        {
            Rows rows;

            try
            {
                rows = await _rowService.GetRows();
            }
            catch (HttpRequestException)
            {
                rows = null;
            }

            return rows;
        }

        public async Task<RowInfo> GetRowByIdAsync(int id)
        {
            RowInfo rowInfo;

            try
            {
                rowInfo = await _rowService.GetRowById(id);
            }
            catch (HttpRequestException)
            {
                rowInfo = null;
            }

            return rowInfo;
        }

        public async Task<RowInfo> GetRowByDateAsync(string date)
        {
            RowInfo rowInfo;

            try
            {
                if (date.Contains("/"))
                    date = date.Remove(2, 1);

                rowInfo = await _rowService.GetRowByDate(date);
            }
            catch (HttpRequestException)
            {
                rowInfo = null;
            }

            return rowInfo;
        }

    }
}
