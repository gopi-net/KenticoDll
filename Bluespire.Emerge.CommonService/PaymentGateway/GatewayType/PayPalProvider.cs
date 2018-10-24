using Bluespire.Emerge.CommonService.PaymentGateway.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PayPal;
using PayPal.Manager;
using PayPal.Api.Payments;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

using Bluespire.Emerge.CommonService.PaymentGateway;
using System.Web;
using Bluespire.Emerge.CommonService.PaymentGateway.PayPalWrapper;
using Bluespire.Emerge.Common.Logging;
using Bluespire.Emerge.Common.Exceptions;
using System.Xml;
using CMS.Helpers;

namespace Bluespire.Emerge.CommonService.PaymentGateway.GatewayType
{
    public class PayPalProvider : IPaymentGateway
    {

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
            PayPalPayment payment = (PayPalPayment)basePayment;
            payment.Validate();

            try
            {
                if (payment.payPalDevelopmentType == PayPalDevelopmentType.REST)
                {
                    return RESTProcessPayment(payment);
                }
                else
                {

                    // SOAP Development change file name after implementing SOAP development
                    return SOAPProccessPayment(payment);
                }
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
        /// <summary>
        /// REST method for Paypal
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>

        private PaymentStatus RESTProcessPayment(PayPalPayment payment)
        {
            PaymentStatus status = new PaymentStatus();
            //try
            //{
            Address billingAddress = new Address();


            billingAddress.city = payment.City;
            billingAddress.country_code = payment.CountryCode.ToString();
            billingAddress.line1 = payment.Address;
            billingAddress.postal_code = payment.Zip;
            billingAddress.state = payment.State;

            // ###CreditCard
            // A resource representing a credit card that can be
            // used to fund a payment.
            CreditCard crdtCard = new CreditCard();
            crdtCard.billing_address = billingAddress;
            crdtCard.cvv2 = payment.CVV;
            crdtCard.expire_month = payment.ExpirationMonth;
            crdtCard.expire_year = payment.ExpirationYear;
            crdtCard.first_name = payment.FirstName;
            crdtCard.last_name = payment.LastName;
            crdtCard.number = payment.CardNumber;
            crdtCard.type = payment.CardType.ToString();

            // Let's you specify a payment amount.
            Amount amnt = new Amount();
            amnt.currency = payment.CurrencyCode.ToString();
            // Total must be equal to sum of shipping, tax and subtotal.
            amnt.total = payment.Amount;
            // amnt.details = details;

            // ###Transaction
            // A transaction defines the contract of a
            // payment - what is the payment for and who
            // is fulfilling it. Transaction is created with
            // a `Payee` and `Amount` types
            Transaction tran = new Transaction();
            tran.amount = amnt;
            tran.description = "This is the payment transaction description.";

            // The Payment creation API requires a list of
            // Transaction; add the created `Transaction`
            // to a List
            List<Transaction> transactions = new List<Transaction>();
            transactions.Add(tran);

            // ###FundingInstrument
            // A resource representing a Payeer's funding instrument.
            // Use a Payer ID (A unique identifier of the payer generated
            // and provided by the facilitator. This is required when
            // creating or using a tokenized funding instrument)
            // and the `CreditCardDetails`
            FundingInstrument fundInstrument = new FundingInstrument();
            fundInstrument.credit_card = crdtCard;

            // The Payment creation API requires a list of
            // FundingInstrument; add the created `FundingInstrument`
            // to a List
            List<FundingInstrument> fundingInstrumentList = new List<FundingInstrument>();
            fundingInstrumentList.Add(fundInstrument);

            // ###Payer
            // A resource representing a Payer that funds a payment
            // Use the List of `FundingInstrument` and the Payment Method
            // as 'credit_card'
            Payer payr = new Payer();
            payr.funding_instruments = fundingInstrumentList;
            payr.payment_method = "credit_card";

            // ###Payment
            // A Payment Resource; create one using
            // the above types and intent as `sale`
            PayPal.Api.Payments.Payment pymnt = new PayPal.Api.Payments.Payment();
            pymnt.intent = payment.PaymentAction.ToString();
            pymnt.payer = payr;
            pymnt.transactions = transactions;


            // ###AccessToken
            // Retrieve the access token from
            // OAuthTokenCredential by passing in
            // ClientID and ClientSecret
            // It is not mandatory to generate Access Token on a per call basis.
            // Typically the access token can be generated once and
            // reused within the expiry window
            string accessToken = new OAuthTokenCredential(payment.ClientID, payment.ClientSecret).GetAccessToken();

            // ### Api Context
            // Pass in a `ApiContext` object to authenticate 
            // the call and to send a unique request id 
            // (that ensures idempotency). The SDK generates
            // a request id if you do not pass one explicitly. 
            APIContext apiContext = new APIContext(accessToken);
            // Use this variant if you want to pass in a request id  
            // that is meaningful in your application, ideally 
            // a order id.
            // String requestId = Long.toString(System.nanoTime();
            // APIContext apiContext = new APIContext(accessToken, requestId ));

            // Create a payment by posting to the APIService
            // using a valid AccessToken
            // The return object contains the status;
            PayPal.Api.Payments.Payment createdPayment = pymnt.Create(apiContext);

            if (createdPayment.state == "completed" || createdPayment.state == "approved")
            {
                status.TransactionID = createdPayment.id;
                status.Status = PaymentStatusCode.Completed;
                status.Message = "Trasaction has been successfully completed";
                return status;
            }
            else
            {
                status.Status = PaymentStatusCode.Failed;
                status.Message = "Transaction has been failed.";
                return status;
            }


            //}
            //catch (PayPal.Exception.PayPalException ex)
            //{
            //    status.Status = PaymentStatusCode.Failed;
            //    status.Message = ex.InnerException.Message;
            //    return status;
            //}
        }


        private string GetClientIpAddress()
        {
            HttpRequest currentRequest = HttpContext.Current.Request;
            string ipAddress = currentRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (ipAddress == null || ipAddress.ToLower() == "unknown")
                ipAddress = currentRequest.ServerVariables["REMOTE_ADDR"];

            return ipAddress;
        }

        private PaymentStatus SOAPProccessPayment(PayPalPayment payment)
        {
            PaymentStatus status = new PaymentStatus();
            //try
            //{
            PayPalWrapper.UserIdPasswordType user = new PayPalWrapper.UserIdPasswordType();
            user.Username = payment.APIUsername;
            user.Password = payment.APIPassword;
            user.Signature = payment.APISignature;
            PayPalWrapper.PayPalAPIAASoapBinding PPInterface = null;
            if (payment.IsProductionEnvironment)
            {
                PPInterface = new PayPalWrapper.PayPalAPIAASoapBinding("https://api-3t.paypal.com/2.0/");
            }
            else
            {
                PPInterface = new PayPalWrapper.PayPalAPIAASoapBinding("https://api-3t.sandbox.paypal.com/2.0/");
            }


            PPInterface.RequesterCredentials = new PayPalWrapper.CustomSecurityHeaderType();
            PPInterface.RequesterCredentials.Credentials = new PayPalWrapper.UserIdPasswordType();
            PPInterface.RequesterCredentials.Credentials = user;


            // Create the request object.
            PayPalWrapper.DoDirectPaymentRequestType pp_Request = new PayPalWrapper.DoDirectPaymentRequestType();
            pp_Request.Version = "51.0";

            PayPalWrapper.PaymentActionCodeType paymentAction = (PayPalWrapper.PaymentActionCodeType)payment.PaymentAction;
            // Add request-specific fields to the request.
            // Create the request details object.
            pp_Request.DoDirectPaymentRequestDetails = new PayPalWrapper.DoDirectPaymentRequestDetailsType();

            pp_Request.DoDirectPaymentRequestDetails.IPAddress = payment.IPAddress; //GetClientIpAddress();
            pp_Request.DoDirectPaymentRequestDetails.MerchantSessionId = payment.MerchantAccountId;
            pp_Request.DoDirectPaymentRequestDetails.PaymentAction = paymentAction;

            pp_Request.DoDirectPaymentRequestDetails.CreditCard = new PayPalWrapper.CreditCardDetailsType();

            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CreditCardNumber = payment.CardNumber;
            string creditCardType = payment.CardType.ToString().ToLower();
            switch (creditCardType)
            {
                case "visa":
                    pp_Request.DoDirectPaymentRequestDetails.CreditCard.CreditCardType = PayPalWrapper.CreditCardTypeType.Visa;
                    break;
                case "mastercard":
                    pp_Request.DoDirectPaymentRequestDetails.CreditCard.CreditCardType = PayPalWrapper.CreditCardTypeType.MasterCard;
                    break;
                case "discover":
                    pp_Request.DoDirectPaymentRequestDetails.CreditCard.CreditCardType = PayPalWrapper.CreditCardTypeType.Discover;
                    break;
                case "amex":
                    pp_Request.DoDirectPaymentRequestDetails.CreditCard.CreditCardType = PayPalWrapper.CreditCardTypeType.Amex;
                    break;
            }
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CVV2 = payment.CVV;
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.ExpMonth = payment.ExpirationMonth;
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.ExpYear = payment.ExpirationYear;
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.ExpMonthSpecified = true;
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.ExpYearSpecified = true;
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner = new PayPalWrapper.PayerInfoType();
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.Payer = "";
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.PayerID = "";
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.PayerStatus = PayPalWrapper.PayPalUserStatusCodeType.unverified;
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.PayerCountry = CountryCodeType.US;

            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.Address = new PayPalWrapper.AddressType();
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.Address.Street1 = payment.Address;
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.Address.Street2 = string.Empty;
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.Address.CityName = payment.City;
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.Address.StateOrProvince = payment.State;
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.Address.PostalCode = payment.Zip;
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.Address.CountryName = payment.CountryName;
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.Address.Country = payment.CountryCode;
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.Address.CountrySpecified = true;

            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.PayerName = new PayPalWrapper.PersonNameType();
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.PayerName.FirstName = payment.FirstName;
            pp_Request.DoDirectPaymentRequestDetails.CreditCard.CardOwner.PayerName.LastName = payment.LastName;
            pp_Request.DoDirectPaymentRequestDetails.PaymentDetails = new PayPalWrapper.PaymentDetailsType();
            pp_Request.DoDirectPaymentRequestDetails.PaymentDetails.OrderTotal = new PayPalWrapper.BasicAmountType();
            // NOTE: The only currency supported by the Direct Payment API at this time is US dollars (USD).

            pp_Request.DoDirectPaymentRequestDetails.PaymentDetails.OrderTotal.currencyID = PayPalWrapper.CurrencyCodeType.USD;
            pp_Request.DoDirectPaymentRequestDetails.PaymentDetails.OrderTotal.Value = payment.Amount;

            // Execute the API operation and obtain the response.
            PayPalWrapper.DoDirectPaymentReq paymentReq = new PayPalWrapper.DoDirectPaymentReq();
            paymentReq.DoDirectPaymentRequest = pp_Request;

            PayPalWrapper.DoDirectPaymentResponseType pp_response = new PayPalWrapper.DoDirectPaymentResponseType();

            pp_response = PPInterface.DoDirectPayment(paymentReq);
            if (pp_response.Ack.ToString().ToLower() == "success")
            {
                status.TransactionID = pp_response.TransactionID;
                status.Status = PaymentStatusCode.Completed;
                status.Message = "Trasaction successfully completed";
            }
            else
            {
                status.Status = PaymentStatusCode.Failed;
                foreach (ErrorType errrors in pp_response.Errors)
                {
                    status.Message += "ErrorCode:-" + errrors.ErrorCode + " ---" + errrors.ShortMessage + " | ";
                }

            }
            //  return Ret;

            //}
            //catch (Exception ex)
            //{

            //    status.Status = PaymentStatusCode.Failed;
            //    status.Message = ex.Message.ToString();
            //    throw ex;
            //}
            return status;
        }
    }
}
