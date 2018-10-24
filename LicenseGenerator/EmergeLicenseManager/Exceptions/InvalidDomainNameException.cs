using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.LicenseManager.Exceptions
{
    [Serializable]
    public class InvalidDomainNameException : Exception
    {
        public InvalidDomainNameException()
            : base() { }

        public InvalidDomainNameException(string message)
            : base(message) { }

        public InvalidDomainNameException(string message, Exception inner)
            : base(message, inner) { }

        public InvalidDomainNameException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
    
}
