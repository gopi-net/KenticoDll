using System;
using System.Runtime.Serialization;

namespace Bluespire.Emerge.Common.Exceptions
{
    public class RegistrationExistException : Exception
    {
        public RegistrationExistException()
            : base() { }

        public RegistrationExistException(string message)
            : base(message) { }

        public RegistrationExistException(string message, Exception inner)
            : base(message, inner) { }

        public RegistrationExistException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
