using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAggregator.Application.DTOs.Weather
{
    public class DailyTemperature
    {
        public double Day { get; set; }

        public double Max { get; set; }

        public double Min { get; set; }
        public double Night { get; set; }

        public double Evening { get; set; } //efara allo onoma

        public double Morning { get; set; } //efara allo onoma
    }
}
