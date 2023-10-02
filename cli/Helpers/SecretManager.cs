using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;
using Tradier.Client.Helpers;


namespace cli.Helpers
{

    public class SecretManager
    {
        public Settings LoadSecrets()
        {
            try
            {
                string secretsJson = File.ReadAllText("secrets.json");
                Settings apiSettings = JsonConvert.DeserializeObject<Settings>(secretsJson);
                return apiSettings;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading secrets: {ex.Message}");
                return null; // Handle the error gracefully as needed
            }
        }
    }

}
