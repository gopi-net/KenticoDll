using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Pages.BasePages;

namespace Bluespire.Emerge.Components.StaffDirectory.Pages
{
    public class StaffDirectoryDashboardPage : EmergeToolsPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.STAFF_DIRECTORY_DASHBOARDPAGE;
            Module = Constants.Modules.StaffDirectory;
            base.OnInit(e);
        }
    }
}
