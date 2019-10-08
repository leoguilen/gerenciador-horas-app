using Newtonsoft.Json;

namespace ManagerHours.Model
{
    public class ObsValue
    {
        [JsonProperty(PropertyName = "date")]
        public string Data { get; set; }

        [JsonProperty(PropertyName = "obs")]
        public string Observacao { get; set; }
    }
}
