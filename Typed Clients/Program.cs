using Typed_clients.Middlewares;
using Typed_Clients.Services;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Middlewares
// Registers the custom handler as a transient service (new instance per request)
builder.Services.AddTransient<ValidateHeaderHandler>();

// Polly
// Defines resilience policies using Polly for failure handling and timeouts
// Short timeout policy for fast operations (200ms)
var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromMilliseconds(200));
// Long timeout policy for longer operations (1000ms)
var longTimeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromMilliseconds(1000));

// Register HttpClient
// Typed Client for GitHubService with conditional timeout policy
// Applies short timeout (200ms) for GET requests and long timeout (1000ms) for other operations
builder.Services.AddHttpClient<GitHubService>()
    .AddPolicyHandler(httpRequestMessage =>
        httpRequestMessage.Method == HttpMethod.Get ? timeoutPolicy : longTimeoutPolicy);

// Typed Client for JsonPlaceholderService with custom handler and retry policy
// AddHttpMessageHandler: adds a custom handler for header validation
// AddTransientHttpErrorPolicy: retry policy that attempts 3 times with 100ms interval between attempts
builder.Services.AddHttpClient<JsonPlaceholderService>()
    .AddHttpMessageHandler<ValidateHeaderHandler>()
    .AddTransientHttpErrorPolicy(policyBuilder =>
        policyBuilder.WaitAndRetryAsync(3, retryNumber => TimeSpan.FromMilliseconds(100)));

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
