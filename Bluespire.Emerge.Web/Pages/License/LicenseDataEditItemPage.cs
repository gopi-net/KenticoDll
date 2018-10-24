using System;
using Bluespire.Emerge.Common;

namespace Bluespire.Emerge.Web.Pages.License
{
    public class LicenseDataEditItemPage : EmergeDataEditItemPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.LICENSE_DATAEDITITEMPAGE;
            Module = Constants.Modules.License;
            base.OnInit(e);
        }
    }
}
