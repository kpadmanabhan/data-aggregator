using Newtonsoft.Json;

namespace DataAggregator.Model
{
    public class Album
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
