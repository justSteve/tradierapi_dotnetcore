// A Stade is a container for a list of TradierOrder instances based on the strike + call or put of the fly's short leg.
// 
 

using System.Collections.Generic;
using Tradier.Client.Models.Account;

public class Strade
{
    private string sideType;
    private string expiry;

    public Strade(int strike, string sideType, DateTime expiry, Order order)
    {
        Strike = strike;
        Type = sideType;
        Expry = expiry;
        Orders = new List<Order>();  // Initialize the Orders list
        Orders.Add(order);

    }

    public int StradeId { get; set; } // db identifier
    public int Strike { get; set; }  // of the underlying
    public float PNL { get; set; }  // cumulative Profit / Loss of children
    public string Type { get; set; } // 'call' or 'put'
    public DateTime Expry { get; set; } // 
    public List<Order> Orders { get; set; } // List of TradierOrder instances
}
