using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{
   
    [Serializable]
    public class NoItemSelectedException : Exception
    {
        public NoItemSelectedException()
            : base() { }

        public NoItemSelectedException(string message)
            : base(message) { }

        public NoItemSelectedException(string message, Exception inner)
            : base(message, inner) { }

        public NoItemSelectedException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
