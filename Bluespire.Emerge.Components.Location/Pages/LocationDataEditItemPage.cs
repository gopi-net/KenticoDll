using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.CMS.SettingsProvider;
using Bluespire.Emerge.Web.Pages;
using CMS.DataEngine;
using CMS.Helpers;
using CMS.UIControls;


namespace Bluespire.Emerge.Components.Location.Pages
{
    public class LocationDataEditItemPage : EmergeDataEditItemPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = LocationConstants.LOCATION_DATAEDITITEMPAGE;
            Module = Constants.Modules.Location;
            base.OnInit(e);
        }
        protected override void setBreadCrumb()
        {
            DataClassInfo dci = DataClassInfoProvider.GetDataClassInfo(CustomTableID);
            string currentItem = string.Empty;
            if (ItemID > 0)
                currentItem = GetString("customtable.data.Edititem");
            else
                currentItem = GetString("customtable.data.NewItem");

            BreadcrumbItem item = new BreadcrumbItem
            {
                Text = GetString(LocationConstants.STRINGCODE_LOCATIONHOME),
                RedirectUrl = LocationConstants.PAGEURL_LOCATION_DASHBOARD
            };
            PageBreadcrumbs.AddBreadcrumb(item);
            item = new BreadcrumbItem
            {
                Text = GetString(LocationConstants.STRINGCODE_LOCATIONHOME),
                RedirectUrl = LocationConstants.PAGEURL_LOCATION_DASHBOARD
            };
            PageBreadcrumbs.AddBreadcrumb(item);

            PageBreadcrumbs.AddBreadcrumb(new BreadcrumbItem
            {
                Text = dci.ClassDisplayName + " List",
                RedirectUrl = ListPage + "?customtableid=" + CustomTableID + (IsSiteManager ? "&sm=1" : String.Empty)
            });

            PageBreadcrumbs.AddBreadcrumb(new BreadcrumbItem
            {
                Text = currentItem
            });

        }
    }
}
