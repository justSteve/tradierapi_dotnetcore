using Microsoft.Extensions.DependencyInjection;
using Tradier.Client;
using Tradier.Interfaces;

public class TradierClientFactory
{
    private readonly IServiceProvider _serviceProvider;

    public TradierClientFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TradierClient Create(string auth, string acct, bool useProduction = true)
    {
        var httpClient = new HttpClient();
        var dbContext = _serviceProvider.GetRequiredService<ITradierDbContext>();
        return new TradierClient(httpClient, auth, acct, dbContext, null, useProduction);
    }

    // You can add more Create methods for other overloads if needed.
}
