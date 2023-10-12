using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tradier.Client.Models.Account;

namespace Tradier.Client.Models.Account.OrdersFromPy
{
    public class BaseOrder
    {
        public int StradeId { get; set; }
        public string Broker { get; set; }
        public int OrderId { get; set; }
        public int NumLegs { get; set; }
        public string Strategy { get; set; }
        public List<BaseLeg> Legs { get; set; }
        ///public string ClassName { get; set; }

        public BaseOrder() { }


        public BaseOrder(
            int stradeId,
            string broker,
            int orderId,
            int numLegs,
            string strategy,
            List<BaseLeg> legs
        )
        {
            if (legs.Count < 1 || legs.Count > 4)
            {
                throw new ArgumentException("An order must have between 1 to 4 legs.");
            }

            if (legs.Count != numLegs)
            {
                throw new ArgumentException("Mismatch between provided number of legs and actual legs.");
            }

            StradeId = stradeId;
            Broker = broker;
            OrderId = orderId;
            NumLegs = numLegs;
            Strategy = strategy;
            Legs = legs;
        }
    }
}
