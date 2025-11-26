using System.Text.Json.Serialization;

namespace Domain
{
    /// <summary>
    /// Data model representing a GitHub branch
    /// Used to deserialize GitHub API response
    /// </summary>
    public class GitHubBranch
    {
        /// <summary>
        /// Name of the branch
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Information about the branch's last commit
        /// </summary>
        [JsonPropertyName("commit")]
        public Commit? Commit { get; set; }

        /// <summary>
        /// Indicates if the branch is protected
        /// </summary>
        [JsonPropertyName("protected")]
        public bool Protected { get; set; }
    }

    /// <summary>
    /// Data model representing a GitHub commit
    /// </summary>
    public class Commit
    {
        /// <summary>
        /// SHA hash of the commit
        /// </summary>
        [JsonPropertyName("sha")]
        public string? Sha { get; set; }

        /// <summary>
        /// API URL to access commit details
        /// </summary>
        [JsonPropertyName("url")]
        public Uri? Url { get; set; }
    }
}