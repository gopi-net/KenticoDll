using System;
using System.Runtime.Serialization;

namespace Bluespire.Emerge.Common.Exceptions
{
    [Serializable]
    public class UserWithUserNameExistsException : Exception
    {
        public UserWithUserNameExistsException() { }
        public UserWithUserNameExistsException(string message) : base(message) { }
        public UserWithUserNameExistsException(string message, Exception inner) : base(message, inner) { }
        protected UserWithUserNameExistsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
