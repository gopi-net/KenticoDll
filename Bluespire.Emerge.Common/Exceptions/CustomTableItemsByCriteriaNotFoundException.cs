using System;
using System.Runtime.Serialization;

namespace Bluespire.Emerge.Common.Exceptions
{
    [Serializable]
    public class CustomTableItemsByCriteriaNotFoundException : Exception
    {
         public CustomTableItemsByCriteriaNotFoundException()
            : base() { }

        public CustomTableItemsByCriteriaNotFoundException(string message)
            : base(message) { }

        public CustomTableItemsByCriteriaNotFoundException(string message, Exception inner)
            : base(message, inner) { }

        public CustomTableItemsByCriteriaNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
