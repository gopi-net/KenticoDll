using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{
    [Serializable]
    public class PermissionsConfigFileNotFoundException : Exception
    {
        public PermissionsConfigFileNotFoundException() { }
        public PermissionsConfigFileNotFoundException(string message) : base(message) { }
        public PermissionsConfigFileNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected PermissionsConfigFileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
