using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Pages.BasePages;

namespace Bluespire.Emerge.Components.PreRegistration.Pages
{
    public class PreRegistrationDashboardPage : EmergeToolsPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.PREREGISTRATION_DASHBOARDPAGE;
            Module = Constants.Modules.PreRegistration;
            base.OnInit(e);
        }
    }
}
