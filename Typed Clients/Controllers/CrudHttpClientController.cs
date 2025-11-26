using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Typed_Clients.Services;

namespace Typed_Clients.Controllers
{
    /// <summary>
    /// Controller that uses Typed Client for CRUD operations
    /// All HTTP logic is encapsulated in JsonPlaceholderService
    /// This service has retry policies and ValidateHeaderHandler applied
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CrudHttpClientController : ControllerBase
    {
        private readonly JsonPlaceholderService _jsonPlaceholderService;

        /// <summary>
        /// Injects the JsonPlaceholderService configured with:
        /// - ValidateHeaderHandler: validates headers before sending requests
        /// - Retry policy: attempts 3 times in case of transient failure
        /// </summary>
        public CrudHttpClientController(JsonPlaceholderService jsonPlaceholderService)
        {
            _jsonPlaceholderService = jsonPlaceholderService;
        }

        /// <summary>
        /// Fetches all posts
        /// If it fails, the retry policy will attempt up to 3 times
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _jsonPlaceholderService.GetAllPostsAsync());
        }

        /// <summary>
        /// Creates a new post
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post(Post post)
        {
            return Ok(await _jsonPlaceholderService.Post(post));
        }

        /// <summary>
        /// Updates an existing post
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Put(Post post)
        {
            return Ok(await _jsonPlaceholderService.Put(post));
        }

        /// <summary>
        /// Removes a post
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id = 2)
        {
            return Ok(await _jsonPlaceholderService.Delete(id));
        }
    }
}