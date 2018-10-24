using Bluespire.Emerge.CommonService.PaymentGateway.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common.Logging;
using Bluespire.Emerge.Common.Exceptions;
using CMS.Helpers;
namespace Bluespire.Emerge.CommonService.PaymentGateway.GatewayType
{
    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="">throws PaymentGatewayException</exception>
    public class AuthorizeNet : IPaymentGateway
    {
        private PaymentStatus ProcessPayment(AuthorizeNetPayment payment)
        {
            PaymentStatus status = new PaymentStatus();

            Hashtable post_values = new Hashtable();

            //the API Login ID and Transaction Key must be replaced with valid values
            post_values.Add("x_login", payment.LoginID);
            post_values.Add("x_tran_key", payment.TransactionKey);


            post_values.Add("x_delim_data", "TRUE");
            post_values.Add("x_delim_char", '|');
            post_values.Add("x_relay_response", "FALSE");
            post_values.Add("x_type", "AUTH_CAPTURE");
            post_values.Add("x_method", "CC");
            post_values.Add("x_card_num", payment.CardNumber);
            post_values.Add("x_exp_date", payment.ExpirationMonth + "/" + payment.ExpirationYear);
            post_values.Add("x_amount", payment.Amount);
            post_values.Add("x_phone", payment.PhoneNumber);
            post_values.Add("x_email", payment.EmailID);
            post_values.Add("x_invoice_num", payment.InvoiceNumber);
            post_values.Add("x_first_name", payment.FirstName + " " + payment.LastName);
            post_values.Add("x_address", payment.Address);
            post_values.Add("x_state", payment.State);
            post_values.Add("x_city", payment.City);
            post_values.Add("x_zip", payment.Zip);


            //Set Production Url if IsProductionEnviroment is True
            String post_url = String.Empty;

            if (payment.IsProductionEnvironment)
            {
                post_url = "https://secure.authorize.net/gateway/transact.dll"; //SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".AuthorizeNetProdUrl");

            }
            else
            {
                post_url = "https://test.authorize.net/gateway/transact.dll"; // SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".AuthorizeNetTestUrl");
            }


            // This section takes the input fields and converts them to the proper format
            // for an http post.  For example: "x_login=username&x_tran_key=a1B2c3D4"

            String post_string = String.Empty;
            foreach (DictionaryEntry field in post_values)
            {
                post_string += field.Key + "=" + field.Value + "&";
            }
            post_string = post_string.TrimEnd('&');

            //create an HttpWebRequest object to communicate with Authorize.net
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(post_url);
            objRequest.Method = "POST";
            objRequest.ContentLength = post_string.Length;
            objRequest.ContentType = "application/x-www-form-urlencoded";

            //proxy authentication settings
            objRequest.Proxy = WebProxy.GetDefaultProxy();
            objRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;

            // post data is sent as a stream
            StreamWriter myWriter = null;
            myWriter = new StreamWriter(objRequest.GetRequestStream());
            myWriter.Write(post_string);
            myWriter.Close();

            // returned values are returned as a stream, then read into a string
            String post_response;
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            using (StreamReader responseStream = new StreamReader(objResponse.GetResponseStream()))
            {
                post_response = responseStream.ReadToEnd();
                responseStream.Close();
            }
            Array response_array = post_response.Split('|');

            if (post_response.Contains("This transaction has been approved"))
            {
                status.Status = PaymentStatusCode.Completed;
                status.Message = post_response;
                status.TransactionID = response_array.GetValue(6).ToString();
            }
            else
            {
                status.Message = "false" + "|" + response_array.GetValue(3).ToString() + "|" + response_array.GetValue(6).ToString();
                status.Status = PaymentStatusCode.Failed;
            }
            return status;
        }
        /// <summary>
        ///  Connect with payment gateway and process transaction.
        /// </summary>
        /// <param name="payment">Accept user payment details</param>
        /// <returns >Payment Status</returns>
        /// <exception cref="NotSupportedException, ArgumentNullException, InvalidOperationException, 
        /// InvalidOperationException, ArgumentException, UriFormatException, System.Security.SecurityException
        /// "  >Throw PaymentGatewayException </exception>
        public PaymentStatus SubmitPayment(Payment basePayment)
        {

            try
            {
                AuthorizeNetPayment payment = (AuthorizeNetPayment)basePayment;
                payment.Validate();
                return ProcessPayment(payment);
            }
            catch (NotSupportedException ex)
            {
                ThrowExceptions(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                ThrowExceptions(ex.ToString());
            }
            catch (InvalidOperationException ex)
            {
                ThrowExceptions(ex.ToString());
            }
            catch (ArgumentException ex)
            {
                ThrowExceptions(ex.ToString());
            }
            catch (UriFormatException ex)
            {
                ThrowExceptions(ex.ToString());
            }
            catch (System.Security.SecurityException ex)
            {
                ThrowExceptions(ex.ToString());
            }
            return null;
        }
        private void ThrowExceptions(string msg)
        {
            EmergeLogWriter.WriteError("Payment Gateway", EventCode.EMERGE_PROCESS_PAYMENTGATEWAY, msg);
            throw new PaymentGatewayException(ResHelper.GetString("Emerge.Exception.PaymentGatewayException"));
        }

    }
}
