using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.CommonService.PaymentGateway.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace Bluespire.Emerge.CommonService.PaymentGateway
{

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "50.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:ebay:apis:eBLBaseComponents")]
    public enum CountryCodeType
    {

        /// <remarks/>
        AF,

        /// <remarks/>
        AL,

        /// <remarks/>
        DZ,

        /// <remarks/>
        AS,

        /// <remarks/>
        AD,

        /// <remarks/>
        AO,

        /// <remarks/>
        AI,

        /// <remarks/>
        AQ,

        /// <remarks/>
        AG,

        /// <remarks/>
        AR,

        /// <remarks/>
        AM,

        /// <remarks/>
        AW,

        /// <remarks/>
        AU,

        /// <remarks/>
        AT,

        /// <remarks/>
        AZ,

        /// <remarks/>
        BS,

        /// <remarks/>
        BH,

        /// <remarks/>
        BD,

        /// <remarks/>
        BB,

        /// <remarks/>
        BY,

        /// <remarks/>
        BE,

        /// <remarks/>
        BZ,

        /// <remarks/>
        BJ,

        /// <remarks/>
        BM,

        /// <remarks/>
        BT,

        /// <remarks/>
        BO,

        /// <remarks/>
        BA,

        /// <remarks/>
        BW,

        /// <remarks/>
        BV,

        /// <remarks/>
        BR,

        /// <remarks/>
        IO,

        /// <remarks/>
        BN,

        /// <remarks/>
        BG,

        /// <remarks/>
        BF,

        /// <remarks/>
        BI,

        /// <remarks/>
        KH,

        /// <remarks/>
        CM,

        /// <remarks/>
        CA,

        /// <remarks/>
        CV,

        /// <remarks/>
        KY,

        /// <remarks/>
        CF,

        /// <remarks/>
        TD,

        /// <remarks/>
        CL,

        /// <remarks/>
        C2,

        /// <remarks/>
        CN,

        /// <remarks/>
        CX,

        /// <remarks/>
        CC,

        /// <remarks/>
        CO,

        /// <remarks/>
        KM,

        /// <remarks/>
        CG,

        /// <remarks/>
        CD,

        /// <remarks/>
        CK,

        /// <remarks/>
        CR,

        /// <remarks/>
        CI,

        /// <remarks/>
        HR,

        /// <remarks/>
        CU,

        /// <remarks/>
        CY,

        /// <remarks/>
        CZ,

        /// <remarks/>
        DK,

        /// <remarks/>
        DJ,

        /// <remarks/>
        DM,

        /// <remarks/>
        DO,

        /// <remarks/>
        TP,

        /// <remarks/>
        EC,

        /// <remarks/>
        EG,

        /// <remarks/>
        SV,

        /// <remarks/>
        GQ,

        /// <remarks/>
        ER,

        /// <remarks/>
        EE,

        /// <remarks/>
        ET,

        /// <remarks/>
        FK,

        /// <remarks/>
        FO,

        /// <remarks/>
        FJ,

        /// <remarks/>
        FI,

        /// <remarks/>
        FR,

        /// <remarks/>
        GF,

        /// <remarks/>
        PF,

        /// <remarks/>
        TF,

        /// <remarks/>
        GA,

        /// <remarks/>
        GM,

        /// <remarks/>
        GE,

        /// <remarks/>
        DE,

        /// <remarks/>
        GH,

        /// <remarks/>
        GI,

        /// <remarks/>
        GR,

        /// <remarks/>
        GL,

        /// <remarks/>
        GD,

        /// <remarks/>
        GP,

        /// <remarks/>
        GU,

        /// <remarks/>
        GT,

        /// <remarks/>
        GN,

        /// <remarks/>
        GW,

        /// <remarks/>
        GY,

        /// <remarks/>
        HT,

        /// <remarks/>
        HM,

        /// <remarks/>
        VA,

        /// <remarks/>
        HN,

        /// <remarks/>
        HK,

        /// <remarks/>
        HU,

        /// <remarks/>
        IS,

        /// <remarks/>
        IN,

        /// <remarks/>
        ID,

        /// <remarks/>
        IR,

        /// <remarks/>
        IQ,

        /// <remarks/>
        IE,

        /// <remarks/>
        IL,

        /// <remarks/>
        IT,

        /// <remarks/>
        JM,

        /// <remarks/>
        JP,

        /// <remarks/>
        JO,

        /// <remarks/>
        KZ,

        /// <remarks/>
        KE,

        /// <remarks/>
        KI,

        /// <remarks/>
        KP,

        /// <remarks/>
        KR,

        /// <remarks/>
        KW,

        /// <remarks/>
        KG,

        /// <remarks/>
        LA,

        /// <remarks/>
        LV,

        /// <remarks/>
        LB,

        /// <remarks/>
        LS,

        /// <remarks/>
        LR,

        /// <remarks/>
        LY,

        /// <remarks/>
        LI,

        /// <remarks/>
        LT,

        /// <remarks/>
        LU,

        /// <remarks/>
        MO,

        /// <remarks/>
        MK,

        /// <remarks/>
        MG,

        /// <remarks/>
        MW,

        /// <remarks/>
        MY,

        /// <remarks/>
        MV,

        /// <remarks/>
        ML,

        /// <remarks/>
        MT,

        /// <remarks/>
        MH,

        /// <remarks/>
        MQ,

        /// <remarks/>
        MR,

        /// <remarks/>
        MU,

        /// <remarks/>
        YT,

        /// <remarks/>
        MX,

        /// <remarks/>
        FM,

        /// <remarks/>
        MD,

        /// <remarks/>
        MC,

        /// <remarks/>
        MN,

        /// <remarks/>
        MS,

        /// <remarks/>
        MA,

        /// <remarks/>
        MZ,

        /// <remarks/>
        MM,

        /// <remarks/>
        NA,

        /// <remarks/>
        NR,

        /// <remarks/>
        NP,

        /// <remarks/>
        NL,

        /// <remarks/>
        AN,

        /// <remarks/>
        NC,

        /// <remarks/>
        NZ,

        /// <remarks/>
        NI,

        /// <remarks/>
        NE,

        /// <remarks/>
        NG,

        /// <remarks/>
        NU,

        /// <remarks/>
        NF,

        /// <remarks/>
        MP,

        /// <remarks/>
        NO,

        /// <remarks/>
        OM,

        /// <remarks/>
        PK,

        /// <remarks/>
        PW,

        /// <remarks/>
        PS,

        /// <remarks/>
        PA,

        /// <remarks/>
        PG,

        /// <remarks/>
        PY,

        /// <remarks/>
        PE,

        /// <remarks/>
        PH,

        /// <remarks/>
        PN,

        /// <remarks/>
        PL,

        /// <remarks/>
        PT,

        /// <remarks/>
        PR,

        /// <remarks/>
        QA,

        /// <remarks/>
        RE,

        /// <remarks/>
        RO,

        /// <remarks/>
        RU,

        /// <remarks/>
        RW,

        /// <remarks/>
        SH,

        /// <remarks/>
        KN,

        /// <remarks/>
        LC,

        /// <remarks/>
        PM,

        /// <remarks/>
        VC,

        /// <remarks/>
        WS,

        /// <remarks/>
        SM,

        /// <remarks/>
        ST,

        /// <remarks/>
        SA,

        /// <remarks/>
        SN,

        /// <remarks/>
        SC,

        /// <remarks/>
        SL,

        /// <remarks/>
        SG,

        /// <remarks/>
        SK,

        /// <remarks/>
        SI,

        /// <remarks/>
        SB,

        /// <remarks/>
        SO,

        /// <remarks/>
        ZA,

        /// <remarks/>
        GS,

        /// <remarks/>
        ES,

        /// <remarks/>
        LK,

        /// <remarks/>
        SD,

        /// <remarks/>
        SR,

        /// <remarks/>
        SJ,

        /// <remarks/>
        SZ,

        /// <remarks/>
        SE,

        /// <remarks/>
        CH,

        /// <remarks/>
        SY,

        /// <remarks/>
        TW,

        /// <remarks/>
        TJ,

        /// <remarks/>
        TZ,

        /// <remarks/>
        TH,

        /// <remarks/>
        TG,

        /// <remarks/>
        TK,

        /// <remarks/>
        TO,

        /// <remarks/>
        TT,

        /// <remarks/>
        TN,

        /// <remarks/>
        TR,

        /// <remarks/>
        TM,

        /// <remarks/>
        TC,

        /// <remarks/>
        TV,

        /// <remarks/>
        UG,

        /// <remarks/>
        UA,

        /// <remarks/>
        AE,

        /// <remarks/>
        GB,

        /// <remarks/>
        US,

        /// <remarks/>
        UM,

        /// <remarks/>
        UY,

        /// <remarks/>
        UZ,

        /// <remarks/>
        VU,

        /// <remarks/>
        VE,

        /// <remarks/>
        VN,

        /// <remarks/>
        VG,

        /// <remarks/>
        VI,

        /// <remarks/>
        WF,

        /// <remarks/>
        EH,

        /// <remarks/>
        YE,

        /// <remarks/>
        YU,

        /// <remarks/>
        ZM,

        /// <remarks/>
        ZW,

        /// <remarks/>
        AA,

        /// <remarks/>
        QM,

        /// <remarks/>
        QN,

        /// <remarks/>
        QO,

        /// <remarks/>
        QP,

        /// <remarks/>
        CS,

        /// <remarks/>
        CustomCode,

        /// <remarks/>
        GG,

        /// <remarks/>
        IM,

        /// <remarks/>
        JE,

        /// <remarks/>
        TL,
    }

    public enum PaymentStatusCode
    {
        Unknown = 0,
        Completed = 1,
        CanceledReversal = 2,
        Denied = 3,
        Expired = 4,
        Failed = 5,
        InProgress = 6,
        Pending = 7,
        Processed = 8,
        Refunded = 9,
        Reversed = 10,
        Voided = 11,
    }

    /// <summary>
    /// Payment gateway result in Status, Message and TransactionID.
    /// </summary>
    public class PaymentStatus
    {
        private PaymentStatusCode _status = PaymentStatusCode.Unknown;
        public PaymentStatusCode Status
        {
            get { return _status; }
            internal set { _status = value; }
        }
        private string _message = string.Empty;
        public string Message
        {
            get { return _message; }
            internal set { _message = value; }
        }
        private string _TransactionID = string.Empty;
        public string TransactionID
        {
            get { return _TransactionID; }
            internal set { _TransactionID = value; }
        }
    }

    /// <summary>
    /// Payment Gateway List
    /// </summary>
    public enum PaymentGateways
    {
        AuthorizeNet,
        PayPal,
        Orbital


    }

    public enum CreditCardType
    {

        /// <remarks/>
        visa,

        /// <remarks/>
        mastercard,

        /// <remarks/>
        Discover,

        /// <remarks/>
        amex,

        /// <remarks/>
        discover,


    }

    public enum CurrencyCode
    {
        AUD,
        CAD,
        CZK,
        DKK,
        EUR,
        HKD,
        HUF,
        JPY,
        NOK,
        NZD,
        PLN,
        GBP,
        SGD,
        SEK,
        CHF,
        USD
    }

    /// <summary>
    /// Accept require parameters for payment gateway
    /// </summary>
    public class Payment : IPaymentGatewayValidate
    {
        #region [Properties]

        protected StringBuilder result { get; set; }

        /// <summary>
        /// Order Details
        /// </summary>
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// Credit Card Info
        /// </summary>
        public string CardNumber { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public string CVV { get; set; }


        /// <summary>
        /// User Details 
        /// </summary>
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Amount { get; set; }
        public string EmailID { get; set; }
        public string PhoneNumber { get; set; }


        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CountryName { get; set; }
        public string Zip { get; set; }

        public CreditCardType CardType { get; set; }
        public CountryCodeType CountryCode { get; set; }
        /// <summary>
        /// Set Test/Production Enviournment
        /// </summary>
        private bool _IsProductionEnvironment = false;
        public bool IsProductionEnvironment
        {
            get
            {
                return _IsProductionEnvironment;
            }
            set
            {
                _IsProductionEnvironment = value;
            }
        }
        #endregion


        /// <summary>
        /// Validated required parameters
        /// </summary>
        public virtual void Validate()
        {
            
            //StringBuilder result = new StringBuilder(); //string.Empty;
            if (CardNumber == string.Empty) { result.Append("Card Number, "); }
            if (ExpirationMonth.ToString() == string.Empty || ExpirationMonth.ToString().Equals("0")) { result.Append("Card Expiration Month, "); }
            if (ExpirationYear.ToString() == string.Empty || ExpirationYear.ToString().Equals("0")) { result.Append("Card Expiration Year, "); }
            if (FirstName == string.Empty) { result.Append("First Name, "); }
            if (LastName == string.Empty) { result.Append("Last Name, "); }
            if (Amount == string.Empty) { result.Append("Amount, "); }
            if (EmailID == string.Empty) { result.Append("EmailID, "); }
            if (PhoneNumber == string.Empty) { result.Append("Phone Number, "); }
            if (Address == string.Empty) { result.Append("Address, "); }
            if (City == string.Empty) { result.Append("City, "); }
            if (State == string.Empty) { result.Append("State, "); }
            if (Zip == string.Empty) { result.Append("zip, "); }
            if (string.IsNullOrEmpty(CardType.ToString())) { result.Append("Card Type, "); }

            ThrowInsufficiantInfoException(result);
        }

        internal void ThrowInsufficiantInfoException(StringBuilder result)
        {
            string resultVal = string.Empty;
            if (result.ToString().EndsWith(", "))
                resultVal = result.ToString().Substring(0, result.ToString().LastIndexOf(", "));
            if (!string.IsNullOrEmpty(resultVal.ToString()))
            {
                string errorMsg = "Information for {0} not valid.";
                if (string.IsNullOrEmpty(CMS.Helpers.ResHelper.GetString("Emerge.PaymentGateway.InsufficientInfo")))
                {
                    errorMsg = CMS.Helpers.ResHelper.GetString("Emerge.PaymentGateway.InsufficientInfo");
                }

                throw new PaymentGatewayInsufficientInfoException(string.Format(errorMsg, resultVal.ToString()));

            }
        }
    }
}
