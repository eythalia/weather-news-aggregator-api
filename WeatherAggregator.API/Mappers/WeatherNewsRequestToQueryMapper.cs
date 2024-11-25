using WeatherAggregator.API.DTOs;
using WeatherAggregator.Application.Queries;

namespace WeatherAggregator.API.Mappers
{
    public static class WeatherNewsRequestToQueryMapper
    {
        public static GetLocationsQuery Map (GetWeatherNewsRequest request) 
        {
            return new GetLocationsQuery { CityName = request.CityName };
        
        }

    }
}
