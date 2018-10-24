using System;
using Bluespire.Emerge.Common;

namespace Bluespire.Emerge.Web.Pages.Maintenance
{
    public class MaintenanceListPage : EmergeListPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.MAINTENANCE_LISTPAGE;
            Module = Constants.Modules.Maintenance;
            base.OnInit(e);
        }
    }
}
