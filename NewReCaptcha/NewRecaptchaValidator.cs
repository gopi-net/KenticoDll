using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;

using CMS.EventLog;
using CMS.IO;

namespace CMS.UIControls
{
    /// <summary>
    /// Calls the new reCAPTCHA server to validate the answer to a reCAPTCHA challenge.
    /// </summary>
    public class NewRecaptchaValidator
    {
        #region "Constants"

        private const string VERIFYURL = "https://www.google.com/recaptcha/api/siteverify";

        #endregion


        #region "Variables"

        private string mRemoteIp;

        #endregion


        #region "Properties"

        /// <summary>
        /// Required. The shared key between your site and ReCAPTCHA..
        /// </summary>
        public string Secret
        {
            get;
            set;
        }


        /// <summary>
        /// Optional. The user's IP address.
        /// </summary>
        public string RemoteIP
        {
            get
            {
                return mRemoteIp;
            }

            set
            {
                IPAddress ip = IPAddress.Parse(value);

                if (ip == null ||
                    (ip.AddressFamily != AddressFamily.InterNetwork &&
                    ip.AddressFamily != AddressFamily.InterNetworkV6))
                {
                    throw new ArgumentException("Expecting an IP address, got " + ip);
                }

                mRemoteIp = ip.ToString();
            }
        }


        /// <summary>
        /// Required. The user response token provided by the reCAPTCHA to the user and provided to your site on..
        /// </summary>
        public string Response
        {
            get;
            set;
        }


        /// <summary>
        /// reCAPTCHA proxy to be used.
        /// </summary>
        public IWebProxy Proxy
        {
            get;
            set;
        }

        #endregion


        #region "Methods"

        /// <summary>
        /// Validate reCAPTCHA response.
        /// </summary>
        public NewRecaptchaResponse Validate()
        {
            if (string.IsNullOrEmpty(Secret))
            {
                return NewRecaptchaResponse.MissingSecret;
            }
            if (string.IsNullOrEmpty(Response))
            {
                return NewRecaptchaResponse.MissingResponse;
            }

            // Prepare web request
            var request = (HttpWebRequest)WebRequest.Create(VERIFYURL);
            request.ProtocolVersion = HttpVersion.Version10;
            request.Timeout = 30 * 1000 /* 30 seconds */;
            request.Method = "POST";
            request.UserAgent = "reCAPTCHA/ASP.NET";
            request.ContentType = "application/x-www-form-urlencoded";

            if (Proxy != null)
            {
                request.Proxy = Proxy;
            }

            // Prepare form data
            string formdata = String.Format("secret={0}&response={1}&remoteip={2}", HttpUtility.UrlEncode(Secret), HttpUtility.UrlEncode(Response), HttpUtility.UrlEncode(RemoteIP));

            // Write data to request
            byte[] formbytes = Encoding.ASCII.GetBytes(formdata);
            using (Stream requestStream = StreamWrapper.New(request.GetRequestStream()))
            {
                requestStream.Write(formbytes, 0, formbytes.Length);
            }

            // Get validation response
            try
            {
                using (var httpResponse = request.GetResponse())
                {
                    return NewRecaptchaResponse.ParseResponse(httpResponse);
                }
            }
            catch (WebException ex)
            {
                EventLogProvider.LogException("NewReCAPTCHA", "VALIDATE", ex);
                return NewRecaptchaResponse.Invalid;
            }
        }

        #endregion
    }
}
