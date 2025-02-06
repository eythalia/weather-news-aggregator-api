using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAggregator.Domain.Entities.News;
using WeatherAggregator.Domain.Entities.Weather;
using WeatherAggregator.Shared.Results;

namespace WeatherAggregator.Application.Interfaces
{
    public interface INewsRepository
    {
        Task<Result<IEnumerable<NewsDataIO>>> GetNewsAsync(string country);
    }
}
