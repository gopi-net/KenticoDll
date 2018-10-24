using System;

using Bluespire.Emerge.Common;

namespace Bluespire.Emerge.Web.Pages.Maintenance
{
    public class MaintenanceDataSelectFieldsPage : EmergeDataSelectFieldsPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.MAINTENANCE_DATASELECTFIELDSPAGE;
            Module = Constants.Modules.Maintenance;
            base.OnInit(e);
        }
    }
}
