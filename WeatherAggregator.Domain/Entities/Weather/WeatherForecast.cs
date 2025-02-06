using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherAggregator.Domain.Entities.Weather
{
    public class WeatherForecast
    {
        [JsonProperty("timezone_offset")]
        public int TimezoneOffset { get; set; }

        [JsonProperty("timezone")]
        public string Timezone {  get; set; }

        [JsonProperty("current")]
        public CurrentWeatherInfo Current { get; set; }

        [JsonProperty("hourly")]
        public List<HourlyForecast> Hourly { get; set; }

        [JsonProperty("daily")]
        public List<DailyForecast> Daily { get; set; }

        [JsonProperty("alerts")]
        public List<WeatherAlertInfo> Alerts { get; set; }
    }
}
