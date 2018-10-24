using CMS.Base.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Web.Controls
{
    /// <summary>
    /// Used in case custom table column mappings needed.
    /// </summary>
    public class EmergeTextBox : CMSTextBox
    {
        /// <summary>
        /// Comma seperated string containing column names of Custom table on which search will be carried out. 
        /// </summary>
        public string MappedToCustomTableColumns
        {
            get;
            set;
        }
    }
}
