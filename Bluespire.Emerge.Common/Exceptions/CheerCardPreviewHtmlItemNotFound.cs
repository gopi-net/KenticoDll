using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{

    [Serializable]
    public class CheerCardPreviewHtmlItemNotFound : Exception
    {
        public CheerCardPreviewHtmlItemNotFound()
            : base() { }

        public CheerCardPreviewHtmlItemNotFound(string message)
            : base(message) { }

        public CheerCardPreviewHtmlItemNotFound(string message, Exception inner)
            : base(message, inner) { }

        public CheerCardPreviewHtmlItemNotFound(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }

    
}
