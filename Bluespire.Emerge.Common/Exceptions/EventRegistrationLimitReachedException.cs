using System;
using System.Runtime.Serialization;

namespace Bluespire.Emerge.Common.Exceptions
{
     [Serializable]
    public class EventRegistrationLimitReachedException : Exception
    {
        public EventRegistrationLimitReachedException()
            : base() { }

        public EventRegistrationLimitReachedException(string message)
            : base(message) { }

        public EventRegistrationLimitReachedException(string message, Exception inner)
            : base(message, inner) { }

        public EventRegistrationLimitReachedException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
