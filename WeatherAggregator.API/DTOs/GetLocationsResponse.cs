namespace WeatherAggregator.API.DTOs
{
    public class GetLocationsResponse<T>
    {
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
