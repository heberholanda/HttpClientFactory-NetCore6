using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Named_Clients.Controllers
{
    /// <summary>
    /// Controller that demonstrates the use of Named Clients
    /// The "GitHub" client was pre-configured in Program.cs with base URL and headers
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HttpClientController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public IEnumerable<GitHubBranch>? _GitHubBranches { get; set; }

        public HttpClientController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Fetches repository branches using a Named Client
        /// Note that there's no need to configure base URL or headers - they're already pre-configured
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // Creates an HttpClient using the "GitHub" name that was registered in Program.cs
            // This client already has BaseAddress and Headers pre-configured
            var httpClient = _httpClientFactory.CreateClient("GitHub");
            
            // Since BaseAddress is already configured, we only pass the relative path
            var httpResponseMessage = await httpClient.GetAsync("repos/dotnet/AspNetCore.Docs/branches");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream =
                    await httpResponseMessage.Content.ReadAsStreamAsync();

                _GitHubBranches = await JsonSerializer.DeserializeAsync<IEnumerable<GitHubBranch>>(contentStream);
            }

            return Ok(_GitHubBranches);
        }
    }
}