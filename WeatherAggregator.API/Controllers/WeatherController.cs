using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherAggregator.Application.Queries;
using WeatherAggregator.Application.Services;

namespace WeatherAggregator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("locations")]
        public async Task<IActionResult> GetLocations([FromQuery] string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName))
                return BadRequest("City name is required.");

            var query = new GetLocationsQuery { CityName = cityName };
            var locations = await _weatherService.GetLocationsAsync(query);
            return Ok(locations);
        }
    }
}
