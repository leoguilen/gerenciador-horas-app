using System;
using Newtonsoft.Json;

namespace GHoras.Aplicativo.Model
{
    public class Row
    {
        [JsonProperty(PropertyName = "data")]
        public string Data { get; set; }

        [JsonProperty(PropertyName = "entrada")]
        public DateTime? Entrada { get; set; }

        [JsonProperty(PropertyName = "saida_almoco")]
        public DateTime? SaidaAlmoco { get; set; }

        [JsonProperty(PropertyName = "entrada_almoco")]
        public DateTime? EntradaAlmoco { get; set; }

        [JsonProperty(PropertyName = "saida")]
        public DateTime? Saida { get; set; }

        [JsonProperty(PropertyName = "obs")]
        public string Observacao { get; set; }

        [JsonProperty(PropertyName = "extra")]
        public DateTime? HorasExtras { get; set; }
    }
}
