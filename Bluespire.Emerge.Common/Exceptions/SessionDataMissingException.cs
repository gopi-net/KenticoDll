using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{
   
    [Serializable]
    public class SessionDataMissingException : Exception
    {
        public SessionDataMissingException()
            : base() { }

        public SessionDataMissingException(string message)
            : base(message) { }

        public SessionDataMissingException(string message, Exception inner)
            : base(message, inner) { }

        public SessionDataMissingException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
