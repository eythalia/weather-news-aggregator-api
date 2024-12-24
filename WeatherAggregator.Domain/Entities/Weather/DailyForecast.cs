using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherAggregator.Domain.Entities.Weather
{
    public class DailyForecast
    {
        [JsonProperty("dt")]
        public long Dt { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("temp")]
        public DailyTemperature Temp { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }

        [JsonProperty("wind_speed")]
        public double WindSpeed { get; set; }

        // Backing field for weather
        [JsonProperty("weather")]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private List<WeatherCondition> Weather { get; set; }

        // Public property exposing the first "main" value
        public string WeatherConditionDescription => Weather?.FirstOrDefault()?.Main;
    }
}
