using Microsoft.AspNetCore.Mvc;
using WeatherAggregator.API.DTOs;
using WeatherAggregator.Shared.Results;

namespace WeatherAggregator.API.Mappers
{
    public static class ResultToResponseMapper
    {
        /// <summary>
        /// Converts a Result<T> into an appropriate IActionResult.
        /// </summary>
        public static IActionResult Map<T>(Result<T> result)
        {
            var responseBody = new GetLocationsResponse<T>
            {
                Data = result.Data,
                ErrorMessage = result.ErrorMessage,
                IsSuccess = result.IsSuccess
            };

            return new ObjectResult(responseBody)
            {
                StatusCode = result.StatusCode // Use the status code from Result<T>
            };
        }
    }
}
