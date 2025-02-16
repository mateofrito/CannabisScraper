using CannabisScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannabisScraper.Data
{
    public interface IDataStorage
    {
        void Save(List<CannabisData> data, string target);
    }
}
