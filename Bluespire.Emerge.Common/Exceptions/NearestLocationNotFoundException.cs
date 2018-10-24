using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{
   
    [Serializable]
    public class NearestLocationNotFoundException : Exception
    {
        public NearestLocationNotFoundException()
            : base() { }

        public NearestLocationNotFoundException(string message)
            : base(message) { }

        public NearestLocationNotFoundException(string message, Exception inner)
            : base(message, inner) { }

        public NearestLocationNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
