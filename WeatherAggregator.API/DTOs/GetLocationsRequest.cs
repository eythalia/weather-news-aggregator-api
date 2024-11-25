using Microsoft.AspNetCore.Mvc;

namespace WeatherAggregator.API.DTOs
{
    public class GetLocationsRequest
    {
        [FromQuery(Name = "cityName")]
        public string CityName { get; set; }
    }
}
