using MarketPulse.Server.Services;
using MarketPulse.Server.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>(true);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .AllowAnyOrigin()   // For development, allows any origin
            .AllowAnyMethod()   // Allows any HTTP method
            .AllowAnyHeader();  // Allows any header
    });
});

// Add services to the container
builder.Services.AddHttpClient<FinnhubService>();
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions();

builder.Services.AddHostedService<StockPriceBackgroundService>();

var app = builder.Build();

app.UseCors();
app.UseWebSockets();
app.MapGet("/", () => "Welcome to Market Pulse!");
app.MapGraphQL();
app.MapBananaCakePop("/graphql/ui");
app.Run();
