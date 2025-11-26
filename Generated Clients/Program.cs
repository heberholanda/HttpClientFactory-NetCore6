using Refit;
using Generated_Clients.Interfaces;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register Refit Client
// Refit is a library that automatically generates HTTP client implementations from interfaces
// Registers the Refit client for GitHub API
builder.Services.AddRefitClient<IGitHubClient>()
    .ConfigureHttpClient(httpClient =>
    {
        // Defines the base address for all requests
        httpClient.BaseAddress = new Uri("https://api.github.com/");

        // Required headers
        // Mandatory headers for communication with GitHub API
        httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept,    "application/vnd.github.v3+json");
        httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "HttpRequestsSample");
    });

// Registers the Refit client for JsonPlaceholder API
builder.Services.AddRefitClient<IJsonPlaceholderClient>()
    .ConfigureHttpClient(httpClient =>
    {
        // Defines the base address for all requests
        httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");

        // Required headers
        // Mandatory headers for communication with the API
        httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
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
