using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Skolplattformen.Elevapp.InfomentorStockholmApi.Models
{
    public class TimeTableItem
    {
        [JsonPropertyName("start")]
        public DateTime Start { get; set; }

        [JsonPropertyName("end")]
        public DateTime End { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("startTime")]
        public string StartTime { get; set; }

        [JsonPropertyName("endTime")]
        public string EndTime { get; set; }

        [JsonPropertyName("notes")]
        public TimeTableItemNotes Notes { get; set; }

        [JsonPropertyName("allDay")]
        public bool AllDay { get; set; }

        [JsonPropertyName("establishmentName")]
        public object EstablishmentName { get; set; }

        [JsonPropertyName("details")]
        public string Details { get; set; }
    }

}
