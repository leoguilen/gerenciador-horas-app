namespace ManagerHours.Dtos
{
    public class PontoDto
    {
        public string Data { get; set; }
        public string Saida { get; set; }
        public bool IndicVerde { get; set; }
        public bool IndicAzul { get; set; }
        public string Status { get; set; }
        public string CorStatus { get; set; }

        public PontoDto()
        {

        }
    }
}
