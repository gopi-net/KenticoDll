using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{
   
    [Serializable]
    public class CheerCardEmailToFormFieldMissingException : Exception
    {
        public CheerCardEmailToFormFieldMissingException()
            : base() { }

        public CheerCardEmailToFormFieldMissingException(string message)
            : base(message) { }

        public CheerCardEmailToFormFieldMissingException(string message, Exception inner)
            : base(message, inner) { }

        public CheerCardEmailToFormFieldMissingException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
