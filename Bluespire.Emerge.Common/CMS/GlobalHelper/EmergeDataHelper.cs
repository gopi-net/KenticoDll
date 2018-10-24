using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Helpers;

namespace Bluespire.Emerge.Common.CMS.GlobalHelper
{

    public static class EmergeDataHelper
    {
        public static bool DataSourceIsEmpty(object ds)
        {
            return DataHelper.DataSourceIsEmpty(ds);
        }
       
    }

}
