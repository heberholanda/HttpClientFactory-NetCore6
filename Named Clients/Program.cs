using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register HttpClient
// Registers a named HttpClient "GitHub" with specific configurations
// Named Clients allow having multiple HTTP clients with different configurations in the same application
builder.Services.AddHttpClient("GitHub", httpClient =>
{
    // Defines the base address for all requests made with this client
    httpClient.BaseAddress = new Uri("https://api.github.com/");

    // Required Headers
    // Mandatory headers for communication with GitHub API
    httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept,    "application/vnd.github.v3+json");
    httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "HttpRequestsSample");
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
