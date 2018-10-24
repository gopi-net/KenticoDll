using System;
using System.Runtime.Serialization;

namespace Bluespire.Emerge.Common.Exceptions
{
    [Serializable]
    public class ViewItemPageNotFoundException : Exception
    {
        public ViewItemPageNotFoundException() { }
        public ViewItemPageNotFoundException(string message) : base(message) { }
        public ViewItemPageNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected ViewItemPageNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
