using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Pages;

namespace Bluespire.Emerge.Components.Location.Pages
{
    public class LocationDataViewItemPage : EmergeDataViewItemPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = LocationConstants.LOCATION_DATAVIEWITEMPAGE;
            Module = Constants.Modules.Location;
            base.OnInit(e);
        }
    }
}
