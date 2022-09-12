using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Named_Clients.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrudHttpClientController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public IEnumerable<Post>? _Posts { get; set; }
        private static string? BaseUrl { get; set; }

        public CrudHttpClientController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            BaseUrl = "https://jsonplaceholder.typicode.com";
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/posts")
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.UserAgent, "HttpRequestsSample" }
                }
            };

            var httpClient = _httpClientFactory.CreateClient("GitHub");
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                _Posts = await JsonSerializer.DeserializeAsync<IEnumerable<Post>>(contentStream);
            }

            return Ok(_Posts);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Post newPost)
        {
            Post result = new();

            string jsonPost = JsonSerializer.Serialize(newPost);
            var httpContent = new StringContent(jsonPost, Encoding.UTF8, "application/json");

            var httpClient = _httpClientFactory.CreateClient("GitHub");
            var httpResponseMessage = await httpClient.PostAsync($"{BaseUrl}/posts", httpContent);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                result = await JsonSerializer.DeserializeAsync<Post>(contentStream);
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Post newPost)
        {
            newPost.Title = $"{newPost.Title} - {newPost.Id}";

            Post result = new();

            string jsonPost = JsonSerializer.Serialize(newPost);
            var httpContent = new StringContent(jsonPost, Encoding.UTF8, "application/json");

            var httpClient = _httpClientFactory.CreateClient("GitHub");
            var httpResponseMessage = await httpClient.PutAsync($"{BaseUrl}/posts/{newPost.Id}", httpContent);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                result = await JsonSerializer.DeserializeAsync<Post>(contentStream);
            }

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id = 2)
        {
            var httpClient = _httpClientFactory.CreateClient("GitHub");
            var httpResponseMessage = await httpClient.DeleteAsync($"{BaseUrl}/posts/{id}");

            return Ok(httpResponseMessage.StatusCode);
        }
    }
}