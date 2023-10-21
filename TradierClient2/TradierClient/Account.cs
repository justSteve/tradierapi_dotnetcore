﻿using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using Tradier.Client.Exceptions;
using Tradier.Client.Helpers;
using Tradier.Client.Models.Account;
using Tradier.Interfaces;


using Serilog;
using Microsoft.EntityFrameworkCore;

// ReSharper disable once CheckNamespace
namespace Tradier.Client
{
    /// <summary>
    /// The <c>Account</c> class. 
    /// </summary>
    public class Account
    {
        private readonly Requests _requests;
        private readonly string _accountNumber = "";
        private readonly string _authToken = "";
        public static List<Strade> GlobalStradesList;
        public static List<StradeFly> GlobalStradeFlysList;
        private readonly ITradierDbContext _dbContext;

        private readonly Serilog.ILogger _logger;

        // Other member variables and methods


        /// <summary>
        /// Account Constructor
        /// </summary>
        public Account(Requests requests, string defaultAccountNumber, Serilog.ILogger logger = null, ITradierDbContext dbContext = null)
        {
            _requests = requests;
            _accountNumber = defaultAccountNumber;
            _logger = logger ?? Log.Logger;  // Use provided logger or fallback to static logger
            _dbContext = dbContext;
        }

        /// <summary>
        /// Retrieve orders placed within an account
        /// </summary>
        public async Task<Orders> GetOrders(string accountNumber = null)
        {
            accountNumber = string.IsNullOrEmpty(accountNumber) ? _accountNumber : accountNumber;

            if (string.IsNullOrEmpty(accountNumber))
            {
                throw new MissingAccountNumberException();
            }

            if (string.IsNullOrEmpty(accountNumber))
            {
                throw new MissingAccountNumberException();
            }

            var response = await _requests.GetRequest($"accounts/{accountNumber}/orders");

            var rsp = JsonConvert.DeserializeObject<OrdersRootobject>(response).Orders;

            // parse 

            return rsp;

        }

        /// <summary>
        /// Retrieve orders placed within an account
        /// </summary>
        public async Task<IEnumerable<Strade>> GetStrades(string accountNumber = null)
        {
            accountNumber = string.IsNullOrEmpty(accountNumber) ? _accountNumber : accountNumber;

            if (string.IsNullOrEmpty(accountNumber))
            {
                throw new MissingAccountNumberException();
            }

            if (string.IsNullOrEmpty(accountNumber))
            {
                throw new MissingAccountNumberException();
            }

            var response = await _requests.GetRequest($"accounts/{accountNumber}/orders");

            _logger.Information(nameof(response));

            var rsp = JsonConvert.DeserializeObject<OrdersRootobject>(response).Orders;


            string json = File.ReadAllText("C:\\Users\\steve\\OneDrive\\Code\\tradier-api\\orders.15_03.json");
            var ordersRoot = JsonConvert.DeserializeObject<OrdersRootobject>(json);
            var o = 0;
            var c = 0;

            // LINQ query to filter orders
            var filteredOrders = ordersRoot.Orders.Order
                .Where(o => o.Status == "filled" && o.NumLegs == 3)
                //.Where(o => o.Legs.All(l => l.Side.Contains("open")))
                .OrderBy(o => o.TransactionDate)
                .ToList();


            // Print or further process the filtered orders
            // ... previous code

            foreach (var order in filteredOrders)
            {
                var strike = 0;  // Center strike
                var sideType = "c";
                DateTime expiry = DateTime.Now;

                List<int> legStrikes = new List<int>();  // To store all strikes of the order

                foreach (var leg in order.Legs)
                {
                    OptionSymbolHelper.ParseOCCSymbol(leg.OptionSymbol, out string _underlying, out DateTime _expiry, out string _side, out int _strike);
                    legStrikes.Add(_strike);
                    if (leg.Side == "sell_to_open")
                    {
                        strike = _strike;
                        expiry = _expiry;
                    }
                }

                LoadStrades(_dbContext, expiry);

                var widthsFromCenter = legStrikes.Select(s => Math.Abs(s - strike)).Distinct().ToList();
                int width = widthsFromCenter.FirstOrDefault(w => w != 0);

                bool foundExistingStrade = false;
                foreach (var strade in GlobalStradesList)
                {
                    if (strike == strade.Strike)
                    {
                        foundExistingStrade = true;
                        bool foundExistingFly = false;
                        foreach (var fly in strade.Flies)
                        {
                            // Compute the width for the fly based on the first order's legs
                            var flyWidths = fly.Orders.First().Legs.Select(l =>
                            {
                                OptionSymbolHelper.ParseOCCSymbol(l.OptionSymbol, out _, out _, out _, out int _strike);
                                return Math.Abs(_strike - strike);
                            }).Distinct().ToList();

                            //var flyWidths = fly.Orders.First().Legs.Select(l => Math.Abs(l.Strike - strike)).Distinct().ToList();
                            int flyWidth = flyWidths.FirstOrDefault(w => w != 0);

                            if (flyWidth == width)
                            {
                                foundExistingFly = true;
                                fly.Orders.Add(order);
                                break;
                            }
                        }

                        if (!foundExistingFly)
                        {
                            var newFly = new StradeFly(strike, sideType, expiry, order);
                            strade.Flies.Add(newFly);
                        }

                        break;
                    }
                }

                if (!foundExistingStrade)
                {
                    var newStrade = new Strade
                    {
                        Strike = strike,
                        Type = sideType,
                        Expiry = expiry,
                        Flies = new List<StradeFly>
                    {
                        new StradeFly(strike, sideType, expiry, order)
                    }
                    };
                    GlobalStradesList.Add(newStrade);
                }
            }
            return null;


        }

