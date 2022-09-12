using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Typed_Clients.Services;

namespace Typed_Clients.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrudHttpClientController : ControllerBase
    {
        private readonly JsonPlaceholderService _jsonPlaceholderService;

        public CrudHttpClientController(JsonPlaceholderService jsonPlaceholderService)
        {
            _jsonPlaceholderService = jsonPlaceholderService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _jsonPlaceholderService.GetAllPostsAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post(Post post)
        {
            return Ok(await _jsonPlaceholderService.Post(post));
        }

        [HttpPut]
        public async Task<IActionResult> Put(Post post)
        {
            return Ok(await _jsonPlaceholderService.Put(post));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id = 2)
        {
            return Ok(await _jsonPlaceholderService.Delete(id));
        }
    }
}