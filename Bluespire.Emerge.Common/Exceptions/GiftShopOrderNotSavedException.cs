using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{
   
    [Serializable]
    public class GiftShopOrderNotSavedException : Exception
    {
        public GiftShopOrderNotSavedException()
            : base() { }

        public GiftShopOrderNotSavedException(string message)
            : base(message) { }

        public GiftShopOrderNotSavedException(string message, Exception inner)
            : base(message, inner) { }

        public GiftShopOrderNotSavedException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
