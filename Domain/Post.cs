using System.Text.Json.Serialization;

namespace Domain
{
    public class Post
    {
        [JsonPropertyName("UserId")]
        public int UserId { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("Title")]
        public string? Title { get; set; }

        [JsonPropertyName("Body")]
        public string Body { get; set; }
    }
}