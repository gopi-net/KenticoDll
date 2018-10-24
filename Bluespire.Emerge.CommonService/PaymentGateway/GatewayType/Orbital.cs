using Bluespire.Emerge.CommonService.PaymentGateway.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Bluespire.Emerge.Common.Logging;
using Bluespire.Emerge.Common.Exceptions;
using CMS.Helpers;

namespace Bluespire.Emerge.CommonService.PaymentGateway.GatewayType
{
    /// <summary>
    /// Process Orbital payment gateway
    /// </summary>
    public class Orbital : IPaymentGateway
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        private PaymentStatus ProcessPayment(OrbitalPayment payment)
        {
            PaymentStatus status = new PaymentStatus();


            string terminalId = string.Empty;
            string bin = string.Empty;
            string post_url = string.Empty;

            if (payment.IsProductionEnvironment)
            {

                post_url = "https://orbital1.paymentech.net";
            }
            else
            {

                post_url = "https://orbitalvar1.paymentech.net/";

            }

            terminalId = "001";
            bin = "000002";


            string Amount = payment.Amount.Trim() + "00";

            System.Text.StringBuilder post_values = new System.Text.StringBuilder();

            post_values.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?> ");
            post_values.Append("<Request>");
            post_values.Append("<NewOrder>");
            post_values.Append("<IndustryType>EC</IndustryType> ");
            post_values.Append("<MessageType>AC</MessageType>");
            post_values.Append("<BIN>" + bin + "</BIN>");
            post_values.Append("<MerchantID>" + payment.MerchantId + "</MerchantID>");
            post_values.Append("<TerminalID>" + terminalId + "</TerminalID>");
            post_values.Append("<AccountNum>" + payment.CardNumber + "</AccountNum>");
            post_values.Append("<Exp>" + payment.ExpirationMonth + "/" + payment.ExpirationYear + "</Exp>");
            post_values.Append("<CurrencyCode>" + payment.currencyCode.ToString() + "</CurrencyCode>");
            post_values.Append("<CurrencyExponent>2</CurrencyExponent>");
            post_values.Append("<CardSecVal>" + payment.CVV + "</CardSecVal>");
            post_values.Append("<AVSzip>" + payment.Zip + "</AVSzip>");
            post_values.Append("<AVSaddress1>" + payment.Address + "</AVSaddress1> ");

            post_values.Append("<AVScity>" + payment.City + "</AVScity>");
            post_values.Append("<AVSstate>" + payment.State + "</AVSstate>");
            post_values.Append("<AVSphoneNum>" + payment.PhoneNumber + "</AVSphoneNum>");
            post_values.Append("<AVSname>" + payment.FirstName + " " + payment.LastName + "</AVSname> ");
            post_values.Append("<AVScountryCode>" + payment.CountryCode.ToString() + "</AVScountryCode>");
            post_values.Append("<OrderID>" + payment.InvoiceNumber + "</OrderID>");
            post_values.Append("<Amount>" + Amount + "</Amount>");
            post_values.Append("<Comments></Comments>");
            post_values.Append("</NewOrder>");
            post_values.Append("</Request>");
            // create an HttpWebRequest object to communicate with payment gateway
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(post_url);
            objRequest.Method = "POST";
            objRequest.ContentLength = post_values.Length;//post_string.Length;
            objRequest.ContentType = "application/PTI50";
            objRequest.Headers.Add("Mime-Version", "1.1");
            objRequest.Headers.Add("Content-Transfer-Encoding", "text");
            objRequest.Headers.Add("Document-type", "Request");
            objRequest.Headers.Add("Request-number", "1");

            // post data is sent as a stream
            StreamWriter myWriter = null;
            objRequest.Proxy = WebProxy.GetDefaultProxy();
            objRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;
            myWriter = new StreamWriter(objRequest.GetRequestStream());
            myWriter.Write(post_values.ToString());
            myWriter.Close();


            String post_response;

            // Use this code for to retrive actual information.
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            using (StreamReader responseStream = new StreamReader(objResponse.GetResponseStream()))
            {
                post_response = responseStream.ReadToEnd();

            }


            //TODO: Test response
            // post_response = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><NewOrderResp><IndustryType></IndustryType><MessageType>AC</MessageType><MerchantID>700000009308</MerchantID><TerminalID>001</TerminalID><CardBrand>VI</CardBrand><AccountNum>4788250000028291</AccountNum><OrderID>1234567890123456789012</OrderID><TxRefNum>4CAB272C34FDAB6463AE78B4F2EB024A545554ED</TxRefNum><TxRefIdx>1</TxRefIdx><ProcStatus>0</ProcStatus><ApprovalStatus>1</ApprovalStatus><RespCode>00</RespCode><AVSRespCode>  </AVSRespCode><CVV2RespCode> </CVV2RespCode><AuthCode>292846</AuthCode><RecurringAdviceCd></RecurringAdviceCd><CAVVRespCode></CAVVRespCode><StatusMsg>Approved</StatusMsg><RespMsg></RespMsg><HostRespCode>00</HostRespCode><HostAVSRespCode></HostAVSRespCode><HostCVV2RespCode></HostCVV2RespCode><CustomerRefNum></CustomerRefNum><CustomerName></CustomerName><ProfileProcStatus></ProfileProcStatus><CustomerProfileMessage></CustomerProfileMessage><RespTime>092501</RespTime><PartialAuthOccurred></PartialAuthOccurred><RequestedAmount></RequestedAmount><RedeemedAmount></RedeemedAmount><RemainingBalance></RemainingBalance><CountryFraudFilterStatus></CountryFraudFilterStatus><IsoCountryCode></IsoCountryCode></NewOrderResp></Response>";
            //post_response = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><!DOCTYPE Response SYSTEM "
            //                + "\"Response_PTI26.dtd\"><Response><QuickResponse HcsTcsInd=\"T\" MessageType=\"U\"" +
            //                "LangInd=\"\" TzCode=\"\" Version=\"2\"><ProcStatus>9717</ProcStatus><StatusMsg StatusMsgLth=\"54\">" +
            //                "Security Information - agent/chain/merchant is missing</StatusMsg></QuickResponse></Response>";


            if (post_response.Contains("Response_PTI26.dtd"))
            {

                Match match = Regex.Match(post_response, @"<StatusMsg StatusMsgLth=""[0-9]+"">([0-9A-Za-z\-' '/]+)<\/StatusMsg>", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    status.Message = match.Groups[1].Value;
                }
                status.Status = PaymentStatusCode.Failed;

            }
            else
            {
                XmlDocument xmlResponse = new XmlDocument();
                xmlResponse.LoadXml(post_response);

                XmlNode errornode = xmlResponse.SelectSingleNode("/QuickResponse");
                XmlNode errorMessage = xmlResponse.SelectSingleNode("//StatusMsg");
                if (errorMessage != null)
                {
                    status.Message = errorMessage.InnerText;
                }
                if (errornode != null && errornode.HasChildNodes)
                {
                    status.Status = PaymentStatusCode.Failed;

                    return status;
                }
                else
                {
                    XmlNode approvalNode = xmlResponse.SelectSingleNode("//ApprovalStatus");
                    if (approvalNode != null && approvalNode.InnerText.Trim() == "1")
                    {
                        XmlNode referenceNode = xmlResponse.SelectSingleNode("//TxRefNum");
                        if (referenceNode != null)
                        {
                            status.TransactionID = referenceNode.InnerText;
                        }
                        status.Status = PaymentStatusCode.Completed;

                    }
                    else
                    {
                        status.Status = PaymentStatusCode.Failed;

                    }

                    return status;

                }
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
        /// XmlException
        /// "  >Throw PaymentGatewayException </exception>
        public PaymentStatus SubmitPayment(Payment basePayment)
        {
            try
            {
                OrbitalPayment payment = (OrbitalPayment)basePayment;
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
            catch (XmlException ex)
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
