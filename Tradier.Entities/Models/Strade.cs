using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tradier.Client.Models.Account;

public class Strade
{
    public Strade()
    {
        Flies = new List<StradeFly>(); // Initialize the Flies list
    }
    public Strade(int strike, string sideType, DateTime expiry, StradeFly fly)
    {
        this.Strike = strike;
        this.Type = sideType;
        this.Expiry = expiry;
        this.Flies = new List<StradeFly> { fly };
    }

    [Key]
    public int StradeId { get; set; } // db identifier
    public int Strike { get; set; }  // of the underlying
    public string Type { get; set; } // 'call' or 'put'
    public DateTime Expiry { get; set; } // Expiry date
    public List<StradeFly> Flies { get; set; } // List of StradeFly instances
}