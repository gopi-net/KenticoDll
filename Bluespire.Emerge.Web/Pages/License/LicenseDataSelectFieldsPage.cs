using System;

using Bluespire.Emerge.Common;

namespace Bluespire.Emerge.Web.Pages.License
{
    public class LicenseDataSelectFieldsPage : EmergeDataSelectFieldsPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.LICENSE_DATASELECTFIELDSPAGE;
            Module = Constants.Modules.License;
            base.OnInit(e);
        }
    }
}
