using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{
   
    [Serializable]
    public class RatesSheetNotFoundException : Exception
    {
        public RatesSheetNotFoundException()
            : base() { }

        public RatesSheetNotFoundException(string message)
            : base(message) { }

        public RatesSheetNotFoundException(string message, Exception inner)
            : base(message, inner) { }

        public RatesSheetNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
