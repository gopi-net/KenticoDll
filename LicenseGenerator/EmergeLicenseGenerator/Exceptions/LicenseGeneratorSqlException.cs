using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergeLicenseGenerator.Exceptions
{
    public class LicenseGeneratorSqlException : Exception
    {
        public LicenseGeneratorSqlException()
            : base() { }

        public LicenseGeneratorSqlException(string message)
            : base(message) { }

        public LicenseGeneratorSqlException(string message, Exception inner)
            : base(message, inner) { }

    }
}
