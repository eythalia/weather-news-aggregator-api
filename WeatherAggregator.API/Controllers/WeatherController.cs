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

            //efara edw tha mporousa na exw extra mapping alla voithaei gia na min kanw tosa polla na settarw eks arxis ta responses objects tou application ara kanw tin paradoxi me ta responses tou application opou xreiazetai gia na xw poio katharo controller k epeidi kanw teleytaio mappinh edw
            var getLocationsResponse = ResultToResponseMapper.Map(locationsResult);
            return getLocationsResponse;
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

            
            var locationsResult = await _weatherService.GetWeatherNewsAsync(query);

            //efara apo edw kai katw
            if (!locationsResult.IsSuccess)
            {
                return ResultToResponseMapper.Map(locationsResult);
            }

            //logika edw xreiazetai mapping efara
            return Ok(locationsResult);//efara na to ftiaksw meta
        }
    }
}
