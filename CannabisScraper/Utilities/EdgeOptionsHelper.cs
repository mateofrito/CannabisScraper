using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace CannabisScraper.Utilities
{
    public class EdgeOptionsHelper
    {
        
            public static EdgeOptions SetEdgeOptions(EdgeOptions options)
            {
                // Suppressing logs
                options.SetLoggingPreference(LogType.Browser, LogLevel.Severe);
                options.SetLoggingPreference(LogType.Driver, LogLevel.Severe);
                options.AddArgument("start-maximized");
                //options.AddArgument("headless"); // Run browser in headless mode
                //options.AddArgument("disable-gpu"); // Recommended for compatibility
                //options.AddArgument("--window-size=1920,1080"); // Set a virtual screen size for consistency
                //options.AddArgument("--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");


            return options;
            }
        
    }

}

