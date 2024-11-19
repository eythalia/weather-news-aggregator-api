using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAggregator.Domain.Entities;
using WeatherAggregator.Domain.Interfaces;
using WeatherAggregator.Domain.DTOs;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WeatherAggregator.Shared.Results;

namespace WeatherAggregator.Infrastructure.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public WeatherRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["OpenWeather:ApiKey"];
        }
        public async Task<Result<IEnumerable<Location>>> GetLocationsAsync(string cityName)
        {
            try
            {
                var endpoint = $"http://api.openweathermap.org/geo/1.0/direct?q={cityName}&limit=5&appid={_apiKey}";
                var response = await _httpClient.GetAsync(endpoint);

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                var locations = JsonConvert.DeserializeObject<List<LocationResponse>>(result);

                return Result<IEnumerable<Location>>.Success(locations.Select(loc => new Location
                {
                    Name = loc.Name,
                    Latitude = loc.Lat,
                    Longitude = loc.Lon,
                    Country = loc.Country
                }));
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Location>>.Failure($"Failed to fetch locations: {ex.Message}");
            }
        }
    }
}
