using Newtonsoft.Json;

namespace GHoras.Aplicativo.Model
{
    public class DateValue
    {
        [JsonProperty(PropertyName = "date")]
        public string Data { get; set; }

        [JsonProperty(PropertyName = "why")]
        public string Evento { get; set; }

        [JsonProperty(PropertyName = "time")]
        public string Hora { get; set; }

        [JsonProperty(PropertyName = "who")]
        public string Autor { get; set; }
    }
}
