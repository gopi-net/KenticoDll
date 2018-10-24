using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{
   
    [Serializable]
    public class AfterEditActionRedirectToUrlNotFoundException : Exception
    {
        public AfterEditActionRedirectToUrlNotFoundException()
            : base() { }

        public AfterEditActionRedirectToUrlNotFoundException(string message)
            : base(message) { }

        public AfterEditActionRedirectToUrlNotFoundException(string message, Exception inner)
            : base(message, inner) { }

        public AfterEditActionRedirectToUrlNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
