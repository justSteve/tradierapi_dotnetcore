using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tradier.Data;
using Tradier.Interfaces;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Database setup
        services.AddDbContext<TradierDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        // Register the interface to resolve to the concrete DbContext
        services.AddScoped<ITradierDbContext>(provider =>
            provider.GetService(typeof(TradierDbContext)) as ITradierDbContext);
        services.AddTransient<TradierClientFactory>();

        services.AddTransient<IRequestHandler, RequestHandler>();


        // Add other services
    }
}
