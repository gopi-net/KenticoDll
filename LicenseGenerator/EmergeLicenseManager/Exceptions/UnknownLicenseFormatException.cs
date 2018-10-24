using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.LicenseManager.Exceptions
{
    [Serializable]
    public class UnknownLicenseFormatException : Exception
    {
        public UnknownLicenseFormatException()
            : base() { }

        public UnknownLicenseFormatException(string message)
            : base(message) { }

        public UnknownLicenseFormatException(string message, Exception inner)
            : base(message, inner) { }

        public UnknownLicenseFormatException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
    
}
