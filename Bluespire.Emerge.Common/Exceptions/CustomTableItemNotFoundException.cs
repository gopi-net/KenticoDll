using System;
using System.Runtime.Serialization;

namespace Bluespire.Emerge.Common.Exceptions
{
    [Serializable]
    public class CustomTableItemNotFoundException : Exception
    {
        public CustomTableItemNotFoundException()
            : base() { }

        public CustomTableItemNotFoundException(string message)
            : base(message) { }

        public CustomTableItemNotFoundException(string message, Exception inner)
            : base(message, inner) { }

        public CustomTableItemNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
