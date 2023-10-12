using System;

namespace Tradier.Client.Models.Account.OrdersFromPy
{
    public class BaseLeg
    {
        //public string Broker { get; set; }
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Type { get; set; }
        public string Symbol { get; set; }
        public string Side { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public string Duration { get; set; }
        public float Price { get; set; }
        public float AvgFillPrice { get; set; }
        public int ExecQuantity { get; set; }
        public float LastFillPrice { get; set; }
        public int LastFillQuantity { get; set; }
        public int RemainingQuantity { get; set; }
        public string CreateDate { get; set; }
        public string TransactionDate { get; set; }
        public string ClassName { get; set; }
        public string OptionSymbol { get; set; }

        public BaseLeg(
            //string broker,
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
            string optionSymbol
        )
        {
            //Broker = broker;
            Id = id;
            ParentId = parentId;
            Type = type;
            Symbol = symbol;
            Side = side;
            Quantity = quantity;
            Status = status;
            Duration = duration;
            Price = price;
            AvgFillPrice = avgFillPrice;
            ExecQuantity = execQuantity;
            LastFillPrice = lastFillPrice;
            LastFillQuantity = lastFillQuantity;
            RemainingQuantity = remainingQuantity;
            CreateDate = createDate;
            TransactionDate = transactionDate;
            OptionSymbol = optionSymbol;
        }
    }
}
