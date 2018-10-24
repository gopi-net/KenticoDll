using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Components.Career;
using Bluespire.Emerge.Web.Pages.BasePages;

namespace Bluespire.Emerge.Components.Career.Pages
{
    public class CareerDashboardPage : EmergeToolsPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = CareerConstants.CAREER_DASHBOARDPAGE;
            Module = Constants.Modules.Career;
            base.OnInit(e);
        }
    }
}
