using CMS.Membership;
using System;

namespace Emerge.ImportEngine.Entities
{
    public class ImportSettings
    {
        public string WebsiteCodeName
        {
            get;
            set;
        }

        public string WebsitePath
        {
            get;
            set;
        }

        public string PackagePath
        {
            get;
            set;
        }

        public string WebsiteDisplayName
        {
            get;
            set;
        }

        public string WebsiteDomain
        {
            get;
            set;
        }

        public string DatabaseServerName
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string DatabaseName
        {
            get;
            set;
        }

        public UserInfo CurrentUser
        {
            get;
            set;
        }

        public bool IsValid()
        {
            if (String.IsNullOrEmpty(this.WebsiteCodeName) || String.IsNullOrEmpty(this.WebsitePath) || String.IsNullOrEmpty(this.PackagePath) ||
                String.IsNullOrEmpty(this.WebsiteDomain))
                return false;

            return true;
        }
    }
}
