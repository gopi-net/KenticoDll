using Bluespire.Emerge.CommonService.PaymentGateway.GatewayType;
using Bluespire.Emerge.CommonService.PaymentGateway.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.CommonService.PaymentGateway
{
    public class PaymentGatewayFactory:IPaymentGatewayFactory
    {
        /// <summary>
        /// Factory Class Return Authorize.Net, PayPal, Orbital Object.
        /// </summary>
        /// <param name="paymentGateways"></param>
        /// <returns></returns>
        public  IPaymentGateway CreatePaymentGateway(PaymentGateways paymentGateways)
        {
           
            switch (paymentGateways)
            {
                case PaymentGateways.AuthorizeNet:
                    return new AuthorizeNet();
                case PaymentGateways.PayPal:
                    return new PayPalProvider();
                case PaymentGateways.Orbital:
                    return new Orbital();
                default:
                    throw new NotSupportedException("This payment gateway is not supported. Implement IPaymentGatewayFactory interface.");
            }
        }
    }
  
}
