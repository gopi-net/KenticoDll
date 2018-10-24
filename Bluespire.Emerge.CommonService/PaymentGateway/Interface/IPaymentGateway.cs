using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.CommonService.PaymentGateway.Interface
{
    public interface IPaymentGateway
    {
        /// <summary>
        ///  Connect with payment gateway and process transaction.
        /// </summary>
        /// <param name="payment">Accept user payment details</param>
        /// <returns >Payment Status</returns>
        /// <exception cref="NotSupportedException, ArgumentNullException, InvalidOperationException, 
        /// InvalidOperationException, ArgumentException, UriFormatException, System.Security.SecurityException
        /// "  >Throw PaymentGatewayException </exception>
        PaymentStatus SubmitPayment(Payment payment);
       
    }
}
