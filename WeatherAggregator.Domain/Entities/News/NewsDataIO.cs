using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAggregator.Domain.Entities.News
{
    public class NewsDataIO
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("pubDate")]

        public string PublishDate { get; set; }

        [JsonProperty("pubDateTZ")]
        public string DateTZ { get; set; }


    }
}
