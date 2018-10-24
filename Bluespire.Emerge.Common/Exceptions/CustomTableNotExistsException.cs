using System;
using System.Runtime.Serialization;

namespace Bluespire.Emerge.Common.Exceptions
{
    [Serializable]
    public class CustomTableNotExistsException : Exception
    {
         public CustomTableNotExistsException()
            : base() { }

        public CustomTableNotExistsException(string message)
            : base(message) { }

        public CustomTableNotExistsException(string message, Exception inner)
            : base(message, inner) { }

        public CustomTableNotExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
