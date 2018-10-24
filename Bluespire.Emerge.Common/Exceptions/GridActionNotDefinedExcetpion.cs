using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{
    [Serializable]
    public class GridActionNotDefinedExcetpion : Exception
    {
        public GridActionNotDefinedExcetpion()
            : base() { }

        public GridActionNotDefinedExcetpion(string message)
            : base(message) { }

        public GridActionNotDefinedExcetpion(string message, Exception inner)
            : base(message, inner) { }

        public GridActionNotDefinedExcetpion(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }

}
