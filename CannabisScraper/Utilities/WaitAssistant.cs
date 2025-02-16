using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace CannabisScraper.Utilities
{
    public class WaitAssistant
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        // Constructor to initialize driver and WebDriverWait
        public WaitAssistant(IWebDriver driver, TimeSpan timeout)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, timeout);
        }
        public static void WaitForPageToReRender(IWebDriver driver, IWebElement pageElement, string currentFieldValue, int currentPage)
        {
            int retryCounter = 0;
            if (currentPage != 1)
            {
                if (pageElement.Text == currentFieldValue)
                {
                    while (retryCounter < 3)
                    {
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                        retryCounter++;
                    }
                }
            }
        }


        public bool IsElementPresentByXpath(string xpath)
        {
            By by = By.XPath(xpath);

            try
            {
                _wait.Until(ExpectedConditions.ElementExists(by));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public IWebElement WaitForElementToBeClickableByXpath(string xpath)
        {
            By by = By.XPath(xpath);
            return _wait.Until(ExpectedConditions.ElementToBeClickable(by));
        }

        public void WaitForElementToBeVisible(By by)
        {
            
            _wait.Until(ExpectedConditions.ElementIsVisible(by));
        }
    }
}
