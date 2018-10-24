using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Bluespire.Emerge.Common.CMS.WebAnalytics
{
    public class EmergeGeoLocation
    {
        /// <summary>
        /// Latitude
        /// </summary>
        public double Latitude
        {
            get;
            set;
        }


        /// <summary>
        /// Longitude
        /// </summary>
        public double Longitude
        {
            get;
            set;
        }

        public EmergeGeoLocation()
        {
            this.Latitude = 0;
            this.Longitude = 0;
        }
        public EmergeGeoLocation(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

       
    }
}
