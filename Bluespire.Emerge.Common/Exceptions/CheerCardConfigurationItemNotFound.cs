using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{
   
    [Serializable]
    public class CheerCardConfigurationItemNotFound : Exception
    {
        public CheerCardConfigurationItemNotFound()
            : base() { }

        public CheerCardConfigurationItemNotFound(string message)
            : base(message) { }

        public CheerCardConfigurationItemNotFound(string message, Exception inner)
            : base(message, inner) { }

        public CheerCardConfigurationItemNotFound(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
