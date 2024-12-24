namespace WeatherAggregator.API.DTOs
{
    public class ApiGenericResponse<T>
    {
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
