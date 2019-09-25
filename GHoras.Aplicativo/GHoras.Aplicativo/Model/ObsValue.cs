using Newtonsoft.Json;

namespace GHoras.Aplicativo.Model
{
    public class ObsValue
    {
        [JsonProperty(PropertyName = "date")]
        public string Data { get; set; }

        [JsonProperty(PropertyName = "obs")]
        public string Observacao { get; set; }
    }
}
