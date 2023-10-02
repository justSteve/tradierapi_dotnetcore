using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using Tradier.Client.Helpers;
using Tradier.Client.Models.Account;
using System.ComponentModel.DataAnnotations;


namespace Tradier.Client.Models.Account
{
    public class ProfileRootObject
    {
        [JsonProperty("profile")]

        public Profile Profile { get; set; }

    }

    public class Profile
    {
        [Key]
        [JsonProperty("id")]

        public string Id { get; set; }


        [JsonProperty("name")]

        public string Name { get; set; }


        [JsonProperty("account")]
        [JsonConverter(typeof(SingleOrArrayConverter<Account>))]

        public List<Account> Account { get; set; }

    }

    public class Account
    {
        [JsonProperty("account_number")]

        public string AccountNumber { get; set; }


        [JsonProperty("classification")]

        public string Classification { get; set; }


        [JsonProperty("date_created")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("day_trader")]
        public bool DayTrader { get; set; }

        [JsonProperty("option_level")]
        public int OptionLevel { get; set; }

        [JsonProperty("status")]

        public string Status { get; set; }


        [JsonProperty("type")]

        public string Type { get; set; }


        [JsonProperty("last_update_date")]
        public DateTime LastUpdateDate { get; set; }
    }
}
//https://documentation.tradier.com/brokerage-api/user/get-profile
// Version 4.6.2.0    
//using System;
//using System.Net;  
//using System.IO;
//using System.Text;

//public class MainClass
//{
//    public static void Main(string[] args)
//    {
//        var request = (HttpWebRequest)WebRequest.Create("https://api.tradier.com/v1/user/profile");
//        request.Method = "GET";
//        request.Headers["Authorization"] = "Bearer <TOKEN>";
//        request.Accept = "application/json";

//        var response = (HttpWebResponse)request.GetResponse();

//        Console.WriteLine(response.StatusCode);
//        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
//        Console.WriteLine(responseString);
//    }

//Profile
//Field	Description
//account_number	Account number
//classification	Class of account. One of: individual, entity, individual, joint_survivor, joint_survivor, traditional_ira, roth_ira, rollover_ira, entity, sep_ira
//date_created	Date account was created
//day_trader	Marked as day trader
//option_level	Account option level (1-6)
//status Current status of the account: One of: active, closed
//type	Type of the account. One of: cash, margin
//last_update_date	Date account was last updated

//{
//    "profile": {
//        "account": [
//          {
//            "account_number": "VA000001",
//        "classification": "individual",
//        "date_created": "2016-08-01T21:08:55.000Z",
//        "day_trader": false,
//        "option_level": 6,
//        "status": "active",
//        "type": "margin",
//        "last_update_date": "2016-08-01T21:08:55.000Z"
//          },
//      {
//            "account_number": "VA000002",
//        "classification": "traditional_ira",
//        "date_created": "2016-08-05T17:24:34.000Z",
//        "day_trader": false,
//        "option_level": 3,
//        "status": "active",
//        "type": "margin",
//        "last_update_date": "2016-08-05T17:24:34.000Z"
//      },
//      {
//            "account_number": "VA000003",
//        "classification": "rollover_ira",
//        "date_created": "2016-08-01T21:08:56.000Z",
//        "day_trader": false,
//        "option_level": 2,
//        "status": "active",
//        "type": "cash",
//        "last_update_date": "2016-08-01T21:08:56.000Z"
//      }
//    ],
//    "id": "id-gcostanza",
//    "name": "George Costanza"
//    }
//}
//