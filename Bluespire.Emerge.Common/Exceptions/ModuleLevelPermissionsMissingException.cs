using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{
   
    [Serializable]
    public class ModuleLevelPermissionsMissingException : Exception
    {
        public ModuleLevelPermissionsMissingException()
            : base() { }

        public ModuleLevelPermissionsMissingException(string message)
            : base(message) { }

        public ModuleLevelPermissionsMissingException(string message, Exception inner)
            : base(message, inner) { }

        public ModuleLevelPermissionsMissingException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
