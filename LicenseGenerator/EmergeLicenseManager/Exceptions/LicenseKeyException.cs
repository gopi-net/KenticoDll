using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.LicenseManager.Exceptions
{

    public class LicenseKeyException : Exception
    {
        public LicenseKeyException()
            : base() { }

        public LicenseKeyException(string message)
            : base(message) { }

        public LicenseKeyException(string message, Exception inner)
            : base(message, inner) { }

        public LicenseKeyException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

    }
}
