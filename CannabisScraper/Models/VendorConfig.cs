using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannabisScraper.Models
{
    public class VendorConfig
    {
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public string Url { get; set; }
        public Dictionary<string, string> UrlCategories { get; set; }
        public Dictionary<string, string> XPaths { get; set; }
    }
}
