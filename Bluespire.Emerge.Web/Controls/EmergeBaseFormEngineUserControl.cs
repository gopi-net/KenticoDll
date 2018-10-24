using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.CommonService;
using CMS.FormEngine.Web.UI;

namespace Bluespire.Emerge.Web.Controls
{
    /// <summary>
    /// Base Class for all the Form Controls to be created in Emerge 2.0
    /// </summary>
    public class EmergeBaseFormEngineUserControl : FormEngineUserControl
    {
        /// <summary>
        /// Gets or sets the value for the entire control.
        /// </summary>
        public override object Value
        {
            get;
            set;
        }

        /// <summary>
        /// OnError method.
        /// </summary>
        /// <param name="ex"></param>
        protected void OnError(Exception ex)
        {
            string errorMessage = string.Empty;
            ExceptionHandler.HandleException(ex, ref errorMessage);
            ShowError(errorMessage);
        }
    }
}
