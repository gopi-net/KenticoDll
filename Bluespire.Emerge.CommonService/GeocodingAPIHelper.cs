using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Common.CMS.WebAnalytics;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Common.Logging;
using CMS.WebAnalytics;

namespace Bluespire.Emerge.CommonService
{
    public class GeocodingAPIHelper
    {

        public static EmergeGeoLocation GetGeoLocation(string address)
        {
            // Use the Google Geocoding service to get information about the user-entered address
            EmergeGeoLocation location = new EmergeGeoLocation();
            try
            {

                var url = String.Format("{0}://maps.google.com/maps/api/geocode/xml?address={1}&sensor=false", EmergeURLHelper.UrlScheme, HttpContext.Current.Server.UrlEncode(address));
                // Load the XML into an XElement object (whee, LINQ to XML!)

                WebRequest webrequest = WebRequest.Create(url);
                //WebProxy proxy = new WebProxy();
                //proxy.Credentials = CredentialCache.DefaultCredentials;
                //webrequest.Proxy = proxy;

                var results = XElement.Load(new XmlTextReader(webrequest.GetResponse().GetResponseStream()));
                
                var status = results.Element("status").Value;

                if (status != "OK" && status != "ZERO_RESULTS")
                {
                    EmergeLogWriter.WriteError("GetGeoLocation: " + address, EventCode.EMERGE_GET, "Result: " + results.ToString() + " Status:" + status);
                    throw new GeocodingServiceException("There was an error with Google's Geocoding Service");
                }

                if (results != null)
                {
                    location = new EmergeGeoLocation(Convert.ToDouble(results.Element("result").Element("geometry").Element("location").Element("lat").Value), Convert.ToDouble(results.Element("result").Element("geometry").Element("location").Element("lng").Value));
                }


                return location;
            }
            catch (Exception ex)
            {
                EmergeLogWriter.WriteError("GetGeoLocation: " + address, EventCode.EMERGE_GET, ex.ToString());
                throw new GeocodingServiceException("There was an error with Google's Geocoding Service");
            }

        }
    }
}
