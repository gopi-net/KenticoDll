using System;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Pages;

namespace Bluespire.Emerge.Components.StaffDirectory.Pages
{
    public class StaffDirectoryDataViewItemPage : EmergeDataViewItemPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.STAFF_DIRECTORY_DATAVIEWITEMPAGE;
            Module = Constants.Modules.StaffDirectory;
            base.OnInit(e);
        }
    }
}
