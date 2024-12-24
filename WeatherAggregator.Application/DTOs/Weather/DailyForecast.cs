using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAggregator.Domain.Entities.Weather;

namespace WeatherAggregator.Application.DTOs.Weather
{
    public class DailyForecast
    {
        public DateTime Date { get; set; } //efara allos typos allo onoma

        public string Summary { get; set; }

        public DailyTemperature Temperatures { get; set; } //efara allo name

        public int Humidity { get; set; }

        public double WindSpeed { get; set; }

        public string WeatherConditionDescription {  get; set; }
    }
}
