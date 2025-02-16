using CannabisScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannabisScraper.Data
{
    public class DatabaseDataStorage : IDataStorage
    {
        public void Save(List<CannabisData> data, string target)
        {
            // Future implementation: Save to a database
            Console.WriteLine("Database saving not implemented yet.");
            throw new NotImplementedException("Database storage is not yet implemented.");
        }
    }
}
