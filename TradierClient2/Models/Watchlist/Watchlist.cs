﻿using Newtonsoft.Json;
using System.Collections.Generic;
using Tradier.Client.Helpers;

namespace Tradier.Client.Models.Watchlist
{

    public class WatchlistsRootobject
    {
        [JsonProperty("watchlists")]

        public Watchlists Watchlists { get; set; }

    }

    public class WatchlistRootobject
    {
        [JsonProperty("watchlist")]

        public Watchlist Watchlist { get; set; }

    }

    public class Watchlists
    {
        [JsonProperty("watchlist")]
        [JsonConverter(typeof(SingleOrArrayConverter<Watchlist>))]

        public List<Watchlist> Watchlist { get; set; }

    }

    public class Watchlist
    {
        [JsonProperty("name")]

        public string Name { get; set; }


        [JsonProperty("id")]

        public string Id { get; set; }


        [JsonProperty("public_id")]

        public string PublicId { get; set; }

        
        [JsonProperty("items")]

        public Items Items { get; set; }

    }

    public class Items
    {
        [JsonProperty("item")]
        [JsonConverter(typeof(SingleOrArrayConverter<Item>))]

        public List<Item> Item { get; set; }

    }

    public class Item
    {
        [JsonProperty("symbol")]

        public string Symbol { get; set; }

        
        [JsonProperty("id")]

        public string Id { get; set; }

    }
}