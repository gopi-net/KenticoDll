using System;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Pages;

namespace Bluespire.Emerge.Components.Career
{
    public class CareerListPage : EmergeListPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = CareerConstants.CAREER_LISTPAGE;
            Module = Constants.Modules.Career;
            base.OnInit(e);
        }
    }
}
