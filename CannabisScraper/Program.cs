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


EdgeOptions options = new EdgeOptions();
options = EdgeOptionsHelper.SetEdgeOptions(options);
string[] urls = new string[1]
{
    //"https://www.trulieveoh.com/shop/columbus-westerville-dispensary-menu/"
    "https://dutchie.com/embedded-menu/trulieve-medical-marijuana-dispensary-westerville/products/flower"
};


IWebDriver driver = new EdgeDriver(options);

List<CannabisData> cannabisData = new List<CannabisData>();
List<Dispensary> dispensaries = new List<Dispensary>();

Dispensary dispensaryData = new Dispensary
{
    Url = "https://dutchie.com/embedded-menu/trulieve-medical-marijuana-dispensary-westerville/products/flower",
    Location = "Westerville, Ohio",
    CompanyName = "Trulieve"
};

dispensaries.Add(dispensaryData);


Stopwatch stopwatch = Stopwatch.StartNew();
WaitAssistant longWait = new WaitAssistant(driver, TimeSpan.FromSeconds(60));
WaitAssistant tinyWait = new WaitAssistant(driver, TimeSpan.FromSeconds(.25));



Trulieve trulieve = new Trulieve(driver, longWait, tinyWait);

foreach(var dispensary in dispensaries)
{
    trulieve.ExecuteScrape(dispensary.Url, cannabisData, dispensary.CompanyName, dispensary.Location);
}

//foreach (var url in urls)
//{
//    trulieve.ExecuteScrape(url, cannabisData);

//}


stopwatch.Stop();
double durationInSeconds = stopwatch.Elapsed.TotalSeconds;
Console.WriteLine($"Screen scraping completed in {durationInSeconds} seconds. ");
string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
string json = JsonConvert.SerializeObject(cannabisData, Formatting.Indented);
System.IO.File.WriteAllText($"C:\\work\\cannibusList_"+timestamp+".json", json);
Console.ReadLine();

