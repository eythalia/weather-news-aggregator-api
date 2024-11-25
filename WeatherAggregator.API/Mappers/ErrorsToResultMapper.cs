using WeatherAggregator.Shared.Results;
using FluentValidation.Results;

namespace WeatherAggregator.API.Mappers
{
    public static class ErrorsToResultMapper
    {
        public static Result<IEnumerable<string>> Map(ValidationResult validationResult) 
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            var result = Result<IEnumerable<string>>.Failure(string.Join(", ", errors), 400);
            return result;
        }

    }
}
