using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Bluespire.Emerge.Common.Exceptions
{
    [Serializable]
    public class ModuleInMaintenanceModeException : Exception
    {
        public ModuleInMaintenanceModeException() { }
        public ModuleInMaintenanceModeException(string message) : base(message) { }
        public ModuleInMaintenanceModeException(string message, Exception inner) : base(message, inner) { }
        protected ModuleInMaintenanceModeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
