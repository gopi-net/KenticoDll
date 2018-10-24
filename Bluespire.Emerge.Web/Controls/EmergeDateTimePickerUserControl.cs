using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.CommonService;
using CMS.UIControls;

namespace Bluespire.Emerge.Web.Controls
{
    /// <summary>
    /// Base Class for all the user Control to be created in Emerge 2.0
    /// </summary>
    public class EmergeDateTimePickerUserControl : EmergeBaseCMSUserControl
    {
        public virtual DateTime SelectedDateTime
        {
            get;
            set;
        }
        public virtual bool IsValid()
        {
            return true;
        }

        public virtual bool NeedsValidation
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
