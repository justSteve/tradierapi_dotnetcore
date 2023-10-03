using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tradier.Client.Models.Account
{

    public class HistoryRootobject
    {
        [JsonProperty("history")]

        public History History { get; set; }

    }

    public class History
    {
        [JsonProperty("event")]

        public List<Event> Event { get; set; }
        [Key]
        [JsonProperty("id")]
        public Int32 id { get; set; }


    }
    [Owned]
    public class Event
    {
        [JsonProperty("amount")]
        public float Amount { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("type")]

        public string Type { get; set; }


        [JsonProperty("trade")]

        public Trade Trade { get; set; }


        [JsonProperty("adjustment")]

        public Adjustment Adjustment { get; set; }


        [JsonProperty("option")]

        public Option Option { get; set; }


        [JsonProperty("journal")]

        public Journal Journal { get; set; }

    }

    [Owned]
    public class Trade
    {
        [JsonProperty("commission")]
        public float Commission { get; set; }

        [JsonProperty("description")]

        public string Description { get; set; }


        [JsonProperty("price")]
        public float Price { get; set; }

        [JsonProperty("quantity")]
        public float Quantity { get; set; }

        [JsonProperty("symbol")]

        public string Symbol { get; set; }


        [JsonProperty("trade_type")]

        public string TradeType { get; set; }

    }

    [Owned]
    public class Adjustment
    {
        [JsonProperty("description")]

        public string Description { get; set; }

        
        [JsonProperty("quantity")]
        public float Quantity { get; set; }
    }

    [Owned]
    public class Option
    {
        [JsonProperty("option_type")]

        public string OptionType { get; set; }


        [JsonProperty("description")]

        public string Description { get; set; }


        [JsonProperty("quantity")]
        public float Quantity { get; set; }
    }

    [Owned]
    public class Journal
    {
        [JsonProperty("description")]

        public string Description { get; set; }

        
        [JsonProperty("quantity")]
        public float Quantity { get; set; }
    }

}
