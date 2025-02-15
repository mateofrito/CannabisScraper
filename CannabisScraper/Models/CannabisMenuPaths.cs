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

        // Constructor to initialize the row number
        public CannabisMenuPaths(int rowNumber)
        {
            _rowNumber = rowNumber;
        }

        // Base XPath for the row
        public string RootXPath => $"//*[@id=\"main-content\"]/div[2]/div[1]/div[{_rowNumber}]";
       
       

        // Specific XPaths for elements within the row
        public string ProductNameXPath => $"{RootXPath}/div/a/div[2]/div[1]";
        public string BrandNameXPath => $"{RootXPath}/div/a/div[2]/div[2]";
        public string StrainXPath => $"{RootXPath}/div/a/div[2]/div[3]/div[1]";
        public string THCPotencyXPath => $"{RootXPath}/div/a/div[2]/div[3]/div[2]/div";
        public string CBDPotencyXPath => $"{RootXPath}/div/a/div[2]/div[3]/div[2]/div[2]";
        public string GramsXpath => $"{RootXPath}/div/div/div/button";
        public string PriceXpath => $"{RootXPath}/div/div/div/button/b";


    }
}
