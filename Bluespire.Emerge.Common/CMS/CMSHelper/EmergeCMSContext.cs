using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.PortalEngine;
using CMS.SiteProvider;
using CMS.Membership;

namespace Bluespire.Emerge.Common.CMS.CMSHelper
{
    public static class EmergeCMSContext
    {

        public static string CurrentSiteName
        {
            get
            {
                return SiteContext.CurrentSiteName;
            }
            set
            {
                SiteContext.CurrentSiteName = value;
            }
        }

        public static int CurrentSiteID
        {
            get
            {
                return SiteContext.CurrentSiteID;
            }
            set
            {
                SiteContext.CurrentSiteID = value;
            }
        }

        public static UserInfo CurrentUser
        {
            get
            {
                
                return MembershipContext.AuthenticatedUser;
            }
        }

        public static bool IsAuthenticated()
        {
            return AuthenticationHelper.IsAuthenticated();
        }

        public static bool IsLiveMode()
        {
            return (PortalContext.ViewMode == ViewModeEnum.LiveSite ? true : false);
        }

        public static bool IsPreviewMode()
        {
            return (PortalContext.ViewMode == ViewModeEnum.Preview ? true : false);
        }
    }
}


