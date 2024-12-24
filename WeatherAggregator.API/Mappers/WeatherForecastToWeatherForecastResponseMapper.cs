using System;
using System.Collections.Generic;
using System.Linq;
using WeatherAggregator.Application.DTOs.Weather;
using WeatherAggregator.Domain.Entities.Weather;

namespace WeatherAggregator.Application.Mappers
{
    public static class WeatherForecastToWeatherForecastResponseMapper
    {
        public static WeatherForecastResponse MapToResponse(WeatherAggregator.Domain.Entities.Weather.WeatherForecast forecast)
        {
            if (forecast == null)
                return null;

            return new WeatherForecastResponse
            {
                Current = MapToCurrentWeather(forecast.Current),
                Hourly = forecast.Hourly?.Select(MapToHourlyForecast).ToList(),
                Daily = forecast.Daily?.Select(MapToDailyForecast).ToList(),
                Alerts = forecast.Alerts?.Select(MapToAlert).ToList()
            };
        }

        private static CurrentWeather MapToCurrentWeather(CurrentWeatherInfo current)
        {
            if (current == null)
                return null;

            return new CurrentWeather
            {
                Date = UnixTimeStampToDateTime(current.Dt),
                Temperature = current.Temp,
                FeelsLike = current.FeelsLike,
                Humidity = current.Humidity,
                WindSpped = current.WindSpeed,
                WeatherCondition = current.WeatherConditionDescription
            };
        }

        private static WeatherAggregator.Application.DTOs.Weather.HourlyForecast MapToHourlyForecast(WeatherAggregator.Domain.Entities.Weather.HourlyForecast domainHourly)
        {
            if (domainHourly == null)
                return null;

            return new WeatherAggregator.Application.DTOs.Weather.HourlyForecast
            {
                Date = UnixTimeStampToDateTime(domainHourly.Dt),
                Temperature = domainHourly.Temp,
                FeelsLike = domainHourly.FeelsLike,
                Humidity = domainHourly.Humidity,
                WindSpeed = domainHourly.WindSpeed,
                WeatherConditionDescription = domainHourly.WeatherConditionDescription
            };
        }

        private static WeatherAggregator.Application.DTOs.Weather.DailyForecast MapToDailyForecast(WeatherAggregator.Domain.Entities.Weather.DailyForecast domainDaily)
        {
            if (domainDaily == null)
                return null;

            return new WeatherAggregator.Application.DTOs.Weather.DailyForecast
            {
                Date = UnixTimeStampToDateTime(domainDaily.Dt),
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

        private static Alert MapToAlert(WeatherAlertInfo alert)
        {
            if (alert == null)
                return null;

            return new Alert
            {
                EventName = alert.Event,
                Start = UnixTimeStampToDateTime(alert.Start),
                End = UnixTimeStampToDateTime(alert.End),
                Description = alert.Description
            };
        }

        private static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp).UtcDateTime;
        }
    }
}
