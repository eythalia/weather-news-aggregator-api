using FluentValidation;
using WeatherAggregator.API.DTOs;

namespace WeatherAggregator.API.Validators
{
    public class GetWeatherNewsRequestValidator : AbstractValidator<GetWeatherNewsRequest>
    {
        public GetWeatherNewsRequestValidator() 
        {
            RuleFor(x => x.CityName)
           .NotEmpty().WithMessage("City name is required.")
           .MaximumLength(100).WithMessage("City name cannot exceed 100 characters.");

            RuleFor(x => x.CountryName)
           .NotEmpty().WithMessage("Country name is required.")
           .MaximumLength(100).WithMessage("Country name cannot exceed 100 characters.")
           .MinimumLength(2).WithMessage("Country name cannot be less than 2 characters.");
        }
    }
}
