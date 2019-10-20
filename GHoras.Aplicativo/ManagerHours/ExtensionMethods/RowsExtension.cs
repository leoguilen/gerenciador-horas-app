using System;
using System.Collections.Generic;

namespace ManagerHours.Model
{
    public static class RowsExtension
    {
        public static List<Row> RowsInList(this Rows rows)
        {
            List<Row> rowsList = new List<Row>()
            {
                rows.Data_01,
                rows.Data_02,
                rows.Data_03,
                rows.Data_04,
                rows.Data_05,
                rows.Data_06,
                rows.Data_07,
                rows.Data_08,
                rows.Data_09,
                rows.Data_10,
                rows.Data_11,
                rows.Data_12,
                rows.Data_13,
                rows.Data_14,
                rows.Data_15,
                rows.Data_16,
                rows.Data_17,
                rows.Data_18,
                rows.Data_19,
                rows.Data_20,
                rows.Data_21,
                rows.Data_22,
                rows.Data_23,
                rows.Data_24,
                rows.Data_25,
                rows.Data_26,
                rows.Data_27,
                rows.Data_28,
                rows.Data_29,
                rows.Data_30,
                rows.Data_31
            };

            return rowsList;
        }
        public static DateTime LastEvent(this Row row)
        {
            DateTime? lastDateEvent = null;

            if (row.Entrada != null && row.SaidaAlmoco == null && row.EntradaAlmoco == null && row.Saida == null)
                lastDateEvent = row.Entrada.Value;
            else if (row.Entrada != null && row.SaidaAlmoco != null && row.EntradaAlmoco == null && row.Saida == null)
                lastDateEvent = row.SaidaAlmoco.Value;
            else if (row.Entrada != null && row.SaidaAlmoco != null && row.EntradaAlmoco != null && row.Saida == null)
                lastDateEvent = row.EntradaAlmoco.Value;
            else if (row.Entrada != null && row.SaidaAlmoco != null && row.EntradaAlmoco != null && row.Saida != null)
                lastDateEvent = row.Saida.Value;

            return lastDateEvent.Value;
        }
    }
}
