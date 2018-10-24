using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Web.Pages;
using Bluespire.Emerge.Common;

namespace Bluespire.Emerge.Components.PreRegistration.Pages
{
    public class PreRegistrationDataSelectFieldsPage : EmergeDataSelectFieldsPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.PREREGISTRATION_DATASELECTFIELDSPAGE;
            Module = Constants.Modules.PreRegistration;
            base.OnInit(e);
        }
    }
}
