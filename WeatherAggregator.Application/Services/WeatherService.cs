using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAggregator.Application.Queries;
using WeatherAggregator.Domain.Entities;
using WeatherAggregator.Application.Interfaces;
using WeatherAggregator.Shared.Results;
using WeatherAggregator.Application.Mappers;
using WeatherAggregator.Application.Helpers;
using WeatherAggregator.Application.DTOs.Location;
using WeatherAggregator.Application.DTOs.WeatherNews;


namespace WeatherAggregator.Application.Services
{
    public class WeatherService
    {
        private readonly IWeatherRepository _weatherRepository;
        

        public WeatherService(IWeatherRepository weatherRepository, INewsRepository newsRepository)
        {
            _weatherRepository = weatherRepository;
        }

        public async Task<Result<IEnumerable<LocationResponse>>> GetLocationsAsync(GetLocationsQuery query)
        {
            
            try
            {
                var locations = await _weatherRepository.GetLocationsAsync(query.CityName);

                if (!locations.IsSuccess) //this is about to propagate inner error message
                    return Result<IEnumerable<LocationResponse>>.Failure(locations.ErrorMessage, locations.StatusCode);
                

                if (!locations.Data.Any())  //this is about business logic so the controll remains on application layer
                    return Result<IEnumerable<LocationResponse>>.Failure("No matching locations found by the Weather API.", 404);
                

                var locationsResponse = LocationToLocationResponseMapper.ToLocationResponseList(locations.Data);

                return Result<IEnumerable<LocationResponse>>.Success(locationsResponse, 200);
            }
            catch (Exception ex)
            {
                // Return failure result on any exception
                return Result<IEnumerable<LocationResponse>>.Failure($"Error fetching locations: {ex.Message}",500);
            }
        }

        public async Task<Result<WeatherNewsResponse>> GetWeatherNewsAsync(GetWeatherNewsQuery query)  //efara to swsto einai WeatherNewsResponse
        {
            try
            {
                var locations = await _weatherRepository.GetLocationsAsync(query.CityName);

                if (!locations.IsSuccess) // this is to propagate error msg and status code of infraestructure
                    return Result<WeatherNewsResponse>.Failure(locations.ErrorMessage, locations.StatusCode);
                

                if (!locations.Data.Any())  //this is about business logic so the control remains on application layer
                    return Result<WeatherNewsResponse>.Failure("No matching locations found by the Weather API.", 404); 
                

                var locationsResponse = LocationToLocationResponseMapper.ToLocationResponseList(locations.Data);


                string countryPrefix = LocationHelper.ExtractCountryPrefix(query.CountryName);
                var singleLocationObject = await LocationHelper.FilterLocationsByCountryPrefix(locationsResponse, countryPrefix);

                var weatherForecast = await _weatherRepository.GetWeatherForecastAsync(singleLocationObject.Latitude, singleLocationObject.Longitude);

                if (!weatherForecast.IsSuccess)
                    return Result<WeatherNewsResponse>.Failure(weatherForecast.ErrorMessage, weatherForecast.StatusCode);

                if (weatherForecast.Data == null)
                    return Result<WeatherNewsResponse>.Failure("No weather data found by the Weather API.", 404);
                //efara ta modela prwta kai meta to mapping edw
                var weatherForecastResponse = WeatherForecastToWeatherForecastResponseMapper.MapToResponse(weatherForecast.Data);

                return Result<WeatherNewsResponse>.Success(weatherForecastResponse,200);
                
            }
            catch (Exception ex)
            {
                // Return failure result on any exception
                return Result<WeatherNewsResponse>.Failure($"Error fetching locations: {ex.Message}", 500);
            }

        } 
    }
}
