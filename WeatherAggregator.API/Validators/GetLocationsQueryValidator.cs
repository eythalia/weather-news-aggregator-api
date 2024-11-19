using FluentValidation;
using WeatherAggregator.Application.Queries;

namespace WeatherAggregator.API.Validators
{
    public class GetLocationsQueryValidator : AbstractValidator<GetLocationsQuery>
    {
        public GetLocationsQueryValidator()
        {
            RuleFor(x => x.CityName)
                .NotEmpty().WithMessage("City name is required.")
                .MaximumLength(100).WithMessage("City name cannot exceed 100 characters.");
        }
    }
}
