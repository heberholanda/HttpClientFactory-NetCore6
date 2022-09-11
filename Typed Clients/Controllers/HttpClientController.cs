using Domain;
using Microsoft.AspNetCore.Mvc;
using Typed_Clients.Services;

namespace Typed_Clients.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HttpClientController : ControllerBase
    {
        private readonly GitHubService _gitHubService;

        public IEnumerable<GitHubBranch>? _GitHubBranches { get; set; }

        public HttpClientController(GitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _GitHubBranches = await _gitHubService.GetAspNetCoreDocsBranchesAsync();
            }
            catch (HttpRequestException)
            {

                throw;
            }
            return Ok(_GitHubBranches);
        }
    }
}