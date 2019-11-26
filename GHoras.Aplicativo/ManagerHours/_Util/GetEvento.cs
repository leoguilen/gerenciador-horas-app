using ManagerHours.Enum;
using ManagerHours.Services;
using System;
using System.Threading.Tasks;

namespace ManagerHours._Util
{
    public static class GetEvento
    {
        public static Evento GetEventForTime(DateTime dataPto)
        {
            GetRow getRow = new GetRow();
            Evento? ev = null;
            string[] partsData = dataPto.ToShortDateString().Split('/');
            string horaPto = dataPto.ToShortTimeString();
            string[] partsHora = horaPto.Split(':');
            int hrs = int.Parse(partsHora[0]);
            int min = int.Parse(partsHora[1]);

            if (hrs >= 17)
                ev = Evento.saida;
            else if (hrs >= 8 && hrs <= 9)
                ev = Evento.entrada;
            else if ((hrs >= 11 && hrs <= 14)) {
                var rowResult = Task.FromResult(getRow.GetRowAsync($"{partsData[0]}/{partsData[1]}")).Result;

                if (rowResult.IsCompleted) {
                    var rowDetails = rowResult.Result;

                    if (rowDetails.SaidaAlmoco != null)
                        ev = Evento.entrada_almoco;

                    ev = Evento.saida_almoco;
                }
            }

            return ev.Value;
        }
    }
}
