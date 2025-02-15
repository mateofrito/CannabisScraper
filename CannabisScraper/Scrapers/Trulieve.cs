using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using CannabisScraper.Scrapers;
using CannabisScraper.Utilities;
using CannabisScraper.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace CannabisScraper.Scrapers
{
    public class Trulieve
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

        public List<CannabisData> ExecuteScrape(string url, List<CannabisData> cannabisData, string companyName, string location)
        {
          

            string categoryXpath = "//*[@id=\"main-content\"]/div[1]/header/div/div[1]/div/h1";

            driver.Navigate().GoToUrl(url);
                if (longWait.IsElementPresentByXpath(categoryXpath))
                {
                    IWebElement categoryElement = driver.FindElement(By.XPath(categoryXpath));
                    string categoryName = categoryElement.Text;

                    Console.WriteLine($"Begining scrape for the {categoryName} menu.");

                    string allRowsXPath = "//*[@id=\"main-content\"]/div[2]/div[1]/div";

                    // Count the rows
                    var rowElements = driver.FindElements(By.XPath(allRowsXPath));
                    int maxRowCount = rowElements.Count;

                    Console.WriteLine($"Found {maxRowCount} rows on the {categoryName} page.");

                    // Loop through rows
                    for (int rowNumber = 1; rowNumber <= maxRowCount; rowNumber++)
                    {
                        // Initialize paths for the current row
                        var paths = new CannabisMenuPaths(rowNumber);

                        try
                        {
                            // Access product name
                            IWebElement productNameElement = driver.FindElement(By.XPath(paths.ProductNameXPath));
                            string productName = productNameElement.Text;

                            // Access brand name
                            IWebElement brandNameElement = driver.FindElement(By.XPath(paths.BrandNameXPath));
                            string brandName = brandNameElement.Text;

                            // Access strain
                            IWebElement strainElement = driver.FindElement(By.XPath(paths.StrainXPath));
                            string strain = strainElement.Text;

                            // Access THC potency
                            IWebElement thcPotencyElement = driver.FindElement(By.XPath(paths.THCPotencyXPath));
                            string thcPotency = thcPotencyElement.Text;


                            // Access CBD potency (check if exists)
                            string cbdPotency = string.Empty;
                            if (tinyWait.IsElementPresentByXpath(paths.CBDPotencyXPath))
                            {
                                IWebElement cbdPotencyElement = driver.FindElement(By.XPath(paths.CBDPotencyXPath));
                                cbdPotency = cbdPotencyElement.Text;
                            }
                            else
                            {
                                cbdPotency = "N/A";
                            }

                            IWebElement gramsElement = driver.FindElement(By.XPath(paths.GramsXpath));
                            string grams = gramsElement.Text;

                            IWebElement priceElement = driver.FindElement(By.XPath(paths.PriceXpath));
                            string price = priceElement.Text;


                            // Example: Print or store the extracted data
                            Console.WriteLine($"Row {rowNumber}:");
                            Console.WriteLine($"Product Name: {productName}");
                            Console.WriteLine($"Brand Name: {brandName}");
                            Console.WriteLine($"Strain: {strain}");
                            Console.WriteLine($"THC Potency: {thcPotency}");
                            Console.WriteLine($"CBD Potency: {cbdPotency}");
                            Console.WriteLine($"Price: {price} ");

                        CannabisData data = new CannabisData
                        {
                            ProductName = productName,
                            BrandName = brandName,
                            ProductType = categoryName.Replace("All", "").Trim(),
                            ThcPotency = thcPotency,
                            CbdPotency = cbdPotency,
                            Price = price,
                            Strain = strain,
                            StoreName = $"{companyName} - {location}" //again, dynamic

                        };

                        cannabisData.Add(data);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error processing row {rowNumber}: {ex.Message}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Could not load page");
                }

            return cannabisData;

        }
    }
}
