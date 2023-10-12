using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tradier.Client.Models.Account;

namespace Tradier.Client.Models.Account.OrdersFromPy
{
    public class TradierOrder : BaseOrder
    {

        private TradierOrder()
        {
            // Parameterless constructor for EF Core
        }
        public TradierOrder(
            int stradeId,
            string broker,
            int orderId,
            int numLegs,
            string strategy,
            List<BaseLeg> legs
        ) : base(stradeId, "Tradier", orderId, numLegs, strategy, legs)
        {
        }

        public static TradierOrder FromDict(Dictionary<string, object> data)
        {
            List<TradierLeg> legs = new List<TradierLeg>(); // Construct legs here from data

            return new TradierOrder(
                (int)data["stradeId"],
                data["broker"].ToString(),
                (int)data["orderId"],
                (int)data["numLegs"],
                data.ContainsKey("strategy") ? data["strategy"].ToString() : null,
                legs.ConvertAll(leg => (BaseLeg)leg) // Convert list of TradierLeg to list of BaseLeg                
            );
        }

        public void AddLeg(BaseLeg leg)
        {
            Legs.Add(leg);
        }

        //public static bool ValidateOrder(Dictionary<string, object> data)
        //{
        //    string[] requiredFields = { "id", "strategy" };
        //    foreach (var field in requiredFields)
        //    {
        //        if (!data.ContainsKey(field))
        //        {
        //            throw new ArgumentException($"Invalid. Missing required field: {field}");
        //        }
        //    }

        //    // Additional validation for 'butterfly' strategy
        //    // ... (similar to Python code)

        //    return true;
        //}
    }
}
