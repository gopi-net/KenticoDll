using System;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.WebParts.BaseWebParts;

namespace Bluespire.Emerge.Components.CheerCard.WebParts
{
    /// <summary>
    /// Base Class for all the webparts to be created for Cheer Card module.
    /// </summary>
    public class CheerCardWebPart : EmergeBaseWebPart
    {
        /// <summary>
        /// OnInit Method will be used to set the module name to which webpart belongs to.
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            StopProcessing = false;
            Module = Constants.Modules.CheerCard;
            base.OnInit(e);
        }
    }
}
