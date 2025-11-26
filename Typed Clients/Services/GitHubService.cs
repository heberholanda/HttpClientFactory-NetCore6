using Domain;
using Microsoft.Net.Http.Headers;

namespace Typed_Clients.Services
{
    /// <summary>
    /// Typed Client Service for interaction with GitHub API
    /// Encapsulates HTTP communication logic in a specific service
    /// HttpClient is automatically injected by HttpClientFactory
    /// </summary>
    public class GitHubService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Constructor that receives HttpClient via dependency injection
        /// HttpClient is created and managed by HttpClientFactory
        /// </summary>
        public GitHubService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            // Configures the base address for all requests from this service
            _httpClient.BaseAddress = new Uri("https://api.github.com/");

            // Requires Headers
            // Adds mandatory headers for communication with GitHub API
            _httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/vnd.github.v3+json");
            _httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "HttpRequestsSample");
        }

        /// <summary>
        /// Fetches branches from AspNetCore.Docs repository
        /// Uses GetFromJsonAsync to simplify automatic JSON deserialization
        /// </summary>
        public async Task<IEnumerable<GitHubBranch>?> GetAspNetCoreDocsBranchesAsync()
            => await _httpClient.GetFromJsonAsync<IEnumerable<GitHubBranch>>("repos/dotnet/AspNetCore.Docs/branches");
    }
}
