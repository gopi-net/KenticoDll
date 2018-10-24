using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.WebParts.BaseWebParts;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Bluespire.Emerge.Components.Rates.WebParts
{
    /// <summary>
    /// Base Class for all the webparts to be created for Cheer Card module.
    /// </summary>
    public class RatesWebPart : EmergeBaseWebPart
    {
        /// <summary>
        /// OnInit Method will be used to set the module name to which webpart belongs to.
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            StopProcessing = false;
            Module = Constants.Modules.Rates;
            base.OnInit(e);
        }
    }


}
