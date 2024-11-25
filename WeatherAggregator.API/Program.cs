using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System;
using WeatherAggregator.API.Middlewares;
using WeatherAggregator.API.Validators;
using WeatherAggregator.API.DTOs;
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

//builder.Services.AddValidatorsFromAssemblyContaining<GetLocationsQueryValidator>();
builder.Services.AddScoped<IValidator<GetLocationsRequest>, GetLocationsRequestValidator>();

builder.Services.AddScoped<IValidator<GetWeatherNewsRequest>, GetWeatherNewsRequestValidator>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});//efara edw apnergopoiw to default validation tou asp net


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
