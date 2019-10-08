using Newtonsoft.Json;

namespace ManagerHours.Model
{
    public class RowInfo
    {
        [JsonProperty(PropertyName = "row")]
        public int IndexRow { get; set; }

        [JsonProperty(PropertyName = "column")]
        public int IndexCol { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string BatchID { get; set; }

    }
}
