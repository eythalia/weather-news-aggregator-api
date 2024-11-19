using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAggregator.Domain.Entities;
using WeatherAggregator.Shared.Results;

namespace WeatherAggregator.Domain.Interfaces
{
    public interface IWeatherRepository
    {
        Task<Result<IEnumerable<Location>>> GetLocationsAsync(string cityName);
    }
}
