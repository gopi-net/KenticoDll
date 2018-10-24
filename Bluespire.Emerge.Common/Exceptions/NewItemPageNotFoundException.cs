using System;
using System.Runtime.Serialization;

namespace Bluespire.Emerge.Common.Exceptions
{
    [Serializable]
    public class NewItemPageNotFoundException : Exception
    {
        public NewItemPageNotFoundException() { }
        public NewItemPageNotFoundException(string message) : base(message) { }
        public NewItemPageNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected NewItemPageNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
