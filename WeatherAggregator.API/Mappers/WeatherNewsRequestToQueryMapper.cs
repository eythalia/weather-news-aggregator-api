using WeatherAggregator.API.DTOs;
using WeatherAggregator.Application.Queries;

namespace WeatherAggregator.API.Mappers
{
    public static class WeatherNewsRequestToQueryMapper
    {
        public static GetWeatherNewsQuery Map (GetWeatherNewsRequest request) 
        {
            return new GetWeatherNewsQuery 
            { 
                CityName = request.CityName,
                CountryName = request.CountryName
            };
        }

    }
}
