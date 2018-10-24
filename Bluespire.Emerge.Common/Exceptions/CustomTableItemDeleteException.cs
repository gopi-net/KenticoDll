using System;
using System.Runtime.Serialization;

namespace Bluespire.Emerge.Common.Exceptions
{
    [Serializable]
    public class CustomTableItemDeleteException: Exception
    {
        public CustomTableItemDeleteException()
            : base() { }

        public CustomTableItemDeleteException(string message)
            : base(message) { }

        public CustomTableItemDeleteException(string message, Exception inner)
            : base(message, inner) { }

        public CustomTableItemDeleteException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
