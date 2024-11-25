using FluentValidation;
using WeatherAggregator.Application.Queries;
using WeatherAggregator.API.DTOs;

namespace WeatherAggregator.API.Validators
{
    public class GetLocationsRequestValidator : AbstractValidator<GetLocationsRequest>
    {
        public GetLocationsRequestValidator()
        {
            RuleFor(x => x.CityName)
            .NotEmpty().WithMessage("City name is required.")
            .MaximumLength(100).WithMessage("City name cannot exceed 100 characters.");
        }
    }
}
