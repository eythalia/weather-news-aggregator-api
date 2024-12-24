using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAggregator.Application.DTOs.Location;
using WeatherAggregator.Domain.Entities.Location;

namespace WeatherAggregator.Application.Mappers
{
    public static class LocationToLocationResponseMapper
    {
        public static LocationResponse Map(Location location) 
        {

            return new LocationResponse
            {
                Name = location.Name,
                Latitude = location.Lat,
                Longitude = location.Lon,
                Country = location.Country
            };

        }

        public static IEnumerable<LocationResponse> ToLocationResponseList(IEnumerable<Location> locations) 
        {
            return locations.Select(Map).ToList();
        }
    }
}
