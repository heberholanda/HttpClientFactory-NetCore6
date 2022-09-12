using Domain;
using Generated_Clients.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Generated_Clients.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrudHttpClientController : ControllerBase
    {
        private readonly IJsonPlaceholderClient _jsonPlaceholderClient;

        public CrudHttpClientController(IJsonPlaceholderClient jsonPlaceholderClient)
        {
            _jsonPlaceholderClient = jsonPlaceholderClient;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _jsonPlaceholderClient.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post(Post post)
        {
            return Ok(await _jsonPlaceholderClient.Post(post));
        }

        [HttpPut]
        public async Task<IActionResult> Put(Post post)
        {
            return Ok(await _jsonPlaceholderClient.Put(post.Id, post));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id = 2)
        {
            await _jsonPlaceholderClient.Delete(id);

            return Ok();
        }
    }
}