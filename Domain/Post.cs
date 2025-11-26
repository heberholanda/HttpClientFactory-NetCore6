using System.Text.Json.Serialization;

namespace Domain
{
    /// <summary>
    /// Data model representing a Post from JSONPlaceholder API
    /// Used for JSON serialization/deserialization
    /// </summary>
    public class Post
    {
        /// <summary>
        /// ID of the user who created the post
        /// </summary>
        [JsonPropertyName("UserId")]
        public int UserId { get; set; }

        /// <summary>
        /// Unique identifier of the post
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Title of the post
        /// </summary>
        [JsonPropertyName("Title")]
        public string? Title { get; set; }

        /// <summary>
        /// Content/body of the post
        /// </summary>
        [JsonPropertyName("Body")]
        public string Body { get; set; }
    }
}