using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Bluespire.Emerge.Common.Exceptions
{
    [Serializable]
    public class SystemInMaintenanceModeException : Exception
    {
        public SystemInMaintenanceModeException() { }
        public SystemInMaintenanceModeException(string message) : base(message) { }
        public SystemInMaintenanceModeException(string message, Exception inner) : base(message, inner) { }
        protected SystemInMaintenanceModeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
