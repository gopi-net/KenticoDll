using System;
using System.Collections.Generic;
using System.Linq;
using Emerge.ImportEngine.Entities;
using Emerge.WebsiteSetupHelper.Delegates;
using Emerge.WebsiteSetupHelper.Infrastructure;
using System.IO;
using Emerge.WebsiteSetupHelper;
using System.Data;
using CMS.Base;
using CMS.Membership;
using CMS.CMSImportExport;
using CMS.IO;
using CMS.PortalEngine;
namespace Emerge.ImportEngine
{
    public class ImportProcessor : IProcessor
    {
        private ImportSettings settings;
        public event LogProgressHandler OnLogProgress;
        
        public ImportProcessor(ImportSettings importSettings)
        {
            settings = importSettings;
            ImportProvider.OnProgressLog += LogProgress;
        }

        public void Process()
        {
            if (!settings.IsValid())
                throw new Exception("The setting object is not valid. You need to provide the website code name, website path, package path and website domain.");

            Init();
            IUserInfo currentUser = getCurrentUser();
            ImportProvider.ImportSite(
                settings.WebsiteCodeName, 
                settings.WebsiteDisplayName, 
                settings.WebsiteDomain, 
                settings.PackagePath, 
                settings.WebsitePath, 
                currentUser);
            
            LogProgress("The import process has been completed successfully..");
        }
        void LogProgress(string message)
        {
            if (OnLogProgress != null)
            {
                OnLogProgress(message);
            }
        }

        private void Init()
        {
            ImportContextHelper.InitializeDatabaseSettings(settings.DatabaseServerName, settings.DatabaseName, settings.UserName, settings.Password);
            ImportContextHelper.InitializeCMSContext();
            VirtualPathHelper.UsingVirtualPathProvider = true;
            SystemContext.WebApplicationPhysicalPath = settings.WebsitePath;
            TransformationInfoProvider.StoreTransformationsInExternalStorage = true;
        }

        private UserInfo getCurrentUser()
        {
            UserInfo currentUser = settings.CurrentUser;
            if (currentUser == null)
                currentUser = MembershipContext.AuthenticatedUser;
            return currentUser;
        }
    }
}
