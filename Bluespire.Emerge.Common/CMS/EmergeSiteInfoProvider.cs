using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.SiteProvider;

namespace Bluespire.Emerge.Common.CMS
{
    public class EmergeSiteInfoProvider
    {
        public static string CurrentSiteName = GetCurrentSiteName();

        private static string GetCurrentSiteName()
        {
            return SiteContext.CurrentSiteName;
        }
    }
}
