using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using CannabisScraper.Scrapers;
using CannabisScraper.Utilities;
using CannabisScraper.Models;
using CannabisScraper.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

string configFilePath = @"C:\work\CannabisScraper\config\VendorConfig.json";

ConfigManager configManager = new ConfigManager(configFilePath);
List<VendorConfig> vendors = configManager.GetVendors();

EdgeOptions options = new EdgeOptions();
options = EdgeOptionsHelper.SetEdgeOptions(options);

IWebDriver driver = new EdgeDriver(options);

List<CannabisData> cannabisData = new List<CannabisData>();
IDataStorage storage = new JsonDataStorage();


Stopwatch stopwatch = Stopwatch.StartNew();
WaitAssistant longWait = new WaitAssistant(driver, TimeSpan.FromSeconds(60));
WaitAssistant tinyWait = new WaitAssistant(driver, TimeSpan.FromSeconds(.25));



Trulieve trulieve = new Trulieve(driver, longWait, tinyWait);

foreach (var vendor in vendors)
{
    try
    {
        // Create the appropriate scraper using the factory
        IScraper scraper = ScraperFactory.CreateScraper(vendor.CompanyName, driver, longWait, tinyWait);

        Console.WriteLine($"Starting scrape for {vendor.CompanyName}...");
        scraper.ExecuteScrape(vendor.Url, cannabisData, vendor.CompanyName, vendor.Location, configFilePath);

        Console.WriteLine($"Scrape completed for {vendor.CompanyName}.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error processing {vendor.CompanyName}: {ex.Message}");
    }
}



stopwatch.Stop();
double durationInSeconds = stopwatch.Elapsed.TotalSeconds;
Console.WriteLine($"Screen scraping completed in {durationInSeconds} seconds. ");

string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
string filePath = $"C:\\work\\CannabisScraper\\output\\cannabisList_{timestamp}.json";
storage.Save(cannabisData, filePath);

Console.ReadLine();

