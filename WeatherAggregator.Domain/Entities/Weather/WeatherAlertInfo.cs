using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherAggregator.Domain.Entities.Weather
{
    public class WeatherAlertInfo
    {
        [JsonProperty("event")]
        public string Event { get; set; }

        [JsonProperty("start")]
        public long Start { get; set; }

        [JsonProperty("end")]
        public long End { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
