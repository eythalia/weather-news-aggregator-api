using WeatherAggregator.API.DTOs;
using WeatherAggregator.Domain.DTOs;

namespace WeatherAggregator.API.Mappers
{
    public static class LocationMapper
    {
        public static LocationsDTO ToDto(LocationResponse response)
        {
            return new LocationsDTO
            {
                Name = response.Name,
                Latitude = response.Lat,
                Longitude = response.Lon,
                Country = response.Country
            };
        }

        /// <summary>
        /// Maps a collection of LocationResponse to a collection of LocationDto.
        /// </summary>
        public static IEnumerable<LocationsDTO> ToDtoList(IEnumerable<LocationResponse> responses)
        {
            return responses.Select(ToDto).ToList();
        }
    }
}
