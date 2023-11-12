using System;
using System.Text.RegularExpressions;

namespace SharedHelpers2
{
    public static class OrderConverter
    {
        //public static void ConvertTradierOrderToSOrder(Order)
        //{
        //    ;
        //    string format = "yymmdd";  // Adjust the format as needed

        //    // Regular expression to separate alphabets and digits in the OCC symbol
        //    Regex regex = new Regex("([a-zA-Z]+|[0-9]+)");
        //    try
        //    {             // Split the symbol into parts based on the regular expression
        //        var matches = regex.Matches(optionSymbol);

        //        underlying = matches[0].Value;  // The first match is the underlying asset
        //        bool isValidDate = DateTime.TryParseExact(matches[1].Value, format, null, System.Globalization.DateTimeStyles.None, out expiryDate);

        //        CallPut = matches[2].Value;    // The third match is the option type ('P' or 'C')

        //        string rawStrike = "NotSPX";

        //        if (underlying.StartsWith("SPX"))
        //        {
        //            // Extract the strike price, skipping any leading zeros and keeping the next four digits
        //            rawStrike = matches[3].Value.TrimStart('0').Substring(0, 4);
        //        }

        //        // if it's not SPX will raise exception
        //        strike = int.Parse(rawStrike);  // Convert to an integer for the actual strike price
        //    }
        //    catch (Exception e)
        //    {
        //        underlying = "error";
        //        expiryDate = DateTime.UtcNow;  // The second match is the expiration date
        //        CallPut = e.Message;    // The third match is the option type ('P' or 'C')

        //        strike = 0;
        //    }
        //}

        public static void BuildStratName(string stratName)
        {
            //Examine legs to produce named instance
            //

        }
        public static void AddToStrade(string stratName)
        {
            //
            //
            //Process begins by filtering orders to produce a subset where the following are all true:
            // order:side = buy
            // "status": "filled
            // "num_legs": 3,
            // 1 leg is short by a number that is equal to the cumulative sum of the 2 longs.
            //  
            // on the orders meeting the above conditions:
            // The strike of the short leg becomes the strike of the Strade.
            // does short strike match an existing Strade's strike? No, Create and add.
            //      if creating a new Strade name it by combining strike with call|put

        }
    }
}