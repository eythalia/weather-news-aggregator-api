using Microsoft.AspNetCore.Mvc;

namespace WeatherAggregator.API.DTOs
{
    public class GetWeatherNewsRequest
    {
        [FromQuery(Name = "cityName")]
        public string CityName { get; set; }

        [FromQuery(Name = "countryName")]
        public string CountryName { get; set; }
    }
}
