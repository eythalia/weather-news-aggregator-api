using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WeatherAggregator.Domain.Entities;
using WeatherAggregator.Application.Queries;
using WeatherAggregator.Application.Services;
using WeatherAggregator.Shared.Results;
using WeatherAggregator.API.Mappers;
using WeatherAggregator.API.DTOs;
using WeatherAggregator.Application.Helpers;

namespace WeatherAggregator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;
        private readonly IValidator<GetLocationsRequest> _validatorLocations;
        private readonly IValidator<GetWeatherNewsRequest> _validatorWeatherNews;

        public WeatherController(WeatherService weatherService, IValidator<GetLocationsRequest> validatorLocations, IValidator<GetWeatherNewsRequest> validatorWeatherNews)
        {
            _weatherService = weatherService;
            _validatorLocations = validatorLocations;
            _validatorWeatherNews = validatorWeatherNews;
        }

        [HttpGet("locations")]
        public async Task<IActionResult> GetLocations([FromQuery] GetLocationsRequest request)
        {
            var validationResult = await _validatorLocations.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                // Return validation errors using the Result pattern
                var result = ErrorsToResultMapper.Map(validationResult);
                return ResultToResponseMapper.Map(result);
            }

            // Map the DTO to the query object
            var query = RequestToQueryMapper.Map(request);

            var locationsResult = await _weatherService.GetLocationsAsync(query);
            
            return ResultToResponseMapper.Map(locationsResult);
        }

        [HttpGet("weather-news")]
        public async Task<IActionResult> GetWeatherNews([FromQuery] GetWeatherNewsRequest request) 
        {
            var validationResult = await _validatorWeatherNews.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var result = ErrorsToResultMapper.Map(validationResult);
                return ResultToResponseMapper.Map(result);
            }

            // Map the DTO to the query object
            var query = WeatherNewsRequestToQueryMapper.Map(request);

            var locationsResult = await _weatherService.GetLocationsAsync(query);
            if (!locationsResult.IsSuccess)
            {
                return ResultToResponseMapper.Map(locationsResult);
            }

            string countryPrefix = LocationHelper.ExtractCountryPrefix(request.CountryName);
            var singleLocation = await LocationHelper.FilterLocationsByCountryPrefix(locationsResult, countryPrefix);

            if (!singleLocation.IsSuccess) 
            {
                return ResultToResponseMapper.Map(singleLocation);
            }
            

            //logika edw xreiazetai mapping efara
            return Ok(singleLocation);
        }
    }
}
