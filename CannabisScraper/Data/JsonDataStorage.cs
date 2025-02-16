using CannabisScraper.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannabisScraper.Data
{
    public class JsonDataStorage : IDataStorage
    {
        public void Save(List<CannabisData> data, string target)
        {
            try
            {
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(target, json);
                Console.WriteLine($"Data saved to JSON file: {target}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to JSON file: {ex.Message}");
                throw;
            }
        }
    }
}
