using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Tradier.Entities.Models
{
    public class Strade
    {
        public Strade()
        {
            Flies = new List<StradeFly>(); // Initialize the Flies list
        }
        public Strade(int strike, string sideType, DateTime expry, StradeFly fly)
        {
            this.Strike = strike;
            this.CallPut = sideType;
            this.Expry = expry;
            this.Flies = new List<StradeFly> { fly };
        }

        [Key]
        public int Id { get; set; } // db identifier
        //public int StradeId { get; set; }
        public string TOSNotation {get; set;}
        public int Strike { get; set; }  // of the underlying
        public string CallPut { get; set; } // 'call' or 'put'
        public int QtyContractsOpen { get; set; }
        public int QtyContractsClosed { get; set; }
        public float PNLOpen { get; set; }
        public float PNLClosed { get; set; }
        public float MaxProfitYetToGain { get; set; }
        public List<StradeFly> Flies { get; set; } // List of StradeFly instances
        public DateTime Expry { get; set; }
    }
}