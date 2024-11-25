using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAggregator.Domain.DTOs;
using WeatherAggregator.Domain.Entities;
using WeatherAggregator.Shared.Results;
using Newtonsoft.Json;

namespace WeatherAggregator.Application.Helpers
{
    public static class LocationHelper
    {
        public static string ExtractCountryPrefix(string countryName)
        {
            return string.Concat(countryName[0], countryName[1]); //no need even for try catch because country name has been validated
        }

        public static async Task<Result<Location>> FilterLocationsByCountryPrefix(Result<IEnumerable<Location>> locations, string countryPrefix) //efara edw den eimai sigouri gia ton location response
        {
            try
            {
                var singleLocationObject = locations.Data.FirstOrDefault(x => x.Country.Equals(countryPrefix, StringComparison.OrdinalIgnoreCase));
                if (singleLocationObject != null)
                {
                    //return singleLocationObject;
                    return Result<Location>.Success(singleLocationObject,200);
                }
                else 
                {
                    return Result<Location>.Failure($"No matching locations found for Country: {countryPrefix}", 404);
                }
            }
            catch (Exception ex)
            {
                return Result<Location>.Failure($"Internal error matching locations for Country: {countryPrefix}", 500);
            }

        }

    }
}
