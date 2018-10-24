using System;
using System.Runtime.Serialization;


namespace Bluespire.Emerge.Common.Exceptions
{
    [Serializable]
    public class InvalidDiscountCodeException : Exception
    {
        public InvalidDiscountCodeException() { }
        public InvalidDiscountCodeException(string message) : base(message) { }
        public InvalidDiscountCodeException(string message, Exception inner) : base(message, inner) { }
        protected InvalidDiscountCodeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
