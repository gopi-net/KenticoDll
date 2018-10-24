using CMS.SiteProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Membership;

namespace Bluespire.Emerge.Common.CMS.SiteProvider
{
    public class EmergeUserInfoProvider
    {

        public static EmergeUserInfo GetUserInfo(int userId)
        {
            UserInfo userInfo = UserInfoProvider.GetUserInfo(userId);
            return new EmergeUserInfo(userInfo);
        }
    }
}
