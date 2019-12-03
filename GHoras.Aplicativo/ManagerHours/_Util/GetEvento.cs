using ManagerHours.Enum;
using ManagerHours.Services;
using System;
using System.Threading.Tasks;

namespace ManagerHours._Util
{
    public class GetEvento
    {
        public async Task<string> GetEventForTime(DateTime dataPto)
        {
            GetRow getRow = new GetRow();
            Evento? ev = null;
            string[] partsData = dataPto.ToShortDateString().Split('/');
            string horaPto = dataPto.ToShortTimeString();
            string[] partsHora = horaPto.Split(':');
            string dataParam = $"{partsData[0]}/{partsData[1]}";
            int hrs = int.Parse(partsHora[0]);

            if (hrs >= 17)
                ev = Evento.saida;
            else if (hrs >= 8 && hrs <= 9)
                ev = Evento.entrada;
            else {
                var rowResult = await Task.FromResult(await getRow.GetRowAsync(dataParam));

                if (rowResult.SaidaAlmoco != null)
                    ev = Evento.entrada_almoco;
                else
                    ev = Evento.saida_almoco;
            }

            return ev.Value.ToString();
        }
    }
}
