using CannabisScraper.Scrapers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannabisScraper.Utilities
{
    public static class ScraperFactory
    {
        public static IScraper CreateScraper(string companyName, IWebDriver driver, WaitAssistant longWait, WaitAssistant tinyWait, string configFilePath)
        {
            return companyName switch
            {
                "Trulieve-Westerville" => new Trulieve(driver, longWait, tinyWait, configFilePath),
                // Add more vendors here as you create new scrapers
                _ => throw new ArgumentException($"No scraper available for company: {companyName}")
            };
        }
    }
}
