using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{
   
    [Serializable]
    public class ActionNotFeasibleException : Exception
    {
        public ActionNotFeasibleException()
            : base() { }

        public ActionNotFeasibleException(string message)
            : base(message) { }

        public ActionNotFeasibleException(string message, Exception inner)
            : base(message, inner) { }

        public ActionNotFeasibleException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
