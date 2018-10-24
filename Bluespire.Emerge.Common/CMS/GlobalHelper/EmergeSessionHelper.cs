using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Helpers;

namespace Bluespire.Emerge.Common.CMS.GlobalHelper
{
    public class EmergeSessionHelper
    {
        public static object GetValue(String key)
        {
            return SessionHelper.GetValue(key);
        }

        public static void SetValue(String key, object value)
        {
            SessionHelper.SetValue(key,value);
        }

        public static void Remove(String key)
        {
            SessionHelper.Remove(key);
        }

        
    }
}
