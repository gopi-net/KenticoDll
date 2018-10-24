using CMS.SiteProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Membership;

namespace Bluespire.Emerge.Common.CMS.SiteProvider
{
    public class EmergeUserInfo
    {
        UserInfo userInfo;
        public EmergeUserInfo(UserInfo userInformation)
        {
            if (userInfo == null)
                throw new Exception("User info cannot be null");
            userInfo = userInformation;
        }

        public bool Enabled
        {
            get
            {
                return this.userInfo.Enabled;
            }
            set
            {
                this.userInfo.Enabled = value;
            }
        }

        public void Update()
        {
            this.userInfo.Update();
        }

        public bool Delete()
        {
            return this.userInfo.Delete();
        }
    }
}
