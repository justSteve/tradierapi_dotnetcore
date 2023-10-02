using cli.Helpers;
using Tradier.Client;
using Tradier.Client.Models.Account;
using Tradier.Client.Models.MarketData;
using Tradier.Client.Models.Trading;
using cli;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

//var settingsManager = new cli.Helpers.SecretManager();
//var apiSettings = settingsManager.LoadSecrets();

//TradierClient client = new TradierClient("Somethi", "apiSettings.TradierAccountId");

//////Tradier.Client.TradierClient client = new Tradier.Client.TradierClient("HQkigAvv7GtJEMKcbm5DPlbSUyrp", "VA94955401");
////// See https://aka.ms/new-console-template for more information

//////Profile userProfile = await client.Account.GetUserProfile();
//////Balances balance = await client.Account.GetBalances();
//////Positions positions = await client.Account.GetPositions();
////History history = await client.Account.GetHistory();
//////GainLoss gainLoss = await client.Account.GetGainLoss();
////Orders orders = await client.Account.GetOrders();
//////Order getOrder = await client.Account.GetOrder(orders.Order.FirstOrDefault().Id);

//////Quotes quotes = await client.MarketData.GetQuotes("AAPL, NFLX");
////Quotes quotes1 = await client.MarketData.PostGetQuotes("aapl");

//////TOSOrder tOrder = ParseTOSOrder("SELL -1 VERTICAL SPX 100 (Weeklys) 27 FEB 23 3980/3975 PUT @1.10 LMT");


////Options options = await client.MarketData.GetOptionChain("SPX", "09-29-2023");
//////Strikes strikes = await client.MarketData.GetStrikes("SPXW", "02-27-2023");
//////Expirations expirations = await client.MarketData.GetOptionExpirations("AAPL");
////List<Symbol> lookup = await client.MarketData.LookupOptionSymbols("SPX");
//////HistoricalQuotes historicalQuotes = await client.MarketData.GetHistoricalQuotes("aapl", "daily", "January 1, 2023", "January 28, 2023");
//////Series timeSales = await client.MarketData.GetTimeSales("AAPL", "1min", "July 1, 2020", "July 11, 2020");
//////Securities securities = await client.MarketData.GetEtbSecurities();
//////Clock clock = await client.MarketData.GetClock();
//////Calendar calendar = await client.MarketData.GetCalendar();
//////Securities securitiesFilter = await client.MarketData.SearchCompanies("NY");
//////Securities lookup1 = await client.MarketData.LookupSymbol("goog");


//////OrderPreviewResponse order = (OrderPreviewResponse)await client.Trading.PlaceEquityOrder("WMT", "buy", 10, "limit", "day", 1.00, preview: true);
//////OrderReponse order1 = (OrderReponse)await client.Trading.PlaceOptionOrder("WMT", "WMT200717C00129000", "buy_to_open", 1, "limit", "day", 10.00);
////OrderReponse order2 = (OrderReponse)await client.Trading.PlaceMultilegOrder("SPX", "debit", "day", new List<(string, string, int)>
////{ ("WMT200717C00129000", "buy_to_open", 1), ("WMT200717C00132000", "sell_to_open", 1) }, 1.30);
//////OrderReponse order3 = (OrderReponse)await client.Trading.PlaceComboOrder("SPY", "limit", "day", new List<(string, string, int)> { ("SPY", "buy", 1), ("SPY140118C00195000", "buy_to_open", 1) }, 1.00);
//////OrderReponse order4 = (OrderReponse)await client.Trading.PlaceOtoOrder("day", new List<(string, int, string, string, string, double?, double?)> { ("WMT", 1, "limit", "WMT200717C00129000", "buy_to_open", 1.00, null), ("WMT", 1, "limit", "WMT200717C00129000", "sell_to_close", 1.10, null) });
//////OrderReponse order5 = (OrderReponse)await client.Trading.PlaceOcoOrder("day", new List<(string, int, string, string, string, double?, double?)> { ("SPY", 1, "limit", "SPY140118C00195000", "buy_to_open", 1.00, null), ("SPY", 1, "limit", "SPY140118C00195000", "sell_to_close", 1.10, null) });
//////OrderReponse order6 = await client.Trading.ModifyOrder(order1.Id, "limit", "day", 5.00);
//////order1 = await client.Trading.CancelOrder(order1.Id);
////Console.WriteLine("Hello, World!");
