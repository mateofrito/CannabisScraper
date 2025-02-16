using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using CannabisScraper.Models;
using CannabisScraper.Utilities;

namespace CannabisScraper.Scrapers
{
    public class Trulieve : IScraper
    {
        private readonly IWebDriver driver;
        private readonly WaitAssistant longWait;
        private readonly WaitAssistant tinyWait;

        public Trulieve(IWebDriver driver, WaitAssistant longWait, WaitAssistant tinyWait)
        {
            this.driver = driver;
            this.longWait = longWait;
            this.tinyWait = tinyWait;
        }

        public List<CannabisData> ExecuteScrape(string url, List<CannabisData> cannabisData, string companyName, string location, string configFilePathAndName)
        {
            driver.Navigate().GoToUrl(url);

            string categoryXpath = "//*[@id=\"main-content\"]/div[1]/header/div/div[1]/div/h1";
            if (!longWait.IsElementPresentByXpath(categoryXpath))
            {
                Console.WriteLine("Could not load page.");
                return cannabisData;
            }

            IWebElement categoryElement = driver.FindElement(By.XPath(categoryXpath));
            string categoryName = categoryElement.Text;
            Console.WriteLine($"Beginning scrape for the {categoryName} menu.");

            string allRowsXPath = "//*[@id=\"main-content\"]/div[2]/div[1]/div";
            var rowElements = driver.FindElements(By.XPath(allRowsXPath));
            int maxRowCount = rowElements.Count;
            Console.WriteLine($"Found {maxRowCount} rows on the {categoryName} page.");

            // Loop through each row
            for (int rowNumber = 1; rowNumber <= maxRowCount; rowNumber++)
            {
                try
                {
                    // Create CannabisMenuPaths with dynamic XPaths for the current row
                    var paths = new CannabisMenuPaths(rowNumber, new ConfigManager(configFilePathAndName).GetVendor(companyName).XPaths);

                    // Scrape data for this row
                    string productName = driver.FindElement(By.XPath(paths.ProductNameXPath)).Text;
                    string brandName = driver.FindElement(By.XPath(paths.BrandNameXPath)).Text;
                    string strain = driver.FindElement(By.XPath(paths.StrainXPath)).Text;
                    string thcPotency = driver.FindElement(By.XPath(paths.THCPotencyXPath)).Text;

                    string cbdPotency = tinyWait.IsElementPresentByXpath(paths.CBDPotencyXPath)
                        ? driver.FindElement(By.XPath(paths.CBDPotencyXPath)).Text
                        : "N/A";

                    string grams = driver.FindElement(By.XPath(paths.GramsXPath)).Text;
                    string price = driver.FindElement(By.XPath(paths.PriceXPath)).Text;

                    // Add data to the list
                    cannabisData.Add(new CannabisData
                    {
                        ProductName = productName,
                        BrandName = brandName,
                        ProductType = categoryName.Replace("All", "").Trim(),
                        ThcPotency = thcPotency,
                        CbdPotency = cbdPotency,
                        Price = price,
                        Strain = strain,
                        StoreName = $"{companyName} - {location}"
                    });

                    Console.WriteLine($"Scraped row {rowNumber}: {productName}, {brandName}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing row {rowNumber}: {ex.Message}");
                }
            }

            return cannabisData;
        }
    }
}
