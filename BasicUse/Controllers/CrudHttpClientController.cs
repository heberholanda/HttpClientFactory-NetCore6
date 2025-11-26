using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace HttpClientFactory.Controllers
{
    /// <summary>
    /// Controller that demonstrates basic CRUD operations using IHttpClientFactory
    /// Example of basic HttpClient usage without specific configurations
    /// </summary>
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

        /// <summary>
        /// Example of GET request using HttpRequestMessage
        /// Fetches all posts from JSONPlaceholder API
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // Creates an HTTP request message with GET method and configures required headers
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/posts")
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.UserAgent, "HttpRequestsSample" }
                }
            };

            // Creates an HttpClient instance through the factory
            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            // Checks if the request was successful (status code 2xx)
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                // Deserializes the returned JSON to a Post collection
                _Posts = await JsonSerializer.DeserializeAsync<IEnumerable<Post>>(contentStream);
            }

            return Ok(_Posts);
        }

        /// <summary>
        /// Example of POST request
        /// Creates a new post in JSONPlaceholder API
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post(Post newPost)
        {
            Post result = new();

            // Serializes the Post object to JSON
            string jsonPost = JsonSerializer.Serialize(newPost);
            // Creates HTTP content with the serialized JSON
            var httpContent = new StringContent(jsonPost, Encoding.UTF8, "application/json");

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.PostAsync($"{BaseUrl}/posts", httpContent);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                // Deserializes the Post returned by the API
                result = await JsonSerializer.DeserializeAsync<Post>(contentStream);
            }

            return Ok(result);
        }

        /// <summary>
        /// Example of PUT request
        /// Updates an existing post in JSONPlaceholder API
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Put(Post newPost)
        {
            // Modifies the post title to include the ID
            newPost.Title = $"{newPost.Title} - {newPost.Id}";

            Post result = new();

            string jsonPost = JsonSerializer.Serialize(newPost);
            var httpContent = new StringContent(jsonPost, Encoding.UTF8, "application/json");

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.PutAsync($"{BaseUrl}/posts/{newPost.Id}", httpContent);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                result = await JsonSerializer.DeserializeAsync<Post>(contentStream);
            }

            return Ok(result);
        }

        /// <summary>
        /// Example of DELETE request
        /// Removes a post from JSONPlaceholder API
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id = 2)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.DeleteAsync($"{BaseUrl}/posts/{id}");

            // Returns the status code of the delete operation
            return Ok(httpResponseMessage.StatusCode);
        }
    }
}