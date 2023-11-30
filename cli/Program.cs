using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using cli.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Tradier.Client.Helpers;
using Tradier.Client;
using Tradier.Data;
using Microsoft.EntityFrameworkCore;
using Tradier.Interfaces;
using Tradier.Entities.Models;


var builder = WebApplication.CreateBuilder(args);


// Database setup
builder.Services.AddDbContext<TradierDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the interface to resolve to the concrete DbContext
builder.Services.AddScoped<ITradierDbContext>(provider =>
    provider.GetService(typeof(TradierDbContext)) as ITradierDbContext);

builder.Services.AddTransient<TradierClientFactory>();
builder.Services.AddTransient<IRequestHandler, RequestHandler>();

var configuration = builder.Configuration;
string connectionString = configuration.GetConnectionString("DefaultConnection");

// Initialize Serilog
var sinkOpts = new MSSqlServerSinkOptions
{
    TableName = "Logs",
    AutoCreateSqlTable = true
};
//var userAcct = 
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.MSSqlServer(
        connectionString: connectionString,
        sinkOptions: sinkOpts,
        restrictedToMinimumLevel: LogEventLevel.Information
        )
    .WriteTo.File("log.txt", buffered: false)
    .CreateLogger();

builder.Host.UseSerilog();

// Register HttpClient for DI
builder.Services.AddHttpClient();

var app = builder.Build();
SecretManager secretManager = new SecretManager();

// Call the LoadSecrets method
Settings settings = secretManager.LoadSecrets();

// Use the settings object
var auth = settings.ACCESS_TOKEN_pjk;
var acct = settings.ACCOUNT_ID_pjk;

var httpClientFactory = app.Services.GetRequiredService<IHttpClientFactory>();
var httpClient = httpClientFactory.CreateClient();

// Set the base address if it's going to be the same for all requests
//httpClient.BaseAddress = new Uri("YourTradierAPIBaseUrlHere");

// If there are any headers that are going to be the same for all requests, set them here
//httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer YourTradierAPITokenHere");

app.MapGet("/", async () =>
{
    // Use the pre-configured httpClient to make requests
    var response = await httpClient.GetAsync(auth);

    if (response.IsSuccessStatusCode)
    {
        var content = await response.Content.ReadAsStringAsync();
        Log.Information("Tradier API response: {Content}", content);
    }
    else
    {
        Log.Error("Failed to call Tradier API. Status Code: {StatusCode}", response.StatusCode);
    }

    return "Hello World!";
});


app.MapGet("/getbalances", async () =>
{

    var tradierClientFactory = app.Services.GetRequiredService<TradierClientFactory>();

    //var tradierClientFactory = serviceProvider.GetRequiredService<TradierClientFactory>();

    // Initialize your Account object here
    var client = tradierClientFactory.Create(auth, acct, true);

    Balances balances = await client.Account.GetBalances();

    // Return a simple message or the balances object
    //
    return balances;
});

app.MapGet("/getorders", async () =>
{

    var tradierClientFactory = app.Services.GetRequiredService<TradierClientFactory>();
    // Initialize your Account object here
    var client = tradierClientFactory.Create(auth, acct, true);

    IEnumerable<Strade> strades = await client.Account.GetStrades();
    // Return a simple message or the balances object
    //
    return strades;
});

app.Run();