        IEnumerable<Strade> LoadStrades(ITradierDbContext context, DateTime targetDate)
        {
            return context.Strades
                .Include(s => s.Flies)
                    .ThenInclude(f => f.Orders) // If you also want to load the Orders
                .Where(s => s.Expiry == targetDate);

        }

        private static void CreateStrade(int strike, string sideType, DateTime expiry, StradeFly fly)
        {
            var strade = new Strade
            {
                Strike = strike,
                Type = sideType,
                Expiry = expiry,
                Flies = new List<StradeFly> { fly }
            };
            GlobalStradesList.Add(strade);
        }


        // Load Strade objects from a JSON file into the global list
        public static void LoadStradesFromFile()
        {
            if (File.Exists("C:\\Users\\steve\\Source\\Repos\\TradierClient2\\TradierClient2\\parseOrders\\GlobalStradesList.json"))
            {
                string json = File.ReadAllText("C:\\Users\\steve\\Source\\Repos\\TradierClient2\\TradierClient2\\parseOrders\\GlobalStradesList.json");
                GlobalStradesList = JsonConvert.DeserializeObject<List<Strade>>(json);
            }
            else
            {
                GlobalStradesList = new List<Strade>();  // Initialize as an empty list if the file doesn't exist
            }
        }

        // Save the global list of Strade objects to a JSON file
        public static void SaveStradesToFile()
        {

            string json = JsonConvert.SerializeObject(GlobalStradesList, Formatting.Indented);


            File.WriteAllText("C:\\Users\\steve\\Source\\Repos\\TradierClient2\\TradierClient2\\parseOrders\\GlobalStradesList.json", json);
        }


        /// <summary>
        /// Get balances information for a specific or a default user account.
        /// </summary>
        public async Task<Balances> GetBalances()
        {
            _logger.Information("hit bal");
            var response = await _requests.GetRequest($"accounts/{_accountNumber}/balances");

            _logger.Information($"{nameof(Balances)}: {response}");

            var rsp = JsonConvert.DeserializeObject<BalanceRootObject>(response).Balances;

            //outline steps to commit rsp to sql db. please presume that all my connection string bases have been covered and you need not address them unless asked.

            return rsp;


        }


        /// <summary>
        /// Get the current positions being held in an account. These positions are updated intraday via trading
        /// </summary>
        public async Task<Positions> GetPositions(string accountNumber = null)
        {
            accountNumber = string.IsNullOrEmpty(accountNumber) ? _accountNumber : accountNumber;

            if (string.IsNullOrEmpty(accountNumber))
            {
                throw new MissingAccountNumberException();
            }

            var response = await _requests.GetRequest($"accounts/{accountNumber}/positions");
            return JsonConvert.DeserializeObject<PositionsRootobject>(response).Positions;
        }

        /// <summary>
        /// Get historical activity for the default account
        /// </summary>
        public async Task<History> GetHistory(int page = 1, int limitPerPage = 25)
        {
            if (string.IsNullOrEmpty(_accountNumber))
            {
                throw new MissingAccountNumberException("The default account number was not defined.");
            }

            return await GetHistory(_accountNumber, page, limitPerPage);
        }

        /// <summary>
        /// Get historical activity for an account
        /// </summary>
        public async Task<History> GetHistory(string accountNumber, int page = 1, int limitPerPage = 25)
        {
            var response = await _requests.GetRequest($"accounts/{accountNumber}/history?page={page}&limit={limitPerPage}");
            return JsonConvert.DeserializeObject<HistoryRootobject>(response).History;
        }

        /// <summary>
        /// Get cost basis information for the default user account
        /// </summary>
        public async Task<GainLoss> GetGainLoss(int page = 1, int limitPerPage = 25)
        {
            if (string.IsNullOrEmpty(_accountNumber))
            {
                throw new MissingAccountNumberException("The default account number was not defined.");
            }

            return await GetGainLoss(_accountNumber, page, limitPerPage);
        }

        /// <summary>
        /// Get cost basis information for a specific user account
        /// </summary>
        public async Task<GainLoss> GetGainLoss(string accountNumber, int page = 1, int limitPerPage = 25)
        {
            var response = await _requests.GetRequest($"accounts/{accountNumber}/gainloss?page={page}&limit={limitPerPage}");
            return JsonConvert.DeserializeObject<GainLossRootobject>(response).GainLoss;
        }


        /// <summary>
        /// Get detailed information about a previously placed order in the default account
        /// </summary>
        public async Task<Order> GetOrder(int orderId)
        {
            if (string.IsNullOrEmpty(_accountNumber))
            {
                throw new MissingAccountNumberException("The default account number was not defined.");
            }

            return await GetOrder(_accountNumber, orderId);
        }

        /// <summary>
        /// Get detailed information about a previously placed order
        /// </summary>
        public async Task<Order> GetOrder(string accountNumber, int orderId)
        {
            var response = await _requests.GetRequest($"accounts/{accountNumber}/orders/{orderId}");
            return JsonConvert.DeserializeObject<Orders>(response).Order.FirstOrDefault();
        }

        /// <summary>
        /// The user’s profile contains information pertaining to the user and his/her accounts
        /// </summary>
        public async Task<Profile> GetUserProfile()
        {
            var response = await _requests.GetRequest("user/profile");
            return JsonConvert.DeserializeObject<ProfileRootObject>(response).Profile;
        }


    }
}
