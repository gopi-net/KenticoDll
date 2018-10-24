using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.LicenseManager.Exceptions
{
   
    [Serializable]
    public class MissingLicenseKeyException : Exception
    {
        public MissingLicenseKeyException()
            : base() { }

        public MissingLicenseKeyException(string message)
            : base(message) { }

        public MissingLicenseKeyException(string message, Exception inner)
            : base(message, inner) { }

        public MissingLicenseKeyException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
