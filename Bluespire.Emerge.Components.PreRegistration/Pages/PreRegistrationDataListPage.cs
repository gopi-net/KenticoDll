using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Web.Pages;
using Bluespire.Emerge.Common;
using CMS.Helpers;
using CMS.UIControls;

namespace Bluespire.Emerge.Components.PreRegistration.Pages
{
    public class PreRegistrationDataListPage : EmergeDataListPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.PREREGISTRATION_DATALISTPAGE;
            Module = Constants.Modules.PreRegistration;
            base.OnInit(e);
        }
        protected override void setBreadCrumb()
        {
            BreadcrumbItem item = new BreadcrumbItem
            {
                Text = GetString(PreRegistrationConstants.STRINGCODE_PREREGISTRATIONHOME),
                RedirectUrl = PreRegistrationConstants.PAGEURL_PREREG_DASHBOARD
            };
            PageBreadcrumbs.AddBreadcrumb(item);

            item = new BreadcrumbItem
            {
                Text = GetString(PreRegistrationConstants.STRINGCODE_PREREGISTRATIONHOME),
                RedirectUrl = PreRegistrationConstants.PAGEURL_PREREG_DASHBOARD
            };
            PageBreadcrumbs.AddBreadcrumb(item);


            item = new BreadcrumbItem
            {
                Text = DataClassInfo.ClassDisplayName
            };
            PageBreadcrumbs.AddBreadcrumb(item);
        }       
    }
}
