using System;

using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Pages;

namespace Bluespire.Emerge.Components.Career
{
    public class CareerDataSelectFieldsPage : EmergeDataSelectFieldsPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = CareerConstants.CAREER_DATASELECTFIELDSPAGE;
            Module = Constants.Modules.Career;
            base.OnInit(e);
        }
    }
}
