using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Bluespire.Emerge.Common.Exceptions
{
    [Serializable]
    public class SystemInMaintenanceMode : Exception
    {
        public SystemInMaintenanceMode() { }
        public SystemInMaintenanceMode(string message) : base(message) { }
        public SystemInMaintenanceMode(string message, Exception inner) : base(message, inner) { }
        protected SystemInMaintenanceMode(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
