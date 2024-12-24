using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAggregator.Application.Queries
{
    public class GetWeatherNewsQuery
    {
        public string CityName { get; set; }
        public string CountryName { get; set; }
    }
}
