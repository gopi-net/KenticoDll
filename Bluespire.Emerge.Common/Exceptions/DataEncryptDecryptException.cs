using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Bluespire.Emerge.Common.Exceptions
{
    [Serializable]
    public class DataEncryptDecryptException : Exception
    {
        public DataEncryptDecryptException() { }
        public DataEncryptDecryptException(string message) : base(message) { }
        public DataEncryptDecryptException(string message, Exception inner) : base(message, inner) { }
        protected DataEncryptDecryptException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
