using System;
using System.Runtime.Serialization;

namespace Bluespire.Emerge.Common.Exceptions
{
    [Serializable]
    public class UserWithEmailExistsException : Exception
    {
        public UserWithEmailExistsException() { }
        public UserWithEmailExistsException(string message) : base(message) { }
        public UserWithEmailExistsException(string message, Exception inner) : base(message, inner) { }
        protected UserWithEmailExistsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
