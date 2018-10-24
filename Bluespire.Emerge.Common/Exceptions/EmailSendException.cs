using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Bluespire.Emerge.Common.Exceptions
{
    [Serializable]
    public class EmailSendException : Exception
    {
        public EmailSendException() : base() { }
        public EmailSendException(string message) : base(message) { }
        public EmailSendException(string message, Exception inner) : base(message, inner) { }
        public EmailSendException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
