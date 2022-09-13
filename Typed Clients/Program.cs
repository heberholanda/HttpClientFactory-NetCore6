using Typed_clients.Middlewares;
using Typed_Clients.Services;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Middlewares
builder.Services.AddTransient<ValidateHeaderHandler>();

// Register HttpClient
builder.Services.AddHttpClient<GitHubService>();

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
