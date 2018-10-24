using Bluespire.Emerge.Web.WebParts.BaseWebParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;

namespace Bluespire.Emerge.Components.Donation.WebParts
{
    public class DonationBaseWebpart : EmergeBaseWebPart
    {
        protected override void OnInit(EventArgs e)
        {
            StopProcessing = false;
            Module = Constants.Modules.Donation;
            base.OnInit(e);
        }
    }
}
