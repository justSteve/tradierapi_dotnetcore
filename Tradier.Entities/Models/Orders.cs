﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Tradier.Client.Helpers;
using System.ComponentModel.DataAnnotations;


namespace Tradier.Entities.Models
{

    public class OrdersRootobject
    {
        [JsonProperty("orders")]

        public Orders Orders { get; set; }

    }

    public class Orders
    {
        [JsonProperty("order")]
        [JsonConverter(typeof(SingleOrArrayConverter<Order>))]

        public List<Order> Order { get; set; }

    }

    public class Order
    {
        [Key]
        public int DatabaseId { get; set; }  // SQL Server-generated primary key

        [JsonProperty("id")]
        public int Id { get; set; } // 

        [JsonProperty("type")]

        public string Type { get; set; }


        [JsonProperty("symbol")]
        public string Symbol { get; set; }


        [JsonProperty("side")]

        public string Side { get; set; }


        [JsonProperty("quantity")]
        public float Quantity { get; set; }

        [JsonProperty("status")]

        public string Status { get; set; }


        [JsonProperty("duration")]

        public string Duration { get; set; }


        [JsonProperty("price")]
        public float Price { get; set; }

        [JsonProperty("avg_fill_price")]
        public float AvgFillPrice { get; set; }

        [JsonProperty("exec_quantity")]
        public float ExecQuantity { get; set; }

        [JsonProperty("last_fill_price")]
        public float LastFillPrice { get; set; }

        [JsonProperty("last_fill_quantity")]
        public float LastFillQuantity { get; set; }

        [JsonProperty("remaining_quantity")]
        public float RemainingQuantity { get; set; }

        [JsonProperty("create_date")]
        public DateTime CreateDate { get; set; }

        [JsonProperty("transaction_date")]
        public DateTime TransactionDate { get; set; }

        [JsonProperty("_class")]

        public string Class { get; set; }


        [JsonProperty("option_symbol")]

        public string OptionSymbol { get; set; }


        [JsonProperty("num_legs")]
        public int NumLegs { get; set; }

        [JsonProperty("strategy")]

        public string Strategy { get; set; }


        [JsonProperty("leg")]
        public List<Leg> Legs { get; set; }  // Renamed to "Legs" for clarity

    }

    public class Leg
    {
        [Key]
        public int DatabaseId { get; set; }  // SQL Server-generated primary key

        public int DatabaseOrderId { get; set; }  // Foreign key referencing Order


        [JsonProperty("id")]
        public int Id { get; set; }


        [JsonProperty("type")]

        public string Type { get; set; }


        [JsonProperty("symbol")]

        public string Symbol { get; set; }


        [JsonProperty("side")]

        public string Side { get; set; }


        [JsonProperty("quantity")]
        public float Quantity { get; set; }

        [JsonProperty("status")]

        public string Status { get; set; }


        [JsonProperty("duration")]

        public string Duration { get; set; }


        [JsonProperty("price")]
        public float Price { get; set; }

        [JsonProperty("avg_fill_price")]
        public float AvgFillPrice { get; set; }

        [JsonProperty("exec_quantity")]
        public float ExecQuantity { get; set; }

        [JsonProperty("last_fill_price")]
        public float LastFillPrice { get; set; }

        [JsonProperty("last_fill_quantity")]
        public float LastFillQuantity { get; set; }

        [JsonProperty("remaining_quantity")]
        public float RemainingQuantity { get; set; }

        [JsonProperty("create_date")]
        public DateTime CreateDate { get; set; }

        [JsonProperty("transaction_date")]
        public DateTime TransactionDate { get; set; }

        [JsonProperty("_class")]

        public string Class { get; set; }


        [JsonProperty("option_symbol")]

        public string OptionSymbol { get; set; }

    }

}
