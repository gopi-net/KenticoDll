using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{
    [Serializable]
    public class PaymentGatewayInsufficientInfoException : Exception
    {
        public PaymentGatewayInsufficientInfoException() { }
        public PaymentGatewayInsufficientInfoException(string message) : base(message) { }
        public PaymentGatewayInsufficientInfoException(string message, Exception inner) : base(message, inner) { }
        protected PaymentGatewayInsufficientInfoException(SerializationInfo info, StreamingContext context) : base(info, context) { }


    }

    [Serializable]
    public class PaymentGatewayException : Exception
    {
        public PaymentGatewayException() { }
        public PaymentGatewayException(string message) : base(message) { }
        public PaymentGatewayException(string message, Exception inner) : base(message, inner) { }
        protected PaymentGatewayException(SerializationInfo info, StreamingContext context) : base(info, context) { }


    }

}
