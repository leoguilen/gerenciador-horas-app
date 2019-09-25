using System;
using Newtonsoft.Json;

namespace GHoras.Aplicativo.Model
{
    public class Rows
    {
        [JsonProperty(PropertyName = "mes")]
        public string Mes { get; set; }

        [JsonProperty(PropertyName = "data_01")]
        public Row Data_01 { get; set; }

        [JsonProperty(PropertyName = "data_02")]
        public Row Data_02 { get; set; }

        [JsonProperty(PropertyName = "data_03")]
        public Row Data_03 { get; set; }

        [JsonProperty(PropertyName = "data_04")]
        public Row Data_04 { get; set; }

        [JsonProperty(PropertyName = "data_05")]
        public Row Data_05 { get; set; }

        [JsonProperty(PropertyName = "data_06")]
        public Row Data_06 { get; set; }

        [JsonProperty(PropertyName = "data_07")]
        public Row Data_07 { get; set; }

        [JsonProperty(PropertyName = "data_08")]
        public Row Data_08 { get; set; }

        [JsonProperty(PropertyName = "data_09")]
        public Row Data_09 { get; set; }

        [JsonProperty(PropertyName = "data_10")]
        public Row Data_10 { get; set; }

        [JsonProperty(PropertyName = "data_11")]
        public Row Data_11 { get; set; }

        [JsonProperty(PropertyName = "data_12")]
        public Row Data_12 { get; set; }

        [JsonProperty(PropertyName = "data_13")]
        public Row Data_13 { get; set; }

        [JsonProperty(PropertyName = "data_14")]
        public Row Data_14 { get; set; }

        [JsonProperty(PropertyName = "data_15")]
        public Row Data_15 { get; set; }

        [JsonProperty(PropertyName = "data_16")]
        public Row Data_17 { get; set; }

        [JsonProperty(PropertyName = "data_18")]
        public Row Data_18 { get; set; }

        [JsonProperty(PropertyName = "data_19")]
        public Row Data_19 { get; set; }

        [JsonProperty(PropertyName = "data_20")]
        public Row Data_21 { get; set; }

        [JsonProperty(PropertyName = "data_22")]
        public Row Data_22 { get; set; }

        [JsonProperty(PropertyName = "data_23")]
        public Row Data_23 { get; set; }

        [JsonProperty(PropertyName = "data_24")]
        public Row Data_24 { get; set; }

        [JsonProperty(PropertyName = "data_25")]
        public Row Data_25 { get; set; }

        [JsonProperty(PropertyName = "data_26")]
        public Row Data_26 { get; set; }

        [JsonProperty(PropertyName = "data_27")]
        public Row Data_27 { get; set; }

        [JsonProperty(PropertyName = "data_28")]
        public Row Data_28 { get; set; }

        [JsonProperty(PropertyName = "data_29")]
        public Row Data_29 { get; set; }

        [JsonProperty(PropertyName = "data_30")]
        public Row Data_30 { get; set; }

        [JsonProperty(PropertyName = "data_31")]
        public Row Data_31 { get; set; }

        [JsonProperty(PropertyName = "total_horas_extras")]
        public DateTime TotalHorasExtras { get; set; }
    }
}
