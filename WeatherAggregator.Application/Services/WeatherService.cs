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
            
            try
            {
                var locations = await _weatherRepository.GetLocationsAsync(query.CityName);

                if (!locations.Data.Any())  //this is about business logic so the controll remains on application layer
                {
                    return Result<IEnumerable<Location>>.Failure("No matching locations found by the Weather API.", 404);
                }

                return locations;
            }
            catch (Exception ex)
            {
                // Return failure result on any exception
                return Result<IEnumerable<Location>>.Failure($"Error fetching locations: {ex.Message}",500);
            }
        }
    }
}
