using WeatherAggregator.API.DTOs;
using WeatherAggregator.Application.Queries;

namespace WeatherAggregator.API.Mappers
{
    public static class RequestToQueryMapper
    {
        public static GetLocationsQuery Map(GetLocationsRequest request) 
        { 
            return new GetLocationsQuery { CityName = request.CityName };
        }
    }
}
