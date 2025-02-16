using CannabisScraper.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannabisScraper.Utilities
{
    public class ConfigManager
    {
        private readonly ConfigRoot _config;

        // Constructor loads the JSON configuration
        public ConfigManager(string configFilePath)
        {
            if (!File.Exists(configFilePath))
            {
                throw new FileNotFoundException($"Configuration file not found at: {configFilePath}");
            }

            string json = File.ReadAllText(configFilePath);
            _config = JsonConvert.DeserializeObject<ConfigRoot>(json)
                     ?? throw new Exception("Failed to deserialize configuration file.");
        }

        // Get all vendors
        public List<VendorConfig> GetVendors()
        {
            return _config.Vendors;
        }

        // Get a specific vendor by company name
        public VendorConfig GetVendor(string companyName)
        {
            return _config.Vendors.Find(v => v.CompanyName.Equals(companyName, StringComparison.OrdinalIgnoreCase))
                   ?? throw new KeyNotFoundException($"Vendor with company name '{companyName}' not found.");
        }
    }
}
