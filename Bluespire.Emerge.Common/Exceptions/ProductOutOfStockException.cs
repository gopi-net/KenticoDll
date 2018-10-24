using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{
   
    [Serializable]
    public class ProductOutOfStockException : Exception
    {
        public ProductOutOfStockException()
            : base() { }

        public ProductOutOfStockException(string message)
            : base(message) { }

        public ProductOutOfStockException(string message, Exception inner)
            : base(message, inner) { }

        public ProductOutOfStockException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
