using System;
using Bluespire.Emerge.Common;

namespace Bluespire.Emerge.Web.Pages.License
{
    public class LicenseDataViewItemPage : EmergeDataViewItemPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.LICENSE_DATAVIEWITEMPAGE;
            Module = Constants.Modules.License;
            base.OnInit(e);
        }
    }
}
