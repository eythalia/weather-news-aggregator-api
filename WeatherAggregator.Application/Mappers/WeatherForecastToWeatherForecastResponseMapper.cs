using System;
using System.Collections.Generic;
using System.Linq;
using WeatherAggregator.Application.DTOs.Weather;
using WeatherAggregator.Domain.Entities.Weather;

namespace WeatherAggregator.API.Mappers
{
    public static class WeatherForecastToWeatherForecastResponseMapper
    {
        public static WeatherForecastResponse MapToResponse(WeatherForecast forecast)
        {
            if (forecast == null)
                return null;

            return new WeatherForecastResponse
            {
                Current = MapToCurrentWeather(forecast.Current, forecast.TimezoneOffset),
                Hourly = forecast.Hourly?.Select(h => MapToHourlyForecast(h, forecast.TimezoneOffset)).ToList(),
                Daily = forecast.Daily?.Select(d => MapToDailyForecast(d, forecast.TimezoneOffset)).ToList(),
                Alerts = forecast.Alerts?.Select(a => MapToAlert(a, forecast.TimezoneOffset)).ToList()
            };
        }

        private static CurrentWeather MapToCurrentWeather(CurrentWeatherInfo current, int timezoneOffset)
        {
            if (current == null)
                return null;

            return new CurrentWeather
            {
                Date = UnixTimeStampToDateTime(current.Dt, timezoneOffset),
                Temperature = current.Temp,
                FeelsLike = current.FeelsLike,
                Humidity = current.Humidity,
                WindSpped = current.WindSpeed,
                WeatherCondition = current.WeatherConditionDescription
            };
        
        }

        private static WeatherAggregator.Application.DTOs.Weather.HourlyForecast MapToHourlyForecast(WeatherAggregator.Domain.Entities.Weather.HourlyForecast domainHourly, int timezoneOffset)
        {
            if (domainHourly == null)
                return null;

            return new WeatherAggregator.Application.DTOs.Weather.HourlyForecast
            {
                Date = UnixTimeStampToDateTime(domainHourly.Dt, timezoneOffset),
                Temperature = domainHourly.Temp,
                FeelsLike = domainHourly.FeelsLike,
                Humidity = domainHourly.Humidity,
                WindSpeed = domainHourly.WindSpeed,
                WeatherConditionDescription = domainHourly.WeatherConditionDescription
            };
        }

        private static WeatherAggregator.Application.DTOs.Weather.DailyForecast MapToDailyForecast(WeatherAggregator.Domain.Entities.Weather.DailyForecast domainDaily, int timezoneOffset)
        {
            if (domainDaily == null)
                return null;

            return new WeatherAggregator.Application.DTOs.Weather.DailyForecast
            {
                Date = UnixTimeStampToDateTime(domainDaily.Dt, timezoneOffset),
                Summary = domainDaily.Summary,
                Temperatures = MapToDailyTemperature(domainDaily.Temp),
                Humidity = domainDaily.Humidity,
                WindSpeed = domainDaily.WindSpeed,
                WeatherConditionDescription = domainDaily.WeatherConditionDescription
            };
        }

        private static WeatherAggregator.Application.DTOs.Weather.DailyTemperature MapToDailyTemperature(WeatherAggregator.Domain.Entities.Weather.DailyTemperature temp)
        {
            if (temp == null)
                return null;

            return new WeatherAggregator.Application.DTOs.Weather.DailyTemperature
            {
                Day = temp.Day,
                Min = temp.Min,
                Max = temp.Max,
                Night = temp.Night,
                Evening = temp.Eve,
                Morning = temp.Morn
            };
        }

        private static Alert MapToAlert(WeatherAlertInfo alert, int timezoneOffset)
        {
            if (alert == null)
                return null;

            return new Alert
            {
                EventName = alert.Event,
                Start = UnixTimeStampToDateTime(alert.Start, timezoneOffset),
                End = UnixTimeStampToDateTime(alert.End, timezoneOffset),
                Description = alert.Description
            };
        }

        private static DateTime UnixTimeStampToDateTime(long unixTimeStamp, int timezoneOffset)
        {
            return DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp).ToOffset(TimeSpan.FromSeconds(timezoneOffset)).DateTime;
        }
    }
}
