using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using Tradier.Client.Exceptions;
using Tradier.Client.Helpers;
using Tradier.Client.Models.Account;
using Tradier.Interfaces;

using SharedHelpers2;
using Serilog;
using Microsoft.EntityFrameworkCore;
using Azure;
using Tradier.Client.Models.Trading;
using Tradier.Entities.Models;
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
        public Account(Requests requests, string defaultAccountNumber, Serilog.ILogger logger, string apiToken, ITradierDbContext dbContext)
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

            var response = await _requests.GetRequest($"accounts/{accountNumber}/orders");

            _logger.Information($"{nameof(GetStrades)}: {response}");

            var rsp = JsonConvert.DeserializeObject<OrdersRootobject>(response).Orders;


            string json = File.ReadAllText("C:\\Users\\steve\\OneDrive\\Code\\tradier-api\\orders.15_03.json");
            var ordersRoot = JsonConvert.DeserializeObject<OrdersRootobject>(json);
            var o = 0;
            var c = 0;

            int? maxId = _dbContext.Orders.Max(s => (int?)s.Id);


            // LINQ query to filter orders
            var filteredOrders = ordersRoot.Orders.Order
                    .Where(o => o.Status == "filled" && o.NumLegs == 3)
                    //.Where(o => o.Id > maxId)
                    .OrderBy(o => o.TransactionDate)
                    .ToList();



            foreach (var order in filteredOrders)
            {
                var strike = 0;
                var fillClass = order.Class = "option";
                var fillOS = order.OptionSymbol = "wtf";
                foreach (var leg1 in order.Legs)
                {
                    var fillClassLeg = leg1.Class = "option";
                }

                //save to db
                try
                {
                    _dbContext.Orders.Add(order);
                    var saveChanges = _dbContext.SaveChanges();
                }
                catch (Exception e)
                {
                    _logger.Fatal("Save Order" + e.InnerException.ToString());
                    throw;
                }

                var makeSOrder = Transform2SOrder(order);
                makeSOrder.TOSString = TransformToTOS(makeSOrder);

                try
                {
                    _dbContext.SOrders.Add(makeSOrder);
                    var saveChanges = _dbContext.SaveChanges();
                }
                catch (Exception e)
                {
                    _logger.Fatal("Save SOrder" + e.InnerException.ToString());
                    throw;
                }

                //Center strike
                var sideType = "c";
                DateTime expry = DateTime.Now;

                List<int> legStrikes = new List<int>();  // To store all strikes of the order
                int centerStrike = 0;
                foreach (var leg in order.Legs)
                {
                    OptionSymbolHelper.ParseOCCSymbol(leg.OptionSymbol, out string _underlying, out DateTime _expiry, out string _side, out int _strike);
                    legStrikes.Add(_strike);
                    if (leg.Side == "sell_to_open")
                    {
                        sideType = _side;
                        centerStrike = _strike;
                        strike = _strike;
                        expry = _expiry;
                    }
                }

                //var thisStrade = LoadStrade(expry, centerStrike, sideType, makeSOrder);

                //var widthsFromCenter = legStrikes.Select(s => Math.Abs(s - strike)).Distinct().ToList();
                //int width = widthsFromCenter.FirstOrDefault(w => w != 0);

                //bool foundExistingStrade = false;
                //foreach (var strade in thisStrade)
                //{
                //    if (strike == strade.Strike)
                //    {
                //        foundExistingStrade = true;
                //        bool foundExistingFly = false;

                //        //if (flyWidth == width)
                //        //{
                //        //    foundExistingFly = true;
                //        //    fly.SOrders.Add(order);
                //        //    break;
                //        //}
                //    }

                //    //if (!foundExistingFly)
                //    //{
                //    //    var newFly = new StradeFly(strike, sideType, expry, order);
                //    //    strade.Flies.Add(newFly);
                //    //}

                //    break;
                //}
            }

            //if (!foundExistingStrade)
            //{
            //    var newStrade = new Strade
            //    {
            //        Strike = strike,
            //        CallPut = sideType,
            //        Expry = expry,
            //        Flies = new List<StradeFly>
            //        {
            //            new StradeFly(strike, sideType, expry, order)
            //        }
            //    };
            //    GlobalStradesList.Add(newStrade);
            //}

            return null;
        }

        private string TransformToTOS(SOrder sOrder)
        {
            string TOSExpry = sOrder.ExpryDate.ToString("dd MMM yy").ToUpper();
            string TOSStrikes = BuildFlyStrikesForTOS(sOrder);
            string tosString = "";

            if (sOrder.Strategy.ToLower().Contains("fly"))
            {
                tosString = "BUY +" + sOrder.NumContracts + " BUTTERFLY SPX 100 " + TOSExpry + " " + TOSStrikes
                     + " " + sOrder.SLegs.First().PutCall //yeahyeah_hardcoding the fly
                     + " @" + sOrder.Price + " LMT";
            }

            return tosString;
        }

        private string BuildFlyStrikesForTOS(SOrder sOrder)
        {
            var sortedStrikes = sOrder.SLegs
                          .Select(leg => leg.Strike)
                          .OrderByDescending(strike => strike)
                          .ToList();

            string formattedStrikes = string.Join("/", sortedStrikes);

            return formattedStrikes;

        }

        private SOrder Transform2SOrder(Order order)
        {

            SOrder sorder = new SOrder();
            
            var openClose = "open";
            var creditDebit = "debit";
            if (order.Legs.FirstOrDefault().Side.EndsWith("close"))
            {
                openClose = "close";
                creditDebit = "credit";
            }

            sorder.brokerId = order.Id;
            sorder.OpenClosed = openClose;
            sorder.CreditDebit = creditDebit;
            sorder.Symbol = "SPX";
            sorder.CreateDate = order.CreateDate;
            sorder.TransactionDate = order.TransactionDate;
            sorder.NumLegs = order.Legs.Count();
            sorder.Price = order.Price;
            sorder.NumContracts = (int)order.ExecQuantity;

            List<SLeg> slegs = new List<SLeg>();
            sorder.SLegs = slegs;

            foreach (var leg in order.Legs)
            {
                SLeg sleg = new SLeg();
                sleg.brokerId = leg.Id;
                //sleg.orderId = 0;

                //sleg.OpenClose = sorder.OpenClosed;

                sleg.QuantityOrdered = leg.Quantity;
                sleg.QuantityExecuted = leg.ExecQuantity;
                sleg.BuySell = leg.Side;

                // OpenClose - Placeholder logic; you'll need to define the exact transformation
                sleg.OpenClose = (leg.Side.EndsWith("open")) ? "Open" : "Close";

                sleg.RatioThisLegToTotalQty = 0;
                sleg.RatioThisLegToTotalPNL = 0;
                sleg.CostBasis = leg.Price * leg.ExecQuantity;
                sleg.ThisFillPrice = leg.LastFillPrice;
                sleg.ThisFillQuantity = leg.LastFillQuantity;
                sleg.RemainingQuantity = leg.RemainingQuantity;
                sleg.OptionSymbol = leg.OptionSymbol;

                OptionSymbolHelper.ParseOCCSymbol(leg.OptionSymbol, out string _underlying,
                    out DateTime _expiry, out string _putCall, out int _strike);

                if (_putCall == "P")
                {
                    _putCall = "PUT";
                }
                else
                {
                    _putCall = "CALL";
                }

                sleg.PutCall = _putCall;
                sleg.Strike = _strike;
                sleg.Underlying = _underlying;
                sleg.Expry = _expiry;

                sorder.SLegs.Add(sleg);
            }

            sorder.Strategy = ParseForStrategy(sorder);

            sorder.ExpryDate = sorder.SLegs.FirstOrDefault().Expry;

            return sorder;
        }

        private string ParseForStrategy(SOrder sorder)
        {
            try
            {

                if (sorder.NumLegs == 3)
                {
                    int highestStrikeLeg = sorder.SLegs.OrderByDescending(leg => leg.Strike).First().Strike;
                    int lowestStrikeLeg = sorder.SLegs.OrderBy(leg => leg.Strike).First().Strike;
                    int midpointStrikeLeg = sorder.SLegs.OrderBy(leg => leg.Strike).Skip(1).First().Strike;

                    bool isDifferenceEqual = Math.Abs(highestStrikeLeg - midpointStrikeLeg) == Math.Abs(lowestStrikeLeg - midpointStrikeLeg);

                    string flyWidth = Math.Abs(highestStrikeLeg - midpointStrikeLeg).ToString();

                    return "BalancedFly" + flyWidth + "W";
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.Fatal("ParseForStrat: " + e.InnerException.ToString());
                throw;
            }


        }

        //private Strade LoadStrade(DateTime expry, int centerStrike, string sideType, SOrder order)
        //{

        //    //try
        //    //{
        //    //    var strade = _dbContext.Strades
        //    //        .Include(s => s.Flies)
        //    //            .ThenInclude(f => f.SOrders) // If you also want to load the Orders
        //    //        .Where(s => s.Expry == expry)
        //    //        .Where(s => s.CallPut == sideType)
        //    //        .Where(s => s.Strike == centerStrike)
        //    //        //.AsSplitQuery()
        //    //        .SingleOrDefault()
        //    //        ;
        //    //    _logger.Information($"LoadStrade: {strade}");
        //    //    if (strade == null)
        //    //    {
        //    //        return CreateStrade(expry, centerStrike, sideType, order);
        //    //    }
        //    //    else
        //    //    {
        //    //        return strade;
        //    //    }
        //    //}
        //    //catch (Exception e)
        //    //{
        //    //    _logger.Fatal($"LoadStrade: {e}");

        //    //    return null;
        //    //}


        //}
        private Strade CreateStrade(DateTime expry, int centerStrike, string sideType, SOrder sOrder)
        {

            try
            {
                string notation = TransformToTOS(sOrder);

                var strade = new Strade
                {
                    Strike = centerStrike,
                    CallPut = sideType,
                    Expry = expry,
                    Flies = new List<StradeFly>
                    {
                        new StradeFly(centerStrike, sideType, expry, sOrder)
                    },
                    TOSNotation = notation,
                    
                };
                _logger.Information($"{nameof(CreateStrade)}: {strade}");

                //_dbContext.Strades.Add(strade);
                //_dbContext.SaveChanges();

                return strade;
            }
            catch (Exception e)
            {
                _logger.Fatal($"{nameof(CreateStrade)}: {e}");
                throw;
            }

        }


        // Load Strade objects from a JSON file into the global list
        public static void LoadStrade()
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
