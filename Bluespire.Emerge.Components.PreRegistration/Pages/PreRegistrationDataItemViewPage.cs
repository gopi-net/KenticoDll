using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Web.Pages;
using Bluespire.Emerge.Common;

namespace Bluespire.Emerge.Components.PreRegistration.Pages
{
    public class PreRegistrationDataItemViewPage : EmergeDataViewItemPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.PREREGISTRATION_DATAVIEWITEMPAGE;
            Module = Constants.Modules.PreRegistration;
            base.OnInit(e);
        }
    }
}
