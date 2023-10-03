using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tradier.Client.Helpers;

public class BalanceRootObject
{
    [JsonProperty("balances")]
    public Balances? Balances { get; set; }
}
public class Balances
{
    [JsonProperty("option_short_value")]
    public float OptionShortValue { get; set; }

    [JsonProperty("total_equity")]
    public float TotalEquity { get; set; }

    [JsonProperty("account_number")]
    public string? AccountNumber { get; set; }
    

    [JsonProperty("account_type")]

    public string? AccountType { get; set; }


    [JsonProperty("close_pl")]
    public float ClosePL { get; set; }

    [JsonProperty("current_requirement")]
    public float CurrentRequirement { get; set; }

    [JsonProperty("equity")]
    public int Equity { get; set; }

    [JsonProperty("long_market_value")]
    public float LongMarketValue { get; set; }

    [JsonProperty("market_value")]
    public float MarketValue { get; set; }

    [JsonProperty("open_pl")]
    public float OpenPL { get; set; }

    [JsonProperty("option_long_value")]
    public float OptionLongValue { get; set; }

    [JsonProperty("option_requirement")]
    public float OptionRequirement { get; set; }

    [JsonProperty("pending_orders_count")]
    public int PendingOrdersCount { get; set; }

    [JsonProperty("short_market_value")]
    public float ShortMarketValue { get; set; }

    [JsonProperty("stock_long_value")]
    public float StockLongValue { get; set; }

    [JsonProperty("total_cash")]
    public float TotalCash { get; set; }

    [JsonProperty("uncleared_funds")]
    public int UnclearedFunds { get; set; }

    [JsonProperty("pending_cash")]
    public float PendingCash { get; set; }

    public int MarginId { get; set; }
    public Margin? Margin { get; set; }

    public int CashId { get; set; }
    public Cash? Cash { get; set; }

    public int PatternDayTraderId { get; set; }
    public PatternDayTrader? PatternDayTrader { get; set; }
}

public class Margin
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int sId { get; set; }

    [JsonProperty("fed_call")]
    public int FedCall { get; set; }

    [JsonProperty("maintenance_call")]
    public int MaintenanceCall { get; set; }

    [JsonProperty("option_buying_power")]
    public float OptionBuyingPower { get; set; }

    [JsonProperty("stock_buying_power")]
    public float StockBuyingPower { get; set; }

    [JsonProperty("stock_short_value")]
    public int StockShortValue { get; set; }

    [JsonProperty("sweep")]
    public int Sweep { get; set; }

}

public class Cash
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int sId { get; set; }
    
    [JsonProperty("cash_available")]
    public float CashAvailable { get; set; }

    [JsonProperty("sweep")]
    public int Sweep { get; set; }

    [JsonProperty("unsettled_funds")]
    public float UnsettledFunds { get; set; }
}

public class PatternDayTrader
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int sId { get; set; }

    [JsonProperty("fed_call")]
    public int FedCall { get; set; }

    [JsonProperty("maintenance_call")]
    public int MaintenanceCall { get; set; }

    [JsonProperty("option_buying_power")]
    public float OptionBuyingPower { get; set; }

    [JsonProperty("stock_buying_power")]
    public float StockBuyingPower { get; set; }

    [JsonProperty("stock_short_value")]
    public int StockShortValue { get; set; }

}
//so 'keeping a root obj just for serialization' is nothing more than keeping this code block in the Balance.cs file?      public class BalanceRootObject
//{
//    [JsonProperty("balances")]

//    public Balances Balances { get; set; }

//}
//Yes, that 's correct. Keeping the BalanceRootObject class serves as a straightforward mapping between the JSON structure and your C# objects. It's a common practice when you're dealing with APIs that nest the actual data you're interested in.

//By having this root object, you can deserialize the entire JSON response into a BalanceRootObject and then easily access its Balances property to get the data you care about. This keeps your code clean, type-safe, and easy to understand, which is particularly useful as your project grows and possibly deals with more complex API responses.

//Here's how you'd typically use it in your GetBalances method:

//csharp
//Copy code
//public async Task<Balances> GetBalances(string accountNumber = null)
//{
//    ... existing code
//    var response = await _requests.GetRequest($"accounts/{accountNumber}/balances");
//    return JsonConvert.DeserializeObject<BalanceRootObject>(response).Balances;
//}
//It adds a level of indirection, but in doing so, it makes your code more maintainable and your data model more accurately reflective of the underlying JSON structure.




