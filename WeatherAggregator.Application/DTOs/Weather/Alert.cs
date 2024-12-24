using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAggregator.Application.DTOs.Weather
{
    public class Alert
    {
        public string EventName { get; set; } //efara allo name

        public DateTime Start { get; set; } //efara allo type

        public DateTime End { get; set; } //efara allo type

        public string Description { get; set; }
    }
}
