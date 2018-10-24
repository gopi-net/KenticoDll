using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.LicenseManager.Exceptions
{
    [Serializable]
    public class ExpiredLicenseKeyException : Exception
    {
        public ExpiredLicenseKeyException()
            : base() { }

        public ExpiredLicenseKeyException(string message)
            : base(message) { }

        public ExpiredLicenseKeyException(string message, Exception inner)
            : base(message, inner) { }

        public ExpiredLicenseKeyException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
    
}
