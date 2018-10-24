using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{
   
    [Serializable]
    public class GeocodingServiceException : Exception
    {
        public GeocodingServiceException()
            : base() { }

        public GeocodingServiceException(string message)
            : base(message) { }

        public GeocodingServiceException(string message, Exception inner)
            : base(message, inner) { }

        public GeocodingServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
