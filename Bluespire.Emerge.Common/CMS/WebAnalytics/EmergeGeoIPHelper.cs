using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.WebAnalytics;

namespace Bluespire.Emerge.Common.CMS.WebAnalytics
{
    public class EmergeGeoIPHelper
    {

        public static EmergeGeoLocation GetLocationByIp(string dottedQuadIp)
        {
            GeoLocation location = GeoIPHelper.GetLocationByIp(dottedQuadIp);
            EmergeGeoLocation emergeGeoLocation = new EmergeGeoLocation();

            if (null != location)
            {
                emergeGeoLocation.Latitude = location.Latitude;
                emergeGeoLocation.Longitude = location.Longitude;
            }

            return emergeGeoLocation;
        }
    }
}
