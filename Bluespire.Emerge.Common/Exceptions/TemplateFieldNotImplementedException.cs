using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{
   
    [Serializable]
    public class TemplateFieldNotImplementedException : Exception
    {
        public TemplateFieldNotImplementedException()
            : base() { }

        public TemplateFieldNotImplementedException(string message)
            : base(message) { }

        public TemplateFieldNotImplementedException(string message, Exception inner)
            : base(message, inner) { }

        public TemplateFieldNotImplementedException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
