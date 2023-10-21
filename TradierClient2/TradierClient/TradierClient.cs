using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using Tradier.Client.Helpers;
using Tradier.Client.Models.Account;
using Serilog;
using Tradier.Interfaces;


// ReSharper disable once CheckNamespace
namespace Tradier.Client
{
    /// <summary>
    /// The <c>TradierClient</c> class
    /// </summary>
    public class TradierClient
    {
        public Authentication Authentication { get; set; }
        public Account Account { get; set; }
        public MarketData MarketData { get; set; }
        public Trading Trading { get; set; }
        public WatchlistEndpoint Watchlist { get; set; }

        private readonly Serilog.ILogger _logger;
        private readonly ITradierDbContext _dbContext;

        /// <summary>
        /// The TradierClient constructor (with an existing HttpClient)
        /// </summary>
        public TradierClient(HttpClient httpClient, string apiToken, string defaultAccountNumber, ITradierDbContext dbContext, Serilog.ILogger logger = null, bool useProduction = false )
        {
            _logger = logger ?? Log.Logger;  // Use provided logger or fallback to static logger
            _logger.Information("Initializing TradierClient...");
            _dbContext= dbContext;

            Uri baseEndpoint = useProduction ? new Uri(Settings.PRODUCTION_ENDPOINT) : new Uri(Settings.SANDBOX_ENDPOINT);

            httpClient.BaseAddress = baseEndpoint;
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiToken}");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            Requests request = new Requests(httpClient);

            Authentication = new Authentication(request);
            Account = new Account(request, defaultAccountNumber);
            MarketData = new MarketData(request);
            Trading = new Trading(request, defaultAccountNumber);
            Watchlist = new WatchlistEndpoint(request);

            // TODO: Coming soon
            //Streaming = new Streaming(request);
        }

        /// <summary>
        /// The TradierClient constructor (with no HttpClient passed)
        /// </summary>
        //public TradierClient(string apiToken, string defaultAccountNumber, bool useProduction = true)
        //   : this(new HttpClient(), apiToken, defaultAccountNumber, useProduction)
        //{
        //}

        ///// <summary>
        ///// The TradierClient constructor (with no HttpClient and no defaultAccount passed)
        ///// </summary>
        //public TradierClient(string apiToken, bool useProduction = false)
        //   : this(new HttpClient(), apiToken, null, useProduction)
        //{
        //}
    }
}
