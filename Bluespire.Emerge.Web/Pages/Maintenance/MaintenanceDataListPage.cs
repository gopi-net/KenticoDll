using System;

using Bluespire.Emerge.Common;

namespace Bluespire.Emerge.Web.Pages.Maintenance
{
    public class MaintenanceDataListPage : EmergeDataListPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.MAINTENANCE_DATALISTPAGE;
            Module = Constants.Modules.Maintenance;
            base.OnInit(e);
        }
    }
}
