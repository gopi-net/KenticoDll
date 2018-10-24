using Emerge.WebsiteSetupHelper.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerge.DatabaseEngine
{
    public class DatabaseSettings
    {
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

        public string WebsitePath
        {
            get;
            set;
        }
    }
}
