using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tradier.Client.Models.Account;

public class StradeFly
{
    // Parameterless constructor for EF Core
    public StradeFly()
    {
        Orders = new List<Order>();  // Initialize the Orders list
    }

    // Your existing constructor
    public StradeFly(int strike, string sideType, DateTime expiry, Order order)
    {
        Strike = strike;
        Type = sideType;
        Expry = expiry;
        Orders = new List<Order>();  // Initialize the Orders list
        Orders.Add(order);
    }

    [Key]
    public int StradeId { get; set; } // db identifier
    public int Strike { get; set; }  // of the underlying
    public float PNL { get; set; }  // cumulative Profit / Loss of children
    public string Type { get; set; } // 'call' or 'put'
    public DateTime Expry { get; set; } // Expiry date
    public List<Order> Orders { get; set; } // List of TradierOrder instances
}
