using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{

    [Serializable]
    public class PermissionsMissingInPermissionConfigException : Exception
    {
        public PermissionsMissingInPermissionConfigException()
            : base() { }

        public PermissionsMissingInPermissionConfigException(string message)
            : base(message) { }

        public PermissionsMissingInPermissionConfigException(string message, Exception inner)
            : base(message, inner) { }

        public PermissionsMissingInPermissionConfigException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
