using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherAggregator.Domain.Entities.Weather
{
    public class WeatherCondition
    {
        [JsonProperty("main")]
        public string Main { get; set; }
    }
}
