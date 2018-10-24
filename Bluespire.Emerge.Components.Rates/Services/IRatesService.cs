using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Components.Rates.Services
{
    public interface IRatesService
    {
        void UpdateCustomTable(string serverFile, string sheetName);
        DataTable GetRatesList(string whereCondition); 
    }
}
