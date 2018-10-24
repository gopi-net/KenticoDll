using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{
   
    [Serializable]
    public class ExpectedColumnNotFoundException : Exception
    {
        public ExpectedColumnNotFoundException()
            : base() { }

        public ExpectedColumnNotFoundException(string message)
            : base(message) { }

        public ExpectedColumnNotFoundException(string message, Exception inner)
            : base(message, inner) { }

        public ExpectedColumnNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
