using System.Collections.ObjectModel;

namespace ManagerHours.Dtos
{
    public class HistoricoPonto : ObservableCollection<PontoDto>
    {
        public string Mes { get; set; }
    }
}
