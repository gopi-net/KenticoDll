using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergeLicenseGenerator.Exceptions
{
    public class NoLicenseFoundException : Exception
    {
        public NoLicenseFoundException()
            : base() { }

        public NoLicenseFoundException(string message)
            : base(message) { }

        public NoLicenseFoundException(string message, Exception inner)
            : base(message, inner) { }

    }
}
