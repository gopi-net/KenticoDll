using System;

using System.Runtime.Serialization;

namespace Bluespire.Emerge.Common.Exceptions
{
    public class UserNotVolunteerException : Exception
    {
         public UserNotVolunteerException()
            : base() { }

        public UserNotVolunteerException(string message)
            : base(message) { }

        public UserNotVolunteerException(string message, Exception inner)
            : base(message, inner) { }

        public UserNotVolunteerException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
