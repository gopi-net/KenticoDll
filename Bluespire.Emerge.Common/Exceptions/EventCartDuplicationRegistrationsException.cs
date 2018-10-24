using System;
using System.Runtime.Serialization;

namespace Bluespire.Emerge.Common.Exceptions
{
     [Serializable]
    public class EventCartDuplicationRegistrationsException :Exception
    {
         public EventCartDuplicationRegistrationsException()
            : base() { }

        public EventCartDuplicationRegistrationsException(string message)
            : base(message) { }

        public EventCartDuplicationRegistrationsException(string message, Exception inner)
            : base(message, inner) { }

        public EventCartDuplicationRegistrationsException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
