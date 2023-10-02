using System.Security.Cryptography.Xml;
using System;

namespace Tradier.Client.Helpers
{
    public class Settings
    {
        public const string PRODUCTION_ENDPOINT = "https://api.tradier.com/v1/";
        public const string SANDBOX_ENDPOINT = "https://sandbox.tradier.com/v1/";

        public  string ACCESS_TOKEN_sjh { get; set; }
        public string ACCESS_TOKEN_pjk { get; set; }
        public string ACCOUNT_ID_sjh { get; set; }
        public string ACCOUNT_ID_pjk { get; set; }
        public string ACCESS_TOKEN_SAND_sjh { get; set; }
        public string ACCESS_TOKEN_SAND_pjk { get; set; }
        public string ACCOUNT_ID_SAND_sjh { get; set; }
        public string ACCOUNT_ID_SAND_pjk { get; set; }

    }
}
