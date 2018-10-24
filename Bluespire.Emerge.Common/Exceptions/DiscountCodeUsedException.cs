using System;
using System.Runtime.Serialization;

namespace Bluespire.Emerge.Common.Exceptions
{
    [Serializable]
    public class DiscountCodeUsedException : Exception
    {
        public DiscountCodeUsedException()
            : base() { }

        public DiscountCodeUsedException(string message)
            : base(message) { }

        public DiscountCodeUsedException(string message, Exception inner)
            : base(message, inner) { }

        public DiscountCodeUsedException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
