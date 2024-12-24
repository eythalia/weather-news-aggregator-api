using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAggregator.Application.DTOs.Weather
{
    public class CurrentWeather
    {
        public DateTime? Date { get; set; }

        public double Temperature {  get; set; }

        public double FeelsLike { get; set; }

        public int Humidity { get; set; }

        public double WindSpped { get; set; }

        public string WeatherCondition { get; set; }

    }
}
