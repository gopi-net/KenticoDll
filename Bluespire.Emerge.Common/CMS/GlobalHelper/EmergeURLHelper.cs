using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Helpers;
namespace Bluespire.Emerge.Common.CMS.GlobalHelper
{
    public class EmergeURLHelper
    {
        
        public static string UrlScheme = GetUrlSchemee();

        
        private static string GetUrlSchemee()
        {
            return RequestContext.CurrentScheme;
        }

        public static void Redirect(string url)
        {
            URLHelper.Redirect(url);
        }

        //public static string GetHandledUrl(string url)
        //{
        //    return URLHelper.GetHandledUrl(url);
        //}

        public static Uri Url { get { return RequestContext.URL; } }

        public static string CurrentURL { get { return RequestContext.CurrentURL; } set { RequestContext.CurrentURL= value; } }
    }


     
}
