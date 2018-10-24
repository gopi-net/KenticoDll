using System;

using Bluespire.Emerge.Common;

namespace Bluespire.Emerge.Web.Pages.License
{
    public class LicenseDataListPage : EmergeDataListPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.LICENSE_DATALISTPAGE;
            Module = Constants.Modules.License;
            base.OnInit(e);
        }
    }
}
