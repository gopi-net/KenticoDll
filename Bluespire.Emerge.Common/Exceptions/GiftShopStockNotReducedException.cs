using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{
   
    [Serializable]
    public class GiftShopStockNotReducedException : Exception
    {
        public GiftShopStockNotReducedException()
            : base() { }

        public GiftShopStockNotReducedException(string message)
            : base(message) { }

        public GiftShopStockNotReducedException(string message, Exception inner)
            : base(message, inner) { }

        public GiftShopStockNotReducedException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
