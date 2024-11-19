using FluentValidation;
using FluentValidation.AspNetCore;
using WeatherAggregator.API.Middlewares;
using WeatherAggregator.API.Validators;
using WeatherAggregator.Application.Services;
using WeatherAggregator.Domain.Interfaces;
using WeatherAggregator.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add services
builder.Services.AddHttpClient<IWeatherRepository, WeatherRepository>(client =>
{
    client.BaseAddress = new Uri("http://api.openweathermap.org/");
});


builder.Services.AddScoped<IWeatherRepository, WeatherRepository>();
builder.Services.AddScoped<WeatherService>();

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<GetLocationsQueryValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
