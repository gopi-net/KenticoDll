using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{
    [Serializable]
    public class InvalidCustomTableNameException : Exception
    {
        public InvalidCustomTableNameException()
            : base() { }

        public InvalidCustomTableNameException(string message)
            : base(message) { }

        public InvalidCustomTableNameException(string message, Exception inner)
            : base(message, inner) { }

        public InvalidCustomTableNameException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
  
}
