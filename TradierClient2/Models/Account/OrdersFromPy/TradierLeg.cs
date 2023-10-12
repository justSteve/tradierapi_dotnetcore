using System;
using System.Collections.Generic;
using Tradier.Client.Models.Account;

namespace Tradier.Client.Models.Account.OrdersFromPy
{
    public class TradierLeg : BaseLeg
    {
        public TradierLeg(
            string id,
            string parentId,
            string type,
            string symbol,
            string side,
            int quantity,
            string status,
            string duration,
            float price,
            float avgFillPrice,
            int execQuantity,
            float lastFillPrice,
            int lastFillQuantity,
            int remainingQuantity,
            string createDate,
            string transactionDate,
            //string className,
            string optionSymbol// = null
        ) : base(id, parentId, type, symbol, side, quantity, status, duration, price, avgFillPrice, execQuantity, lastFillPrice, lastFillQuantity, remainingQuantity, createDate, transactionDate, optionSymbol)
        {
        }

        public static TradierLeg FromDict(Dictionary<string, object> data)
        {
            return new TradierLeg(
                data["id"].ToString(),
                data.ContainsKey("parent_id") ? data["parent_id"].ToString() : null,
                data["type"].ToString(),
                data["symbol"].ToString(),
                data["side"].ToString(),
                (int)data["quantity"],
                data["status"].ToString(),
                data["duration"].ToString(),
                Convert.ToSingle(data["price"]),
                Convert.ToSingle(data["avg_fill_price"]),
                Convert.ToInt32(data["exec_quantity"]),
                Convert.ToSingle(data["last_fill_price"]),
                Convert.ToInt32(data["last_fill_quantity"]),
                Convert.ToInt32(data["remaining_quantity"]),
                data["create_date"].ToString(),
                data["transaction_date"].ToString(),
                data.ContainsKey("option_symbol") ? data["option_symbol"].ToString() : null
            );
        }


        public static bool ValidateLeg(Dictionary<string, object> data)
        {
            string[] requiredFields = { "option_symbol", "side" };
            foreach (var field in requiredFields)
            {
                if (!data.ContainsKey(field))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
