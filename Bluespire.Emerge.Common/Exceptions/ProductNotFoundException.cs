﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common.Exceptions
{
   
    [Serializable]
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException()
            : base() { }

        public ProductNotFoundException(string message)
            : base(message) { }

        public ProductNotFoundException(string message, Exception inner)
            : base(message, inner) { }

        public ProductNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
