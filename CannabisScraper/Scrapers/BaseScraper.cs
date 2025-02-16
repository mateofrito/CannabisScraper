using CannabisScraper.Models;
using CannabisScraper.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannabisScraper.Scrapers
{
    public abstract class BaseScraper : IScraper
    {
        protected readonly IWebDriver Driver;
        protected readonly WaitAssistant LongWait;
        protected readonly WaitAssistant TinyWait;

        protected BaseScraper(IWebDriver driver, WaitAssistant longWait, WaitAssistant tinyWait)
        {
            Driver = driver;
            LongWait = longWait;
            TinyWait = tinyWait;
        }

        public abstract List<CannabisData> ExecuteScrape(
            string url,
            List<CannabisData> cannabisData,
            string companyName,
            string location,
            string configFilePathAndName);

        protected bool IsNextButtonEnabled(string nextButtonXPath)
        {
            var nextButton = Driver.FindElement(By.XPath(nextButtonXPath));
            return nextButton.GetAttribute("disabled") == null;
        }

        protected void GoToNextPage(string nextButtonXPath)
        {
            var nextButton = Driver.FindElement(By.XPath(nextButtonXPath));
            nextButton.Click();
        }
    }
}
