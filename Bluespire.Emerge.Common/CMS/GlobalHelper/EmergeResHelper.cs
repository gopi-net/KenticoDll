using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Helpers;

namespace Bluespire.Emerge.Common.CMS.GlobalHelper
{
    public class EmergeResHelper
    {
        public static string GetString(string stringName)
        {
           return ResHelper.GetString(stringName);
        }
        public static string GetStringFormat(string stringName, params object[] parameters)
        {
            return ResHelper.GetStringFormat(stringName, parameters);
        }



        
    }
}
