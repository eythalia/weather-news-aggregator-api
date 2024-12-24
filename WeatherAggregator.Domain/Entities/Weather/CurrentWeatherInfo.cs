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
    public class CurrentWeatherInfo
    {
        [JsonProperty("dt")]
        public long Dt { get; set; }

        [JsonProperty("temp")]
        public double Temp { get; set; }

        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }

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
