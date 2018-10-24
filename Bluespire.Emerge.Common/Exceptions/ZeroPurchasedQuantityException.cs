using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{
   
    [Serializable]
    public class ZeroPurchasedQuantityException : Exception
    {
        public ZeroPurchasedQuantityException()
            : base() { }

        public ZeroPurchasedQuantityException(string message)
            : base(message) { }

        public ZeroPurchasedQuantityException(string message, Exception inner)
            : base(message, inner) { }

        public ZeroPurchasedQuantityException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
