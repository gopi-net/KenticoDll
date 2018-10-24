using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{
   
    [Serializable]
    public class CheerCardPreviewImageConfigItemsNotFound : Exception
    {
        public CheerCardPreviewImageConfigItemsNotFound()
            : base() { }

        public CheerCardPreviewImageConfigItemsNotFound(string message)
            : base(message) { }

        public CheerCardPreviewImageConfigItemsNotFound(string message, Exception inner)
            : base(message, inner) { }

        public CheerCardPreviewImageConfigItemsNotFound(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
