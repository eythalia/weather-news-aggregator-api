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
                Current = MapToCurrentWeather(forecast.Current, forecast.TimezoneOffset),
                Hourly = forecast.Hourly?.Select(h => MapToHourlyForecast(h, forecast.TimezoneOffset)).ToList(),
                Daily = forecast.Daily?.Select(d => MapToDailyForecast(d, forecast.TimezoneOffset)).ToList(),
                Alerts = forecast.Alerts?.Select(a => MapToAlert(a, forecast.TimezoneOffset)).ToList()
            };
        }

        private static CurrentWeather MapToCurrentWeather(CurrentWeatherInfo current, int timeZoneOffset)
        {
            if (current == null)
                return null;

            return new CurrentWeather
            {
                Date = UnixTimeStampToDateTime(current.Dt, timeZoneOffset),
                Temperature = Math.Round(current.Temp - 273.15,1),
                FeelsLike = Math.Round(current.FeelsLike - 273.15,1),
                Humidity = current.Humidity,
                WindSpped = current.WindSpeed,
                WeatherCondition = current.WeatherConditionDescription
            };
        }

        private static WeatherAggregator.Application.DTOs.Weather.HourlyForecast MapToHourlyForecast(WeatherAggregator.Domain.Entities.Weather.HourlyForecast domainHourly, int timeZoneOffset)
        {
            if (domainHourly == null)
                return null;

            return new WeatherAggregator.Application.DTOs.Weather.HourlyForecast
            {
                Date = UnixTimeStampToDateTime(domainHourly.Dt, timeZoneOffset),
                Temperature = Math.Round(domainHourly.Temp - 273.15,1),
                FeelsLike = Math.Round(domainHourly.FeelsLike - 273.15,1),
                Humidity = domainHourly.Humidity,
                WindSpeed = domainHourly.WindSpeed,
                WeatherConditionDescription = domainHourly.WeatherConditionDescription
            };
        }

        private static WeatherAggregator.Application.DTOs.Weather.DailyForecast MapToDailyForecast(WeatherAggregator.Domain.Entities.Weather.DailyForecast domainDaily, int timeZoneOffset)
        {
            if (domainDaily == null)
                return null;

            return new WeatherAggregator.Application.DTOs.Weather.DailyForecast
            {
                Date = UnixTimeStampToDateTime(domainDaily.Dt, timeZoneOffset),
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
                Day = Math.Round(temp.Day - 273.15,1),
                Min = Math.Round(temp.Min - 273.15,1),
                Max = Math.Round(temp.Max - 273.15,1),
                Night = Math.Round(temp.Night - 273.15,1),
                Evening = Math.Round(temp.Eve - 273.15,1),
                Morning = Math.Round(temp.Morn - 273.15,1)
            };
        }

        private static Alert MapToAlert(WeatherAlertInfo alert, int timeZoneOffset)
        {
            if (alert == null)
                return null;

            return new Alert
            {
                EventName = alert.Event,
                Start = UnixTimeStampToDateTime(alert.Start, timeZoneOffset),
                End = UnixTimeStampToDateTime(alert.End, timeZoneOffset),
                Description = alert.Description
            };
        }

        private static DateTime UnixTimeStampToDateTime(long unixTimeStamp, int timeZoneOffset)
        {
            DateTimeOffset utcDateTime = DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp);
            return utcDateTime.ToOffset(TimeSpan.FromSeconds(timeZoneOffset)).DateTime;
        }
    }
}
