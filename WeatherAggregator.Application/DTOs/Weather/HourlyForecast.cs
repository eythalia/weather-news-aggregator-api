using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAggregator.Application.DTOs.Weather
{
    public class HourlyForecast
    {
        public DateTime? Date { get; set; } //efara allos typos allo onoma

        public double? Temperature { get; set; }

        public double? FeelsLike { get; set; }

        public int Humidity { get; set; }  

        public double WindSpeed { get; set; }

        public string WeatherConditionDescription { get; set; }

    }
}
