using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=true, Inherited=true)]
    public class CustomTableFieldAttribute: Attribute
    {
        public string FieldName
        {
            get;
            set;
        }
    }
}
