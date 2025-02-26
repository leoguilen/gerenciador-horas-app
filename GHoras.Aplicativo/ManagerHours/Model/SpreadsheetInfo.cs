﻿using Newtonsoft.Json;

namespace ManagerHours.Model
{
    public class SpreadsheetInfo
    {
        [JsonProperty(PropertyName = "Title")]
        public string Titulo { get; set; }

        [JsonProperty(PropertyName = "RowsCount")]
        public int QuantidadeDeLinhas { get; set; }

        [JsonProperty(PropertyName = "ColumnsCount")]
        public int QuantidadeDeColunas { get; set; }
    }
}
