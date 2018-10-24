using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.WebParts.BaseWebParts;

namespace Bluespire.Emerge.Components.Location.WebParts
{

    public class LocationWebPart : EmergeBaseWebPart
    {

        /// <summary>
        /// OnInit Method will be used to set the module name to which webpart belongs to.
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            StopProcessing = false;
            Module = Constants.Modules.Location;
            base.OnInit(e);
        }

    }
}
