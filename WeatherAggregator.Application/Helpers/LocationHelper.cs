using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAggregator.Domain.Entities;
using WeatherAggregator.Shared.Results;
using Newtonsoft.Json;
using WeatherAggregator.Application.DTOs.Location;

namespace WeatherAggregator.Application.Helpers
{
    public static class LocationHelper
    {
        public static string ExtractCountryPrefix(string countryName)
        {
            return string.Concat(countryName[0], countryName[1]); //no need even for try catch because country name has been validated
        }

        public static async Task<LocationResponse> FilterLocationsByCountryPrefix(IEnumerable<LocationResponse> locations, string countryPrefix) 
        {
            var singleLocationObject = locations.FirstOrDefault(x => x.Country.Equals(countryPrefix, StringComparison.OrdinalIgnoreCase));
            return singleLocationObject;
        }

    }
}
