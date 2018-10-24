using System;
using Bluespire.Emerge.Common;

namespace Bluespire.Emerge.Web.Pages.Maintenance
{
    public class MaintenanceDataEditItemPage : EmergeDataEditItemPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.MAINTENANCE_DATAEDITITEMPAGE;
            Module = Constants.Modules.Maintenance;
            base.OnInit(e);
        }
    }
}
