using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Bluespire.Emerge.Common.Exceptions
{
    [Serializable]
    public class CustomTableIdNotFoundException : Exception
    {
        public CustomTableIdNotFoundException()
            : base() { }

        public CustomTableIdNotFoundException(string message)
            : base(message) { }

        public CustomTableIdNotFoundException(string message,Exception inner)
            : base(message,inner) { }
        
        public CustomTableIdNotFoundException(SerializationInfo info, StreamingContext  context)
            : base(info, context) { }
    }
}
