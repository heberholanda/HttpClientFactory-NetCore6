using Refit;
using Domain;
using Generated_Clients.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Generated_Clients.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HttpClientController : ControllerBase
    {
        private readonly IGitHubClient _gitHubClient;

        public HttpClientController(IGitHubClient gitHubClient)
        {
            _gitHubClient = gitHubClient;
        }

        public IEnumerable<GitHubBranch>? _GitHubBranches { get; set; }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _GitHubBranches = await _gitHubClient.GetAspNetCoreDocsBranchesAsync();
            }
            catch (HttpRequestException)
            {

                throw;
            }
            return Ok(_GitHubBranches);
        }
    }
}