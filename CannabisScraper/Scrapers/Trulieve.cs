using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using CannabisScraper.Models;
using CannabisScraper.Utilities;

namespace CannabisScraper.Scrapers
{
    public class Trulieve : BaseScraper
    {
        private readonly ConfigManager configManager;

        public Trulieve(IWebDriver driver, WaitAssistant longWait, WaitAssistant tinyWait, string configFilePath)
            : base(driver, longWait, tinyWait)
        {
            configManager = new ConfigManager(configFilePath);
        }

        public override List<CannabisData> ExecuteScrape(string url, List<CannabisData> cannabisData, string companyName, string location, string configFilePathAndName)
        {
            string categoryXPath = "//*[@id=\"main-content\"]/div[1]/header/div/div[1]/div/h1";
            string nextButtonXPath = "//*[@id=\"main-content\"]/div[2]/div[2]/nav/button[2]";
            string firstRowXpath = "//*[@id=\"main-content\"]/div[2]/div[1]/div[1]";
            int pageCount = 1;

            // Navigate to the initial URL
            Driver.Navigate().GoToUrl(url);

            if (!LongWait.IsElementPresentByXpath(categoryXPath))
            {
                Console.WriteLine("Could not load page.");
                return cannabisData;
            }

            // Scrape by category
            IWebElement categoryElement = Driver.FindElement(By.XPath(categoryXPath));
            string categoryName = categoryElement.Text;
            Console.WriteLine($"Beginning scrape for {companyName} - {categoryName} menu.");

            while (true)
            {
                Console.WriteLine($"Scraping page {pageCount} of {categoryName}...");
                ScrapeCurrentPage(cannabisData, categoryName, configFilePathAndName, companyName, location);

                if (!IsNextButtonEnabled(nextButtonXPath))
                {
                    Console.WriteLine("All pages have been scraped.");
                    break;
                }

                GoToNextPage(nextButtonXPath);
                pageCount++;

                Console.WriteLine($"Page {pageCount - 1} scraped, moving on to {pageCount}");
                Thread.Sleep(500);
                LongWait.WaitForElementToBeVisible(By.XPath(firstRowXpath));
            }

            return cannabisData;
        }

        private void ScrapeCurrentPage(List<CannabisData> cannabisData, string categoryName, string configFilePathAndName, string companyName, string location)
        {
            string allRowsXPath = "//*[@id=\"main-content\"]/div[2]/div[1]/div";
            var rowElements = Driver.FindElements(By.XPath(allRowsXPath));
            int maxRowCount = rowElements.Count();
           
            for(int rowNumber = 1; rowNumber <= maxRowCount; rowNumber++)
            {
                try
                {
                    CannabisMenuPaths xpaths = new CannabisMenuPaths(rowNumber, new ConfigManager(configFilePathAndName).GetVendor(companyName).XPaths);

                    IWebElement rowElement = Driver.FindElement(By.XPath(allRowsXPath));

                    LongWait.WaitForElementToBeVisible(By.XPath(xpaths.ProductNameXPath));
                    // Extract data for each row using the XPaths
                    string productName = rowElement.FindElement(By.XPath(xpaths.ProductNameXPath)).Text;
                    string brandName = rowElement.FindElement(By.XPath(xpaths.BrandNameXPath)).Text;
                    string strain = rowElement.FindElement(By.XPath(xpaths.StrainXPath)).Text;
                    string thcPotency = rowElement.FindElement(By.XPath(xpaths.THCPotencyXPath)).Text;

                    string cbdPotency = TinyWait.IsElementPresentByXpath(xpaths.CBDPotencyXPath)
                        ? rowElement.FindElement(By.XPath(xpaths.CBDPotencyXPath)).Text
                        : "N/A";

                    string grams = rowElement.FindElement(By.XPath(xpaths.GramsXPath)).Text;
                    string price = rowElement.FindElement(By.XPath(xpaths.PriceXPath)).Text;

                    // Add the scraped data to the list
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
                    Console.WriteLine($"Error scraping row {rowNumber}: {ex.Message}");
                }

               
            }
        }
    }
    
}
