using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Membership;

namespace Bluespire.Emerge.Common.CMS.CMSHelper
{
    public class EmergeCurrentUser
    {
        public static int UserID
        {
            get
            {
                return MembershipContext.AuthenticatedUser.UserID;
            }
            set
            {
                MembershipContext.AuthenticatedUser.UserID = value;
            }
        }
    }
}
