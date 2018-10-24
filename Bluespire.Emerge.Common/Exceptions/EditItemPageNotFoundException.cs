using System;
using System.Runtime.Serialization;

namespace Bluespire.Emerge.Common.Exceptions
{
    [Serializable]
    public class EditItemPageNotFoundException : Exception
    {
        public EditItemPageNotFoundException() { }
        public EditItemPageNotFoundException(string message) : base(message) { }
        public EditItemPageNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected EditItemPageNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
