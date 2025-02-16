using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannabisScraper.Models
{
    public class CannabisMenuPaths
    {
        private readonly int _rowNumber;
        private readonly Dictionary<string, string> _xpaths;

        // Constructor to initialize the row number and XPaths
        public CannabisMenuPaths(int rowNumber, Dictionary<string, string> xpaths)
        {
            _rowNumber = rowNumber;
            _xpaths = xpaths;
        }

        // Base XPath for the row, loaded dynamically from the configuration
        public string RootXPath => _xpaths["rootPath"]+"["+_rowNumber+"]";

        // Specific XPaths for elements within the row, derived from the configuration
        public string ProductNameXPath => $"{RootXPath}{_xpaths["productName"]}";
        public string BrandNameXPath => $"{RootXPath}{_xpaths["brandName"]}";
        public string StrainXPath => $"{RootXPath}{_xpaths["strain"]}";
        public string THCPotencyXPath => $"{RootXPath}{_xpaths["thcPotency"]}";
        public string CBDPotencyXPath => $"{RootXPath}{_xpaths["cbdPotency"]}";
        public string GramsXPath => $"{RootXPath}{_xpaths["grams"]}";
        public string PriceXPath => $"{RootXPath}{_xpaths["price"]}";
    }
    //public class CannabisMenuPaths
    //{
    //    private readonly int _rowNumber;

    //    // Constructor to initialize the row number
    //    public CannabisMenuPaths(int rowNumber)
    //    {
    //        _rowNumber = rowNumber;
    //    }

    //    // Base XPath for the row
    //    public string RootXPath => $"//*[@id=\"main-content\"]/div[2]/div[1]/div[{_rowNumber}]";



    //    // Specific XPaths for elements within the row
    //    public string ProductNameXPath => $"{RootXPath}/div/a/div[2]/div[1]";
    //    public string BrandNameXPath => $"{RootXPath}/div/a/div[2]/div[2]";
    //    public string StrainXPath => $"{RootXPath}/div/a/div[2]/div[3]/div[1]";
    //    public string THCPotencyXPath => $"{RootXPath}/div/a/div[2]/div[3]/div[2]/div";
    //    public string CBDPotencyXPath => $"{RootXPath}/div/a/div[2]/div[3]/div[2]/div[2]";
    //    public string GramsXpath => $"{RootXPath}/div/div/div/button";
    //    public string PriceXpath => $"{RootXPath}/div/div/div/button/b";


    //}
}
