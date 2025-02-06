using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAggregator.Application.Interfaces;
using WeatherAggregator.Domain.Entities.News;
using WeatherAggregator.Shared.Results;

namespace WeatherAggregator.Infrastructure.Repositories
{

    public class NewsRepository : INewsRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public NewsRepository(HttpClient httpClient, IConfiguration configuration) 
        { 
            _httpClient = httpClient;
            _apiKey = configuration["NewsDataIO:ApiKey"];
        }
        public async Task<Result<IEnumerable<NewsDataIO>>> GetNewsAsync(string country)
        {
            try
            {
                var endpoint = $"https://newsdata.io/api/1/latest?apikey={_apiKey}&country={country}";
                var response = await _httpClient.GetAsync(endpoint);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return Result<IEnumerable<NewsDataIO>>.Failure($"External API Error: {errorMessage}", (int)response.StatusCode);

                }

                var result = await response.Content.ReadAsStringAsync();
                var news = JsonConvert.DeserializeObject<List<NewsDataIO>>(result);

                return Result<IEnumerable<NewsDataIO>>.Success(news, (int)response.StatusCode);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<NewsDataIO>>.Failure($"Failed to fetch locations: {ex.Message}", 500);
            }
        }
    }
}
