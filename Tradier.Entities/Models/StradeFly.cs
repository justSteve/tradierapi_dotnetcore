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
        public StradeFly(int strike, string callPut, DateTime expry, SOrder sOrder, int stradeId)
        {
            Strike = strike;
            CallPut = callPut;
            Expry = expry.ToString("dd MMM yy").ToUpper();
            SOrders = new List<SOrder>();  // Initialize the Orders list  
            SOrders.Add(sOrder);
            StradeId= stradeId;
        }

        [Key]
        public int Id { get; set; } // db identifier
        public int StradeId { get; set; } // db identifier of parent
        public int Strike { get; set; }  // of the underlying
        public int QtyContractsOpen { get; set; }
        public int QtyContractsClosed { get; set; }
        public float CostBasis { get; set; }
        public float PNLOpen { get; set; }
        public float PNLClosed { get; set; }
        public string CallPut { get; set; } // 'call' or 'put'
        public string Expry { get; set; } // Expiry date
        public List<SOrder> SOrders { get; set; } // List of Order instances
    }
}