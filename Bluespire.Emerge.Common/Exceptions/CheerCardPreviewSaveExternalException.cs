using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{
   
    [Serializable]
    public class CheerCardPreviewSaveExternalException : Exception
    {
        public CheerCardPreviewSaveExternalException()
            : base() { }

        public CheerCardPreviewSaveExternalException(string message)
            : base(message) { }

        public CheerCardPreviewSaveExternalException(string message, Exception inner)
            : base(message, inner) { }

        public CheerCardPreviewSaveExternalException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
