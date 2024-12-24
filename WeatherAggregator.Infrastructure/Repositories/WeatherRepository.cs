using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAggregator.Application.Interfaces;
using WeatherAggregator.Application.DTOs;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WeatherAggregator.Shared.Results;
using WeatherAggregator.Domain.Entities.Location;
using WeatherAggregator.Domain.Entities.Weather;

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

                if (!response.IsSuccessStatusCode) 
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return Result<IEnumerable<Location>>.Failure($"External API Error: {errorMessage}", (int)response.StatusCode);
                }

                var result = await response.Content.ReadAsStringAsync();
                var locations = JsonConvert.DeserializeObject<List<Location>>(result); //efara edw giati location kai oxi location response

                //return Result<IEnumerable<Location>>.Success(locations.Select(loc => new Location 
                //{
                //    Name = loc.Name,
                //    Latitude = loc.Lat,
                //    Longitude = loc.Lon,
                //    Country = loc.Country
                //}),
                //(int)response.StatusCode);

                return Result<IEnumerable<Location>>.Success(locations, (int)response.StatusCode);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Location>>.Failure($"Failed to fetch locations: {ex.Message}", 500); 
            }
        }

        public async Task<Result<WeatherForecast>> GetWeatherForecastAsync(double lat, double lon) 
        {
            try
            {
                var endpoint = $"https://api.openweathermap.org/data/3.0/onecall?lat={lat}&lon={lon}&exclude=minutely&appid={_apiKey}";
                var response = await _httpClient.GetAsync(endpoint);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return Result<WeatherForecast>.Failure($"External API Error: {errorMessage}", (int)response.StatusCode);
                }

                var result = await response.Content.ReadAsStringAsync();
                var weatherForecast = JsonConvert.DeserializeObject<WeatherForecast>(result);
                
                return Result<WeatherForecast>.Success(weatherForecast, (int)response.StatusCode);
            }
            catch (Exception ex)
            {
                return Result<WeatherForecast>.Failure($"Failed to fetch weather forecast: {ex.Message}", 500);
            }
            
        }
    }
}
