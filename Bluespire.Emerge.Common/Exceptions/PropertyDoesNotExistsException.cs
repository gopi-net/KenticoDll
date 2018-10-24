using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{
   
    [Serializable]
    public class PropertyDoesNotExistsException : Exception
    {
        public PropertyDoesNotExistsException()
            : base() { }

        public PropertyDoesNotExistsException(string message)
            : base(message) { }

        public PropertyDoesNotExistsException(string message, Exception inner)
            : base(message, inner) { }

        public PropertyDoesNotExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
