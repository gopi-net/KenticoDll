using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{
   
    [Serializable]
    public class ConfigurationItemMissingException : Exception
    {
        public ConfigurationItemMissingException()
            : base() { }

        public ConfigurationItemMissingException(string message)
            : base(message) { }

        public ConfigurationItemMissingException(string message, Exception inner)
            : base(message, inner) { }

        public ConfigurationItemMissingException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
