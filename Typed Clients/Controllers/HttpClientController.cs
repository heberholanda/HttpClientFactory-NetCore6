using Domain;
using Microsoft.AspNetCore.Mvc;
using Typed_Clients.Services;

namespace Typed_Clients.Controllers
{
    /// <summary>
    /// Controller that uses Typed Client (GitHubService)
    /// Demonstrates the injection of a service that encapsulates HttpClient
    /// HTTP logic is isolated in the service, making the controller cleaner
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HttpClientController : ControllerBase
    {
        private readonly GitHubService _gitHubService;

        public IEnumerable<GitHubBranch>? _GitHubBranches { get; set; }

        /// <summary>
        /// Injects the GitHubService that was registered in Program.cs
        /// The service comes with HttpClient configured and timeout policies applied
        /// </summary>
        public HttpClientController(GitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        /// <summary>
        /// Fetches branches from AspNetCore.Docs repository
        /// HTTP logic is encapsulated in GitHubService
        /// This endpoint is subject to the timeout policy configured in Program.cs
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _GitHubBranches = await _gitHubService.GetAspNetCoreDocsBranchesAsync();
            }
            catch (HttpRequestException)
            {
                // Re-throws the exception so the error middleware can handle it
                throw;
            }
            return Ok(_GitHubBranches);
        }
    }
}