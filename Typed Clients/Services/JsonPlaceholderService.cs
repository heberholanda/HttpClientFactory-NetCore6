using Domain;
using Microsoft.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Typed_Clients.Services
{
    public class JsonPlaceholderService
    {
        private readonly HttpClient _httpClient;

        public JsonPlaceholderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");

            // Required Headers
            _httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            _httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "HttpRequestsSample");
        }

        public async Task<IEnumerable<Post>?> GetAllPostsAsync()
            => await _httpClient.GetFromJsonAsync<IEnumerable<Post>>("/posts");

        public async Task<Post?> Post(Post post)
        {
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

        public async Task<Post?> Put(Post post)
        {
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

        public async Task<HttpStatusCode> Delete(int id = 2)
        {
            var httpResponseMessage = await _httpClient.DeleteAsync($"/posts/{id}");

            return httpResponseMessage.StatusCode;
        }
    }
}
