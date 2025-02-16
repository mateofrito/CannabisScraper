using CannabisScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannabisScraper.Scrapers
{
    public interface IScraper
    {
        List<CannabisData> ExecuteScrape(string url, List<CannabisData> cannabisData, string companyName, string location, string configFilePathAndName);
    }
}
