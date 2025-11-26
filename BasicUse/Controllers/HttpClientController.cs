using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Text.Json;

namespace HttpClientFactory.Controllers
{
    /// <summary>
    /// Controller que demonstra o uso básico do IHttpClientFactory
    /// Exemplo: requisição GET simples para a API do GitHub
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HttpClientController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public IEnumerable<GitHubBranch>? _GitHubBranches { get; set; }

        public HttpClientController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Busca as branches do repositório AspNetCore.Docs no GitHub
        /// Demonstra como criar uma requisição HTTP com headers customizados
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // Cria uma mensagem de requisição HTTP GET para a API do GitHub
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/repos/dotnet/AspNetCore.Docs/branches")
            {
                Headers =
                {
                    // Headers obrigatórios para a API do GitHub
                    { HeaderNames.Accept, "application/vnd.github.v3+json" },
                    { HeaderNames.UserAgent, "HttpRequestsSample" }
                }
            };

            // Cria um HttpClient através do factory
            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                // Desserializa o JSON da resposta para uma coleção de GitHubBranch
                _GitHubBranches = await JsonSerializer.DeserializeAsync<IEnumerable<GitHubBranch>>(contentStream);
            }

            return Ok(_GitHubBranches);
        }
    }
}