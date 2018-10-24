using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{
   
    [Serializable]
    public class RatesIncorrectMappingsException : Exception
    {
        public RatesIncorrectMappingsException()
            : base() { }

        public RatesIncorrectMappingsException(string message)
            : base(message) { }

        public RatesIncorrectMappingsException(string message, Exception inner)
            : base(message, inner) { }

        public RatesIncorrectMappingsException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
