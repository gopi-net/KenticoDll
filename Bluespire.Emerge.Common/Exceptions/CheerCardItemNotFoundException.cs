using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{

    [Serializable]
    public class CheerCardItemNotFoundException : Exception
    {
        public CheerCardItemNotFoundException()
            : base() { }

        public CheerCardItemNotFoundException(string message)
            : base(message) { }

        public CheerCardItemNotFoundException(string message, Exception inner)
            : base(message, inner) { }

        public CheerCardItemNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }

    
}
