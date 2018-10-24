using System;
using Bluespire.Emerge.Common;

namespace Bluespire.Emerge.Web.Pages.License
{
    public class LicenseListPage : EmergeListPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.LICENSE_LISTPAGE;
            Module = Constants.Modules.License;
            base.OnInit(e);
        }
    }
}
