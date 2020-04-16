using System;
using System.Threading.Tasks;
using ManagerHours.Dependencies;
using ManagerHours.Services;

namespace ManagerHours.Enum
{
    public static class EventoExtension
    {
        public static Evento GetEventForTime(this Evento evento, DateTime dateCurrent)
        {   
            // Das 8 as 10 = Entrada
            // Das 11 as 14 = Saida Almoço (Se não tiver ponto de saída ainda)
            // Das 12 as 15 = Entrada Almoço (Se ja houver ponto de saída)
            // Das maior que 17 = Saída

            GetRow getRow = new GetRow(RowServiceDependencies.Inject());
            Evento? ev = null;

            var value = Task.FromResult(getRow.GetRowAsync(dateCurrent.ToString("dd/MM")).GetAwaiter().GetResult());
            var rowValue = value.Result;

            int hoursCurrent = dateCurrent.Hour;

            if (hoursCurrent >= 8 && hoursCurrent <= 10) {
                ev = Evento.entrada;
            } else if (hoursCurrent >= 11 && hoursCurrent <= 14 && rowValue.SaidaAlmoco == null) {
                ev = Evento.saida_almoco;
            } else if (hoursCurrent >= 12 && hoursCurrent <= 15 && rowValue.SaidaAlmoco != null) {
                ev = Evento.entrada_almoco;
            } else if (hoursCurrent >= 17) {
                ev = Evento.saida;
            }

            return ev.Value;
        }

        public static async Task<string> GetEventForTime(this DateTime dataPto)
        {
            GetRow getRow = new GetRow(RowServiceDependencies.Inject());
            string ev = string.Empty;
            string[] partsData = dataPto.ToShortDateString().Split('/');
            string horaPto = dataPto.ToShortTimeString();
            string[] partsHora = horaPto.Split(':');
            string dataParam = $"{partsData[0]}/{partsData[1]}";
            int hrs = int.Parse(partsHora[0]);

            if (hrs >= 11 && hrs <= 15) {
                var rowResult = Task.FromResult(await getRow.GetRowAsync(dataParam)).Result;

                if (rowResult.SaidaAlmoco != null)
                    ev = Evento.entrada_almoco.ToString();
                else
                    ev = Evento.saida_almoco.ToString();
            }
            else if (hrs >= 8 && hrs <= 9) {
                ev = Evento.entrada.ToString();
            }
            else {
                ev = Evento.saida.ToString();
            }

            return await Task.FromResult(ev);
        }
    }
}
