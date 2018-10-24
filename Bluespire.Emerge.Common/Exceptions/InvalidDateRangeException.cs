using System;
using System.Runtime.Serialization;

namespace Bluespire.Emerge.Common.Exceptions
{
    [Serializable]
    public class InvalidDateRangeException : Exception
    {
        public InvalidDateRangeException()
            : base() { }

        public InvalidDateRangeException(string message)
            : base(message) { }

        public InvalidDateRangeException(string message, Exception inner)
            : base(message, inner) { }

        public InvalidDateRangeException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
