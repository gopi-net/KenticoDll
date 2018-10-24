using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.CommonService.PaymentGateway.Interface;
using Bluespire.Emerge.CommonService.PaymentGateway;
using Bluespire.Emerge.CommonService;
public partial class PaypalInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AuthorizationService.CheckPermission(Bluespire.Emerge.Common.Constants.Modules.GiftShop, "Test2.aspx");
    }
    private void ProcessPayPalSOAP()
    {

        IPaymentGateway paymentGateway = new PaymentGatewayFactory().CreatePaymentGateway(PaymentGateways.PayPal);
        PayPalPayment payment = new PayPalPayment();


        payment.City = "Johnstown";
        payment.CountryCode = CountryCodeType.US;
        payment.Address = "52 N Main ST";
        payment.Zip = "43210";
        payment.State = "OH";



        payment.CVV = "874";
        payment.ExpirationMonth = 11;
        payment.ExpirationYear = 2018;
        payment.FirstName = "Joe";
        payment.LastName = "Shopper";
        payment.CardNumber = "4417119669820331";
        payment.CardType = CreditCardType.visa;
        payment.Amount = "7";
        payment.CurrencyCode = CurrencyCode.USD;
        payment.PaymentAction = PayPalPaymentActionCode.Sale;
        //Emerge Devlepoer Account Details
        //payment.ClientID = "AcGmVhDH9WtsWCB0rIsR1wwHDtxkEtPoUwJDG9KX7Mdz2qrv-xPBxiOFFiWv";
        //payment.ClientSecret = "EN5hVxC-QnPa0djFlZGx4iGAcTh0ZKEe-toAA3RoFYzPJMkeles-QULAj8cL";

        payment.payPalDevelopmentType = PayPalDevelopmentType.SOAP;
        payment.APIUsername = "pushkaraj.awad-facilitator_api1.bitwiseglobal.com";
        payment.APIPassword = "1374826094";
        payment.APISignature = "AXrfFmI66wpR0G-JIZ2Nmfcn0x6NAafhjxe1kMLNpiD0ITwDvnwwJBrc";
        payment.IPAddress = "192.168.1.1";
        payment.MerchantAccountId = "dfsdfsdfsd";
        PaymentStatus result = paymentGateway.SubmitPayment(payment);

        lblMsg.Text = result.Message;

    }

    private void ProcessPayPalREST()
    {

        IPaymentGateway paymentGateway = new PaymentGatewayFactory().CreatePaymentGateway(PaymentGateways.PayPal);
        PayPalPayment payment = new PayPalPayment();


        payment.City = "Johnstown";
        payment.CountryCode = CountryCodeType.US;
        payment.Address = "52 N Main ST";
        payment.Zip = "43210";
        payment.State = "OH";



        payment.CVV = "874";
        payment.ExpirationMonth = 11;
        payment.ExpirationYear = 2018;
        payment.FirstName = "Joe";
        payment.LastName = "Shopper";
        payment.CardNumber = "4417119669820331";
        payment.CardType = CreditCardType.visa;
        payment.Amount = "7";
        payment.CurrencyCode = CurrencyCode.USD;
        payment.PaymentAction = PayPalPaymentActionCode.Sale;
        //Emerge Devlepoer Account Details
        //payment.ClientID = "AcGmVhDH9WtsWCB0rIsR1wwHDtxkEtPoUwJDG9KX7Mdz2qrv-xPBxiOFFiWv";
        //payment.ClientSecret = "EN5hVxC-QnPa0djFlZGx4iGAcTh0ZKEe-toAA3RoFYzPJMkeles-QULAj8cL";

        //
        payment.ClientID = "EBWKjlELKMYqRNQ6sYvFo64FtaRLRR5BdHEESmha49TM";
        payment.ClientSecret = "EO422dn3gQLgDbuwqTjzrFgFtaRLRR5BdHEESmha49TM";
     

        PaymentStatus result = paymentGateway.SubmitPayment(payment);

        lblMsg.Text = result.Message;

    }

    private void ProcessAuthorizeNet()
    {

        IPaymentGateway paymentGateway = new PaymentGatewayFactory().CreatePaymentGateway(PaymentGateways.AuthorizeNet);
        AuthorizeNetPayment payment = new AuthorizeNetPayment();


        payment.City = "Johnstown";
        payment.CountryCode = CountryCodeType.US;
        payment.Address = "52 N Main ST";
        payment.Zip = "43210";
        payment.State = "OH";



        payment.CVV = "874";
        payment.ExpirationMonth = 11;
        payment.ExpirationYear = 2018;
        payment.FirstName = "Joe";
        payment.LastName = "Shopper";
        payment.CardNumber = "4417119669820331";
        payment.CardType = CreditCardType.visa;
        payment.Amount = "8";
        payment.IsProductionEnvironment = false;
        //Emerge Devlepoer Account Details
        //payment.ClientID = "AcGmVhDH9WtsWCB0rIsR1wwHDtxkEtPoUwJDG9KX7Mdz2qrv-xPBxiOFFiWv";
        //payment.ClientSecret = "EN5hVxC-QnPa0djFlZGx4iGAcTh0ZKEe-toAA3RoFYzPJMkeles-QULAj8cL";

        //
        payment.LoginID = "8Fn5Er3Uy";
        payment.TransactionKey = "6tbe7V3U38Mg3g46";


        PaymentStatus result = paymentGateway.SubmitPayment(payment);

        lblMsg.Text = result.Message;

    }

    private void ProcessOrbital()
    {

        IPaymentGateway paymentGateway = new PaymentGatewayFactory().CreatePaymentGateway(PaymentGateways.Orbital);
        OrbitalPayment payment = new OrbitalPayment();


        payment.City = "Johnstown";
        payment.CountryCode = CountryCodeType.US;
        payment.Address = "52 N Main ST";
        payment.Zip = "43210";
        payment.State = "OH";



        payment.CVV = "874";
        payment.ExpirationMonth = 11;
        payment.ExpirationYear = 2018;
        payment.FirstName = "Joe";
        payment.LastName = "Shopper";
        payment.CardNumber = "4417119669820331";
        payment.CardType = CreditCardType.visa;
        payment.Amount = "8";
        payment.IsProductionEnvironment = false;
        //Emerge Devlepoer Account Details
        //payment.ClientID = "AcGmVhDH9WtsWCB0rIsR1wwHDtxkEtPoUwJDG9KX7Mdz2qrv-xPBxiOFFiWv";
        //payment.ClientSecret = "EN5hVxC-QnPa0djFlZGx4iGAcTh0ZKEe-toAA3RoFYzPJMkeles-QULAj8cL";

        //
        payment.MerchantId = "700000009308";
        payment.InvoiceNumber = "1213234434355";


        PaymentStatus result = paymentGateway.SubmitPayment(payment);

        lblMsg.Text = result.Message;

    }


    protected void btnRest_Click(object sender, EventArgs e)
    {

        ProcessPayPalREST();

    }
    protected void btnSOAP_Click(object sender, EventArgs e)
    {
        ProcessPayPalSOAP();
    }
    protected void btnAutho_Click(object sender, EventArgs e)
    {
        ProcessAuthorizeNet();
    }
    protected void btnOrbital_Click(object sender, EventArgs e)
    {
        ProcessOrbital();
    }
}