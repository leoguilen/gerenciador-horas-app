using Newtonsoft.Json;
using System;

namespace ManagerHours.Model
{
    public class SpreadsheetUpdateInfo
    {
        [JsonProperty(PropertyName = "LastUpdate")]
        public DateTime UltimaAtualizacao { get; set; }

        [JsonProperty(PropertyName = "AuthorName")]
        public string NomeAutor { get; set; }

        [JsonProperty(PropertyName = "AuthorEmail")]
        public string EmailAutor { get; set; }
    }
}
