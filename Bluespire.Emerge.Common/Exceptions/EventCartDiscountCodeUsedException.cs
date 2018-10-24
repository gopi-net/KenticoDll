using System;
using System.Runtime.Serialization;

namespace Bluespire.Emerge.Common.Exceptions
{
    [Serializable]
    public class EventCartDiscountCodeUsedException : Exception
    {
        public EventCartDiscountCodeUsedException()
            : base() { }

        public EventCartDiscountCodeUsedException(string message)
            : base(message) { }

        public EventCartDiscountCodeUsedException(string message, Exception inner)
            : base(message, inner) { }

        public EventCartDiscountCodeUsedException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
