using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tradier.Entities.Models
{
    public class SOrdersRootobject
    {
        public SOrders SOrders { get; set; }
    }

    public class SOrders
    {
        public List<SOrder> SLeg { get; set; } // Renamed to OrderList for clarity

    }

    public class SOrder
    {
        [Key]
        public int Id { get; set; }
        public int brokerId { get; set; }

        public int? StradeId { get; set; } // Nullable foreign key to StradeFly

        public string CreditDebit { get; set; } // Credit|Debit Parse this from the 
        public string Symbol { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Expry { get; set; }
        public DateTime TransactionDate { get; set; }
        public int NumLegs { get; set; }
        public int NumContracts { get; set; }
        public string Strategy { get; set; }
        public string OpenClosed { get; set; }
        public float Price { get; set; }
        public List<SLeg> SLegs { get; set; }
        public string TOSString { get; set; }
    }


    public class SLeg
    {
        // Source: Pure TradierAPI // Correct. there is no transforming. It's the Id fields as returned by api
        // Intent: To uniquely identify each leg within an order.
        // Code To Transform: none
        public int brokerId { get; set; }
        public int brokerOrderId { get; set; }


        [Key]
        public int Id { get; set; }
        
        // Source: Pure TradierAPI
        // Intent: To associate the leg with a specific order.
        // Code To Transform: Parent Id field becomes = leg.OrderId;
        public int orderId { get; set; }
        
        // Source: Derived from TradierAPI
        // Intent: To indicate if the leg is a credit or debit transaction.
        // Code To Transform: sLeg.CreditDebit = (leg.Side start with "sell") ? "Credit" : "Debit";
        //public string CreditDebit { get; set; }

        // Source: Pure TradierAPI
        // Intent: To indicate the direction of the leg transaction.
        // Code To Transform: sLeg.BuySell = leg.Side;
        public string BuySell { get; set; }

        // Source: Derived from TradierAPI
        // Intent: To indicate whether the leg is opening or closing a position.
        // Code To Transform: Requires further information. // dirived from suffice of API's leg.Side
        public string OpenClose { get; set; }

        // Source: Derived from TradierAPI
        // Intent: To specify the quantity ordered for this leg.
        // Code To Transform: sLeg.QuantityOrdered = leg.Quantity;
        public float QuantityOrdered { get; set; }

        // Source: Pure TradierAPI
        // Intent: To specify the cumulative quantity executed for this leg.
        // Code To Transform: sLeg.QuantityExecuted = leg.ExecQuantity;
        public float QuantityExecuted { get; set; }

        // Source: Derived from TradierAPI
        // Intent: To represent the proportion of this leg's quantity relative to the total order quantity.
        // Code To Transform: sLeg.RatioThisLegToTotalQty = leg.Quantity / order.TotalQuantity;
        public float RatioThisLegToTotalQty { get; set; }

        // Source: Derived from TradierAPI
        // Intent: To represent the proportion of this leg's profit or loss relative to the total order's P&L.
        // Code To Transform: Requires more context.
        public float RatioThisLegToTotalPNL { get; set; }

        // Source: Combined TradierAPI
        // Intent: Total cost for this leg, derived from the execution price and quantity.
        // Code To Transform: sLeg.CostBasis = leg.Price * leg.ExecQuantity;
        public float CostBasis { get; set; }

        // Source: Pure TradierAPI
        // Intent: To capture the price at which the most recent transaction for this leg was executed.
        // Code To Transform: sLeg.ThisFillPrice = leg.LastFillPrice;
        public float ThisFillPrice { get; set; }

        // Source: Pure TradierAPI
        // Intent: Quantity for the most recent fill for this leg.
        // Code To Transform: sLeg.ThisFillQuantity = leg.LastFillQuantity;
        public float ThisFillQuantity { get; set; }

        // Source: Pure TradierAPI
        // Intent: Quantity that is yet to be executed for this leg.
        // Code To Transform: sLeg.RemainingQuantity = leg.RemainingQuantity;
        public float RemainingQuantity { get; set; }

        // Source: Pure TradierAPI
        // Intent: Identifier for the option associated with this leg.
        // Code To Transform: sLeg.OptionSymbol = leg.OptionSymbol;
        public string OptionSymbol { get; set; }

        public string Underlying { get; set; }
        public int Strike { get; set; }
        public string CallPut { get; set; }
        public DateTime Expry { get; set; }
    }

}
