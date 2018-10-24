using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{
    [Serializable]
    public class ModuleNotPurchasedException : Exception
    {
        public ModuleNotPurchasedException()
            : base() { }

        public ModuleNotPurchasedException(string message)
            : base(message) { }

        public ModuleNotPurchasedException(string message, Exception inner)
            : base(message, inner) { }

        public ModuleNotPurchasedException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
