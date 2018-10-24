using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.DataEngine;

namespace Bluespire.Emerge.Common.CMS.SettingsProvider
{
    public class EmergeQueryDataParameters
    {
        QueryDataParameters parameters = new QueryDataParameters();

        public QueryDataParameters Parameters
        {
            get
            {
                return parameters;
            }
        }

        public void Add(string name, object value)
        {
            this.parameters.Add(name, value);
        }
        
    }
}
