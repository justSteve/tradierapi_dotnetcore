using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradier.Entities.Models;

namespace Tradier.Entities.Helpers
{
    public static class TradierConvertor
    {
        private static SOrder ConvertTradierOrderToSOrder(Order Order)
        {

            return null;
        }
        public static SLeg ConvertTradierLegToSLeg(Leg leg)
        {
            SLeg sLeg = new SLeg();

            // Id
            sLeg.Id = leg.Id;

            // OrderId
            //sLeg.OrderId = leg.OrderId;

            // CreditDebit - Assuming a simplistic transformation based on 'Type' for demonstration
            //sLeg.CreditDebit = (leg.Side.StartsWith("sell")) ? "Credit" : "Debit";

            // BuySell
            sLeg.BuySell = leg.Side;

            // OpenClose - Placeholder logic; you'll need to define the exact transformation
            sLeg.OpenClose = (leg.Side.EndsWith("open")) ? "Open" : "Close";

            // QuantityOrdered
            sLeg.QuantityOrdered = leg.Quantity;

            // QuantityExecuted
            sLeg.QuantityExecuted = leg.ExecQuantity;

            // RatioThisLegToTotalQty - Placeholder logic; this will depend on the total order quantity
            sLeg.RatioThisLegToTotalQty = leg.Quantity /* divided by total order quantity */;

            // RatioThisLegToTotalPNL - Placeholder logic; you'll need to define the exact transformation
            sLeg.RatioThisLegToTotalPNL = /* logic to calculate ratio based on P&L */0;

            // ThisFillPrice
            sLeg.ThisFillPrice = leg.LastFillPrice;

            // ThisFillQuantity
            sLeg.ThisFillQuantity = leg.LastFillQuantity;

            // RemainingQuantity
            sLeg.RemainingQuantity = leg.RemainingQuantity;

            // OptionSymbol
            sLeg.OptionSymbol = leg.OptionSymbol;

            return sLeg;
        }
    }
}