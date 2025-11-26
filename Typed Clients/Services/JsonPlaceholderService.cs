using Domain;
using Microsoft.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Typed_Clients.Services
{
    /// <summary>
    /// Typed Client Service for CRUD operations with JSONPlaceholder API
    /// Demonstrates how to encapsulate HTTP logic in a reusable service
    /// This service has retry policies configured in Program.cs
    /// </summary>
    public class JsonPlaceholderService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Constructor that receives HttpClient via dependency injection
        /// HttpClient comes already configured with Polly resilience policies (retry)
        /// </summary>
        public JsonPlaceholderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");

            // Required Headers
            _httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            _httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "HttpRequestsSample");
        }

        /// <summary>
        /// Fetches all posts from the API
        /// Uses GetFromJsonAsync for automatic deserialization
        /// </summary>
        public async Task<IEnumerable<Post>?> GetAllPostsAsync()
            => await _httpClient.GetFromJsonAsync<IEnumerable<Post>>("/posts");

        /// <summary>
        /// Creates a new post
        /// Demonstrates the use of HttpRequestMessage for greater control over the request
        /// </summary>
        public async Task<Post?> Post(Post post)
        {
            // Serializes the Post object to JSON
            string jsonPost = JsonSerializer.Serialize(post);
            var request = new HttpRequestMessage(HttpMethod.Post, "/posts");
            request.Content = new StringContent(jsonPost, Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.SendAsync(request);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                return await JsonSerializer.DeserializeAsync<Post>(contentStream);
            }

            return null;
        }

        /// <summary>
        /// Updates an existing post
        /// </summary>
        public async Task<Post?> Put(Post post)
        {
            // Modifies the title to include the ID
            post.Title = $"{post.Title} - {post.Id}";

            string jsonPost = JsonSerializer.Serialize(post);
            var httpContent = new StringContent(jsonPost, Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PutAsync($"/posts/{post.Id}", httpContent);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                return await JsonSerializer.DeserializeAsync<Post>(contentStream);
            }

            return null;
        }

        /// <summary>
        /// Removes a post
        /// Returns the HTTP status code of the operation
        /// </summary>
        public async Task<HttpStatusCode> Delete(int id = 2)
        {
            var httpResponseMessage = await _httpClient.DeleteAsync($"/posts/{id}");

            return httpResponseMessage.StatusCode;
        }
    }
}
