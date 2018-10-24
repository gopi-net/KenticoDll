using Bluespire.Emerge.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.CommonService.PaymentGateway
{
    /// <summary>
    /// 
    /// </summary>
    public enum PayPalPaymentActionCode
    {
        None = 0,
        Authorization = 1,
        Sale = 2,
        Order = 3,

    }

    public enum PayPalDevelopmentType
    {
        REST,
        SOAP

    }
    /// <summary>
    /// Payment Class Contains all information relatated to payment gatway
    /// </summary>
    public class PayPalPayment : Payment
    {
        #region [properties]
        private CurrencyCode _CurrenyCode = CurrencyCode.USD;
        public CurrencyCode CurrencyCode
        {
            get
            {
                return _CurrenyCode;
            }
            set
            {
                _CurrenyCode = value;
            }
        }

        private PayPalDevelopmentType _PaymentDevelopmentType = PayPalDevelopmentType.REST;
        public PayPalDevelopmentType payPalDevelopmentType
        {
            get
            {
                return _PaymentDevelopmentType;
            }
            set
            {
                _PaymentDevelopmentType = value;
            }
        }

        private PayPalPaymentActionCode _PaymentAction = PayPalPaymentActionCode.Sale;
        public PayPalPaymentActionCode PaymentAction
        {
            get
            {
                return _PaymentAction;
            }
            set
            {
                _PaymentAction = value;
            }
        }


        /// <summary>
        ///  Use this two keys when using REST API Method.
        ///  To Create those keys follow below steps.
        ///  1. Create Development Account
        ///  2. Create Application Provide.
        ///  3. Provide Application Details
        ///  4. Application Provide us ClientID and Secrete key.
        /// </summary>
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }


        /// <summary>
        /// Use below Details Use when paypal SOAP binding with API
        /// </summary>
        /// 
        [Description("Use when paypal SOAP binding with API")]
        public string APIUsername { get; set; }
        [Description("Use when paypal SOAP binding with API")]
        public string APIPassword { get; set; }
        [Description("Use when paypal SOAP binding with API")]
        public string APISignature { get; set; }
        [Description("Use when paypal SOAP binding with API")]
        public string MerchantAccountId { get; set; }
        [Description("Use when paypal SOAP binding with API")]
        public string IPAddress { get; set; }
        #endregion


        #region [Public Functions]
        public override void Validate()
        {

            StringBuilder errorColumn = new StringBuilder();
            if (payPalDevelopmentType == PayPalDevelopmentType.REST)
            {
                if (string.IsNullOrEmpty(ClientID)) { errorColumn.Append("ClientID, "); }
                if (string.IsNullOrEmpty(ClientSecret)) { errorColumn.Append("ClientSecret Key, "); }
               
            }
            else
            {
                if (string.IsNullOrEmpty(APIUsername)) { errorColumn.Append("API Username, "); }
                if (string.IsNullOrEmpty(APIPassword)) { errorColumn.Append("API Password, "); }
                if (string.IsNullOrEmpty(APISignature)) { errorColumn.Append("API Signature, "); }
                if (string.IsNullOrEmpty(MerchantAccountId)) { errorColumn.Append("Merchant Account Id, "); }
                if (string.IsNullOrEmpty(IPAddress)) { errorColumn.Append("IP Address, "); }
            }
            base.result = errorColumn;
            // this  function check result has a data if yes then throw exception
            base.Validate();
        }
        #endregion
    }
}
