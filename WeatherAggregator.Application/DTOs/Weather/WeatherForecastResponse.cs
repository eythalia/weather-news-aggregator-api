using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAggregator.Application.DTOs.Weather
{
    public class WeatherForecastResponse
    {
        public CurrentWeather Current { get; set; }

        public List<HourlyForecast> Hourly { get; set; }

        public List<DailyForecast> Daily { get; set; }

        public List<Alert> Alerts { get; set; }
    }
}
