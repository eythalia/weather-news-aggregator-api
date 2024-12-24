using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAggregator.Domain.Entities.Location;
using WeatherAggregator.Domain.Entities.Weather;
using WeatherAggregator.Shared.Results;

namespace WeatherAggregator.Application.Interfaces
{
    public interface IWeatherRepository
    {
        Task<Result<IEnumerable<Location>>> GetLocationsAsync(string cityName);

        Task<Result<WeatherForecast>> GetWeatherForecastAsync(double lat, double lon);
    }
}
