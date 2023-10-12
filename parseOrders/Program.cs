using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using SharedHelpers2;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Tradier.Client.Models.Account;
using System.Diagnostics;


class Program
{

    // Declare a static list to hold Strade objects globally
    public static List<Strade> GlobalStradesList;

    static void Main()
    {
        // Initialize the global list from a JSON file at the start of the program
        LoadStradesFromFile();

        string json = File.ReadAllText("C:\\Users\\steve\\OneDrive\\Code\\tradier-api\\orders.15_00.json");
        var ordersRoot = JsonConvert.DeserializeObject<OrdersRootobject>(json);
        var o = 0;
        var c = 0;

        // LINQ query to filter orders
        var filteredOrders = ordersRoot.Orders.Order
            .Where(o => o.Status == "filled" && o.NumLegs == 3)
            .Where(o => o.Legs.All(l => l.Side.Contains("open")))
            .OrderBy(o => o.TransactionDate)
            .ToList();

        // Print or further process the filtered orders
        foreach (var order in filteredOrders)
        {
            var strike = 0;
            var sideType = "c";
            DateTime expiry = DateTime.Now;
            
            foreach (var leg in order.Legs)
            {
                OptionSymbolHelper.ParseOCCSymbol(leg.OptionSymbol, out string underlying, out DateTime _expiryDate, out string _sideType, out int _strike);

                string txTime = leg.TransactionDate.ToString().Split(' ')[1].Replace(":", "_");
                if (leg.Side == "sell_to_open")
                {
                    //center strike
                    strike = _strike;
                    sideType = sideType.ToUpper();
                    expiry = _expiryDate;
                }
            }
            var foundExisting = false;
            foreach (var strade in GlobalStradesList)
            {
                if (strike == strade.Strike)
                {
                    foundExisting = true;
                    strade.Orders.Add(order);
                }
            }
            if (!foundExisting)
            {
                CreateStrade(strike, sideType, expiry, order);
            }
        }

        SaveStradesToFile();
    }

    private static void CreateStrade(int strike, string sideType, DateTime expiry, Order order)
    {
        var strade = new Strade(strike, sideType, expiry, order);
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
}


