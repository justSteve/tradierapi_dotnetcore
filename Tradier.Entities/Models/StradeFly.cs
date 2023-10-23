using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tradier.Entities.Models
{
    public class StradeFly
    {
        // Parameterless constructor for EF Core
        public StradeFly()
        {
            SOrders = new List<SOrder>();  // Initialize the Orders list

        }

        // Your existing constructor
        public StradeFly(int strike, string sideType, DateTime expiry, SOrder order)
        {
            Strike = strike;
            CallPut = sideType;
            Expry = expiry;
            SOrders = new List<SOrder>();  // Initialize the Orders list
            SOrders.Add(order);
        }

        [Key]
        public int StradeFlyId { get; set; } // db identifier
        public int StradeId { get; set; } // db identifier
        public int Strike { get; set; }  // of the underlying
        public int QtyContractsOpen { get; set; }
        public int QtyContractsClosed { get; set; }
        public float PNLOpen { get; set; }
        public float PNLClosed { get; set; }
        public string CallPut { get; set; } // 'call' or 'put'
        public DateTime Expry { get; set; } // Expiry date
        public List<SOrder> SOrders { get; set; } // List of TradierOrder instances
    }
}