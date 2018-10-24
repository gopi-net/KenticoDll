using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{
   
    public class CustomTableStatusColumnNotExistsException : Exception
    {
        public CustomTableStatusColumnNotExistsException()
            : base() { }

        public CustomTableStatusColumnNotExistsException(string message)
            : base(message) { }

        public CustomTableStatusColumnNotExistsException(string message, Exception inner)
            : base(message, inner) { }

        public CustomTableStatusColumnNotExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
