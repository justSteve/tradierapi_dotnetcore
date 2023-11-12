using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradier.Entities.Models;

namespace Tradier.Entities.DTOs
{
    public class TransformSLeg2StradeFly
    {
        public TransformSLeg2StradeFly(int strike, string callPut, DateTime expry, SOrder sOrder)
        {
            Strike = strike;
            CallPut = callPut;
            Expry = expry.ToString("dd MMM yy").ToUpper();
            List<SOrder> SOrders = new List<SOrder>();  // Initialize the Orders list
            SOrders.Add(sOrder);
        }

        public int Strike { get; set; }
        public string CallPut { get; set; }
        public string Expry { get; set; }
        public float CostBasis { get; set; }
        public SOrder SOrders { get; set; }
    }
}