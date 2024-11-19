using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAggregator.Application.Queries;
using WeatherAggregator.Domain.Entities;
using WeatherAggregator.Domain.Interfaces;
using WeatherAggregator.Shared.Results;

namespace WeatherAggregator.Application.Services
{
    public class WeatherService
    {
        private readonly IWeatherRepository _weatherRepository;

        public WeatherService(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        public async Task<Result<IEnumerable<Location>>> GetLocationsAsync(GetLocationsQuery query)
        {
            if (string.IsNullOrWhiteSpace(query.CityName))
                throw new ArgumentException("City name cannot be null or empty.");

            return await _weatherRepository.GetLocationsAsync(query.CityName);
        }
    }
}
