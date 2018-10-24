using System;
using Bluespire.Emerge.Common;

namespace Bluespire.Emerge.Web.Pages.Maintenance
{
    public class MaintenanceDataViewItemPage : EmergeDataViewItemPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.MAINTENANCE_DATAVIEWITEMPAGE;
            Module = Constants.Modules.Maintenance;
            base.OnInit(e);
        }
    }
}
